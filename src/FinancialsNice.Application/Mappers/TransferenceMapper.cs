using FinancialsNice.Application.Dtos.Transferences;
using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Application.Mappers;

public static class TransferenceMapper
{
    public static Transference ToDomain(TransferenceRequest request)
    {
        return new Transference()
        {
            Amount = request.Amount,
            Description = request.Description,
            Currency = request.Currency
        };
    }
    
    public static TransferenceResponse ToResponse(Transference transference)
    {
        return new TransferenceResponse()
        {
            Id = transference.Id,
            Amount = transference.Amount,
            Description = transference.Description,
            Currency = transference.Currency,
            SentAt = transference.SentAt
        };
    }
}