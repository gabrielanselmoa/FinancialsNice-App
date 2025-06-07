using System.ComponentModel.DataAnnotations;

namespace FinancialsNice.Application.Dtos.Transferences;

public record TransferenceRequest(
    [Required, Range(0.01, double.MaxValue)] decimal Amount,
    [StringLength(250)] string? Description,
    [StringLength(3)] string Currency
);