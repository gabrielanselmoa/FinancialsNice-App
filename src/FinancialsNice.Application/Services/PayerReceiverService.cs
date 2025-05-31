using FinancialsNice.Application.Dtos.PayerReceivers;
using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class PayerReceiverService(IPayerReceiverRepository payerReceiverRepository) : IPayerReceiverService
{
    public async Task<PagedResponseModel<ICollection<PayerReceiverResponse>>> GetAllBySearchAsync(string? search,
        UserType? type, int page, int perPage, Guid userId)
    {
        var response = new PagedResponseModel<ICollection<PayerReceiverResponse>>();

        var payerReceivers = await payerReceiverRepository.GetAllBySearchAsync(search, type, userId);

        var totalItems = payerReceivers.Count();
        var totalPages = (int)Math.Ceiling((double)payerReceivers.Count() / perPage);

        if (!payerReceivers.Any())
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
            response.Message = "No PayerReceivers were found!";
            response.Success = true;
            return response;
        }

        var dto = payerReceivers.Select(PayerReceiverMapper.ToResponse).ToList();
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
    
    public async Task<ResponseModel<PayerReceiverResponse?>> GetByIdAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<PayerReceiverResponse?>();
        var payerReceiver = await payerReceiverRepository.GetByIdAsync(id);

        if (payerReceiver == null || payerReceiver.OwnerId != userId)
            return response.Fail(null, "PayerReceiver not found or does not belong to the user!");

        var dto = PayerReceiverMapper.ToResponse(payerReceiver);
        return response.Ok(dto, "PayerReceiver retrieved successfully.")!;
    }

    public async Task<ResponseModel<PayerReceiverResponse>> CreateAsync(PayerReceiverRequest request, Guid userId)
    {
        var response = new ResponseModel<PayerReceiverResponse>();
        var payerReceiverExistent = await payerReceiverRepository.GetByNameAsync(request.Name);

        if (payerReceiverExistent != null && payerReceiverExistent.OwnerId == userId)
            return response.Fail(null, "PayerReceiver already exists!");

        var payerReceiver = PayerReceiverMapper.ToDomain(request);
        payerReceiver.OwnerId = userId;
        
        await payerReceiverRepository.CreateAsync(payerReceiver);
        var dto = PayerReceiverMapper.ToResponse(payerReceiver);
        return response.Ok(dto, "PayerReceiver created successfully.")!;
    }

    public async Task<ResponseModel<PayerReceiverResponse>> UpdateAsync(Guid id, Guid userId,
        PayerReceiverUpdate update)
    {
        var response = new ResponseModel<PayerReceiverResponse>();
        var payerReceiver = await payerReceiverRepository.GetByIdAsync(id);

        if (payerReceiver == null || payerReceiver.OwnerId != userId)
            return response.Fail(null, "PayerReceiver not found or does not belong to the user!");

        var updated = PayerReceiverMapper.ToUpdate(payerReceiver, update);
        await payerReceiverRepository.UpdateAsync(id, updated);
        
        var dto = PayerReceiverMapper.ToResponse(updated);
        return response.Ok(dto, "PayerReceiver updated successfully.")!;
    }

    public async Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var payerReceiver = await payerReceiverRepository.GetByIdAsync(id);

        if (payerReceiver == null || payerReceiver.OwnerId != userId) 
            return response.Fail(false, "PayerReceiver not found or does not belong to the user!");

        payerReceiver.Status = Status.INACTIVE;
        await payerReceiverRepository.SoftDeleteAsync(payerReceiver);
        return response.Ok(true, "Soft-deleted successfully")!;
    }

    public async Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var payerReceiver = await payerReceiverRepository.GetByIdAsync(id);

        if (payerReceiver == null || payerReceiver.OwnerId != userId) 
            return response.Fail(false, "PayerReceiver not found or does not belong to the user!");

        await payerReceiverRepository.HardDeleteAsync(payerReceiver);
        return response.Ok(true, "Hard-deleted successfully")!;
    }
}