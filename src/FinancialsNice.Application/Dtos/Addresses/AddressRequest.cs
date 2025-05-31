using System.ComponentModel.DataAnnotations;

namespace FinancialsNice.Application.Dtos.Addresses
{
    public record AddressRequest(
        [Required, StringLength(100)] string Street,
        [Required, StringLength(100)] string City,
        [Required, StringLength(50)] string State,
        [Required, StringLength(20)] string ZipCode,
        [Required, StringLength(100)] string Neighborhood,
        [StringLength(100)] string? Complement
    );
}