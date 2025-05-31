using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Dtos.Tickets;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class TicketService (ITicketRepository repository, IUserRepository userRepository) : ITicketService
{
    public async Task<PagedResponseModel<ICollection<TicketResponse>>> GetAllAsync(int page, int perPage, Guid userId)
    {
       var response = new PagedResponseModel<ICollection<TicketResponse>>();
       var tickets = await repository.GetAllAsync(userId);
       if (!tickets.Any())
       {
           response.Data = [];
           response.Message = "No Tickets were found!";
           response.Success = true;
           return response;
       }
       var dto = tickets.Select(TicketMapper.ToResponse).ToList();

       var totalItems = tickets.Count;
       var totalPages = (int)Math.Ceiling((double)totalItems / perPage);

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

    public async Task<ResponseModel<TicketResponse?>> GetByIdAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<TicketResponse>();
        var ticket = await repository.GetByIdAsync(id, userId);
        if (ticket == null)
            return response.Fail(null, "Ticket not found!")!;
        
        var dto = TicketMapper.ToResponse(ticket);
        return response.Ok(dto, "Ticket retrieved successfully.")!;
    }

    public async Task<ResponseModel<TicketResponse?>> CreateAsync(TicketRequest request, Guid userId)
    {
        var response = new ResponseModel<TicketResponse?>();
        var user = await userRepository.GetByIdAsync(userId);
        var ticketDomain = TicketMapper.ToDomain(request);
        ticketDomain.Client = user;
        ticketDomain.ClientId = userId;
        
        await repository.CreateAsync(ticketDomain);
        var dto = TicketMapper.ToResponse(ticketDomain);
        return response.Ok(dto, "Ticket created successfully.")!;
    }

    public async Task<ResponseModel<TicketResponse?>> UpdateAsync(Guid id, Guid userId, TicketUpdate update)
    {
        var response = new ResponseModel<TicketResponse>();
        var ticketDomain = await repository.GetByIdAsync(id, userId);
        if (ticketDomain == null) return response.Fail(null, "Ticket not found!")!;
        
        var ticketUpdated = TicketMapper.ToUpdate(ticketDomain, update);
        var dto = TicketMapper.ToResponse(ticketUpdated);
        await repository.UpdateAsync(ticketUpdated);
        return response.Ok(dto, "Ticket updated successfully.")!;
    }

    public async Task<ResponseModel<bool>> SoftDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var ticket = await repository.GetByIdAsync(id, userId);
        if (ticket == null || ticket.ClientId != userId)
            return response.Fail(false, "Ticket not found or does not belong to the user!");

        ticket.IsResolved = true;
        await repository.SoftDeleteAsync(ticket);
        return response.Ok(true, "Closed Successfully");
    }

    public async Task<ResponseModel<bool>> HardDeleteAsync(Guid id, Guid userId)
    {
        var response = new ResponseModel<bool>();
        var ticket = await repository.GetByIdAsync(id, userId);
        if (ticket == null || ticket.ClientId != userId)
            return response.Fail(false, "Ticket not found or does not belong to the user!");

        await repository.HardDeleteAsync(ticket);
        return response.Ok(true, "Deleted Successfully");
    }
}