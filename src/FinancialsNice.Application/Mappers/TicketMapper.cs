using FinancialsNice.Application.Dtos.Tickets;
using FinancialsNice.Application.Helpers;
using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Application.Mappers;

public static class TicketMapper
{
    public static TicketResponse ToResponse(Ticket ticket)
    {
        return new TicketResponse
        {
            Id = ticket.Id,
            Code = ticket.Code,
            Email = ticket.Email,
            Name = ticket.Name,
            Subject = ticket.Subject,
            Message = ticket.Message,
            Type = ticket.Type,
            Client = UserMapper.ToUserPreview(ticket.Client),
            CreatedAt = ticket.CreatedAt,
            IsResolved = ticket.IsResolved,
            ResolvedAt = ticket.ResolvedAt,
            ResolvedBy = ticket.ResolvedBy == null ? null : UserMapper.ToUserPreview(ticket.ResolvedBy),
        };
    }

    public static Ticket ToDomain(TicketRequest request)
    {
        return new Ticket
        {
            Name = request.Name,
            Email = request.Email,
            Subject = request.Subject,
            Message = request.Message,
            Type = request.Type,
            Code = CodeGenerator.GenerateTicketCode(),
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow
        };
    }

    public static Ticket ToUpdate(Ticket ticket, TicketUpdate update)
    {
        if (!string.IsNullOrWhiteSpace(update.Name) && update.Name != ticket.Name)
            ticket.Name = update.Name;
        if (!string.IsNullOrWhiteSpace(update.Email) && update.Email != ticket.Email)
            ticket.Email = update.Email;
        if (!string.IsNullOrWhiteSpace(update.Subject) && update.Subject != ticket.Subject)
            ticket.Subject = update.Subject;
        if (!string.IsNullOrWhiteSpace(update.Message) && update.Message != ticket.Message)
            ticket.Message = update.Message;
        if (update.IsResolved != null && update.IsResolved != ticket.IsResolved)
            ticket.IsResolved = (bool)update.IsResolved;
        
        ticket.ModifiedAt = DateTime.UtcNow;
        return ticket;
    }
}