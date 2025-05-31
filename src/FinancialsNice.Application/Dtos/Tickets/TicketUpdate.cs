using System.ComponentModel.DataAnnotations;
using FinancialsNice.Application.Dtos.Users;

namespace FinancialsNice.Application.Dtos.Tickets;

public record TicketUpdate(
    Guid? Id,
    [StringLength(100)] string? Name,
    [EmailAddress, StringLength(100)] string? Email,
    [StringLength(200)] string? Subject,
    [StringLength(2000)] string? Message,
    bool? IsResolved,
    DateTime? ResolvedAt
);
