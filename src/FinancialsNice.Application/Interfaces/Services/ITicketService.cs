using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Dtos.Tickets;

namespace FinancialsNice.Application.Interfaces.Services;

public interface ITicketService
{
    Task<PagedResponseModel<ICollection<TicketResponse>>> GetAllAsync(int page, int perPage, Guid userId);
    Task<ResponseModel<TicketResponse?>> GetByIdAsync(Guid id, Guid userId);
    Task<ResponseModel<TicketResponse?>> CreateAsync(TicketRequest request, Guid userId);
    Task<ResponseModel<TicketResponse?>> UpdateAsync(Guid id, Guid userId, TicketUpdate update);
    Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId);
    Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId);
}
