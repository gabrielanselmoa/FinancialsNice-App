using FinancialsNice.Application.Dtos.Payments;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Design_Pattern;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class PaymentService(IPaymentRepository paymentRepository) : IPaymentService
{
    public async Task<PagedResponseModel<ICollection<PaymentResponse>>> GetAllAsync(int page, int perPage, Guid userId)
    {
        var response = new PagedResponseModel<ICollection<PaymentResponse>>();
        var payments = await paymentRepository.GetAllAsync(userId);

        var totalItems = payments.Count();
        var totalPages = (int)Math.Ceiling((double)payments.Count() / perPage);

        if (!payments.Any())
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
            response.Message = "No payments were found!";
            response.Success = true;
            return response;
        }

        var dto = payments.Select(PaymentMapper.ToResponse).ToList();

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

    public async Task<ResponseModel<PaymentResponse?>> GetByIdAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<PaymentResponse?>();
        var payment = await paymentRepository.GetByIdAsync(id, userId);

        if (payment == null || payment.OwnerId != userId)
            return response.Fail(null, "Payment not found or does not belong to the user!");

        var dto = PaymentMapper.ToResponse(payment);
        return response.Ok(dto, "Payment retrieved successfully.")!;
    }

    public async Task<ResponseModel<PaymentResponse>> CreateAsync(PaymentRequest request, Guid userId)
    {
        var response = new ResponseModel<PaymentResponse>();
        var payment = PaymentMapper.ToDomain(request);
        payment.OwnerId = userId;

        await paymentRepository.CreateAsync(payment);
        var dto = PaymentMapper.ToResponse(payment);
        return response.Ok(dto, "Payment created successfully.")!;
    }

    public async Task<ResponseModel<PaymentResponse>> UpdateAsync(Guid id, Guid userId, PaymentUpdate update)
    {
        var response = new ResponseModel<PaymentResponse>();
        var payment = await paymentRepository.GetByIdAsync(id, userId);

        if (payment == null || payment.OwnerId != userId)
            return response.Fail(null, "Payment not found or does not belong to the user!");

        var updated = PaymentMapper.ToUpdate(payment, update);
        await paymentRepository.UpdateAsync(updated);
        
        var dto = PaymentMapper.ToResponse(updated);
        return response.Ok(dto, "Payment updated successfully.")!;
    }

    public async Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var payment = await paymentRepository.GetByIdAsync(id, userId);
        if (payment == null || payment.OwnerId != userId)
            return response.Fail(false, "Payment not found or does not belong to the user!");

        payment.Status = Status.INACTIVE;
        await paymentRepository.SoftDeleteAsync(payment);
        return response.Ok(true, "Soft-deleted successfully")!;
    }

    public async Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var payment = await paymentRepository.GetByIdAsync(id, userId);

        if (payment == null || payment.OwnerId != userId)
            return response.Fail(false, "Payment not found or does not belong to the user!");

        await paymentRepository.HardDeleteAsync(payment);
        return response.Ok(true, "Hard-deleted successfully")!;
    }
}