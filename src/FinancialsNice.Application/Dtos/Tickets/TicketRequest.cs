using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Tickets;

public record TicketRequest(
    [Required, StringLength(100)] string Name,
    [Required, EmailAddress, StringLength(100)] string Email,
    [Required, StringLength(200)] string Subject,
    [Required, StringLength(2000)] string Message,
    [property: JsonConverter(typeof(JsonStringEnumConverter))] TicketType Type
);
