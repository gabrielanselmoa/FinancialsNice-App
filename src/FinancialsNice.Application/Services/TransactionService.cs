using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Dtos.Transactions;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Interfaces.WebHook;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;

namespace FinancialsNice.Application.Services;

public class TransactionService(
    ITransactionRepository transactionRepository,
    ICardRepository cardRepository,
    IPayerReceiverRepository payerReceiverRepository,
    IPaymentRepository paymentRepository,
    IWebhookClient webhookClient,
    ILogger<TransactionService> logger,
    IUserRepository userRepository,
    IGoalRepository goalRepository)
    : ITransactionService
{
    private readonly IWebhookClient _webhookClient = webhookClient;
    private readonly ILogger<TransactionService> _logger = logger;

    public async Task<PagedResponseModel<ICollection<TransactionResponse>>> GetAllAsync(string? search, string?
            startDate, string? endDate, int page,
        int perPage, Guid userId)
    {
        var response = new PagedResponseModel<ICollection<TransactionResponse>>();
        var transactions = await transactionRepository.GetAllAsync(search?.ToLower(), userId);

        if (!transactions.Any())
        {
            response.Data = [];
            response.Meta = new MetaData()
            {
                TotalItems = 0,
                TotalPages = 0,
                Page = page,
                PerPage = perPage
            };
            response.Message = "No transactions were found";
            response.Success = true;
            return response;
        }

        var minDate = DateOnly.FromDateTime(DateTime.Today.AddYears(-2));
        var maxDate = DateOnly.FromDateTime(DateTime.Today.AddYears(1));
        var start = string.IsNullOrWhiteSpace(startDate) ? minDate : DateConvertHelper.ParseDateOnly(startDate);
        var end = string.IsNullOrWhiteSpace(endDate) ? maxDate : DateConvertHelper.ParseDateOnly(endDate);

        var collection = transactions.Where(t => t.ScheduledAt >= start && t
            .ScheduledAt <= end).ToList();

        var totalItems = collection.Count();
        var totalPages = (int)Math.Ceiling((double)collection.Count() / perPage);

        if (!collection.Any())
        {
            response.Data = [];
            response.Meta = new MetaData()
            {
                Page = page,
                PerPage = perPage,
                TotalItems = totalItems,
                TotalPages = totalPages,
                NextPage = page < totalPages ? true : null,
                PrevPage = page > 1 ? true : null
            };
            response.Message = "No transactions were found!";
            response.Success = false;
            return response;
        }

        var dto = collection.Select(TransactionMapper.ToResponse).ToList();

        response.Data = dto;
        response.Meta = new MetaData()
        {
            Page = page,
            PerPage = perPage,
            TotalItems = totalItems,
            TotalPages = totalPages,
            NextPage = page < totalPages ? true : null,
            PrevPage = page > 1 ? true : null
        };
        response.Message = "Success";
        response.Success = true;
        return response;
    }

    public async Task<ResponseModel<TransactionResponse?>> GetByIdAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<TransactionResponse?>();
        var transaction = await transactionRepository.GetByIdAsync(id);
        if (transaction == null || transaction.OwnerId != userId)
            return response.Fail(null, "Transaction not found or does not belong to the user!");

        var dto = TransactionMapper.ToResponse(transaction);
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<ICollection<TransactionPreview>>> GetLastAsync(Guid userId)
    {
        var response = new ResponseModel<ICollection<TransactionPreview>>();
        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
            return response.Fail([], "Owner not found!");

        var now = DateOnly.FromDateTime(DateTime.Today);
        var maxDue = DateOnly.FromDateTime(DateTime.Today.AddDays(3));

        var transactions = await transactionRepository.GetAllAsync(null, userId);
        if (!transactions.Any())
            return response.Ok([], "No transactions were found!");

        var last = transactions.Where(t => t.ScheduledAt >= now && t.ScheduledAt <= maxDue).Take(3).ToList();
        var dto = last.Select(TransactionMapper.ToPreview).ToList();
        return response.Ok(dto, "Success");
    }

    public async Task<ResponseModel<TransactionResponse>> CreateAsync(TransactionRequest request, Guid userId)
    {
        var response = new ResponseModel<TransactionResponse>();
        var transaction = TransactionMapper.ToDomain(request);

        var user = await userRepository.GetByIdAsync(userId);
        if (user == null)
            return response.Fail(null, "Owner not found!");

        transaction.OwnerId = userId;
        transaction.WalletId = user.WalletId;
        transaction.Wallet = user.Wallet;

        if (request.PayerReceiverId == null && request.GoalId == null)
            return response.Fail(null, "You have to select for which purpose this transaction is for.");

        if (request.GoalId != null && request.PayerReceiverId == null)
        {
            var goal = await goalRepository.GetByIdAsync(request.GoalId);
            if (goal == null)
                return response.Fail(null, "Goal not found!");

            transaction.GoalId = goal.Id;
            transaction.Goal = goal;
        }

        if (request.PayerReceiverId != null && request.GoalId == null)
        {
            var payerReceiver = await payerReceiverRepository.GetByIdAsync(request.PayerReceiverId!.Value);
            if (payerReceiver == null || payerReceiver.OwnerId != userId)
                return response.Fail(null, "PayerReceiver not found or does not belong to the user!");

            transaction.PayerReceiverId = request.PayerReceiverId;
            transaction.PayerReceiver = payerReceiver;
        }

        Decimal sum = 0;

        foreach (var payment in transaction.Payments)
        {
            if (payment.PaymentType.Equals(PaymentType.CREDIT) || payment.PaymentType == PaymentType.DEBIT)
            {
                if (payment.Installments <= 0)
                    return response.Fail(null, "Installments must be equal or greater than 0!");
                if (payment.Installments != 0 && payment.PaymentType == PaymentType.DEBIT)
                    return response.Fail(null, "Debit payments cannot have installments!");
                if (payment.CardId == null)
                    return response.Fail(null, "Please select a card for this payment type!");

                var card = await cardRepository.GetByIdAsync(payment.CardId.Value, userId);
                if (card == null || card.OwnerId != userId)
                    return response.Fail(null, "Card not found or does not belong to the user!");

                if (!((card.CardType == CardType.DEBIT && payment.PaymentType == PaymentType.DEBIT) ||
                      (card.CardType == CardType.CREDIT && payment.PaymentType == PaymentType.CREDIT) ||
                      (card.CardType == CardType.CREDIT_AND_DEBIT && (payment.PaymentType == PaymentType.CREDIT ||
                                                                      payment.PaymentType == PaymentType.DEBIT))))
                {
                    return response.Fail(null, "Card type does not match the payment type!");
                }

                payment.Card = card;
                card.Transactions?.Add(transaction);
                payment.ValuePerInstallment = payment.Amount / payment.Installments;
            }

            // Other payments types
            if (payment.Installments != 0)
            {
                payment.Installments = 0;
                payment.ValuePerInstallment = 0;
                payment.CardId = null;
                payment.Card = null;
                return response.Fail(null, "Installments cannot be greater than 0!");
            }

            if (payment.Amount > request.Amount)
                return response.Fail(null, "Payment amount cannot be greater than transaction amount!");

            payment.OwnerId = userId;
            sum += payment.Amount;
        }

        if (sum != request.Amount)
            return response.Fail(null, "Payments sum does not match the transaction amount!");

        // Wallet & Goals calculation
        if (transaction.TransactionType == TransactionType.RECEIVE && transaction.PayerReceiver != null &&
            transaction.Goal == null)
        {
            user.Wallet!.Balance += request.Amount;
        }
        else
        {
            user.Wallet!.Balance -= request.Amount;
        }

        if (transaction.GoalId != null)
        {
            var goal = await goalRepository.GetByIdAsync(transaction.GoalId.Value);
            if (goal == null || goal.OwnerId != userId)
                return response.Fail(null, "Goal not found or does not belong to the user!");
            goal.Balance = user.Wallet!.Balance;
        }

        await transactionRepository.CreateAsync(transaction);
        var dto = TransactionMapper.ToResponse(transaction);
        return response.Ok(dto, "Transaction created successfully!");
    }

    public async Task<ResponseModel<TransactionResponse>> UpdateAsync(Guid id, Guid userId, TransactionUpdate update)
    {
        var response = new ResponseModel<TransactionResponse>();
        var transaction = await transactionRepository.GetByIdAsync(id);

        if (transaction == null || transaction.OwnerId != userId)
            return response.Fail(null, "Transaction not found or does not belong to the user!");

        if (update.Payments != null && update.Payments?.Count != 0)
        {
            foreach (var payment in update.Payments!)
            {
                if (payment.Id == null)
                    return response.Fail(null, "Payment ID cannot be null!");
                
                var p = await paymentRepository.GetByIdAsync(payment.Id.Value, userId);
                if (p == null || p.OwnerId != userId)
                    return response.Fail(null, "Payment not found or does not belong to the user!");
                
                var updatedPayment = PaymentMapper.ToUpdate(p!, payment);
            }
        }

        var updatedTransaction = TransactionMapper.ToUpdate(transaction, update);
        await transactionRepository.UpdateAsync(id, updatedTransaction);
        var dto = TransactionMapper.ToResponse(updatedTransaction);
        return response.Ok(dto, "Transaction updated successfully!");
    }

    public async Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var transaction = await transactionRepository.GetByIdAsync(id);
        if (transaction == null || transaction.OwnerId != userId)
            return response.Fail(false, "Transaction not found or does not belong to the user!");

        transaction.Status = Status.INACTIVE;
        await transactionRepository.SoftDeleteAsync(transaction);
        return response.Ok(true, "Soft-deleted successfully")!;
    }

    public async Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var transaction = await transactionRepository.GetByIdAsync(id);
        if (transaction == null || transaction.OwnerId != userId)
            return response.Fail(false, "Transaction not found or does not belong to the user!");

        await transactionRepository.HardDeleteAsync(transaction);
        return response.Ok(true, "Hard-deleted successfully")!;
    }
}