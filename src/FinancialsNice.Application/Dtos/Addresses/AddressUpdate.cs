using System.ComponentModel.DataAnnotations;

namespace FinancialsNice.Application.Dtos.Addresses
{
    public record AddressUpdate(
        Guid? Id,
        [StringLength(100)] string? Street,
        [StringLength(100)] string? City,
        [StringLength(50)] string? State,
        [StringLength(20)] string? ZipCode,
        [StringLength(100)] string? Neighborhood,
        [StringLength(100)] string? Complement
    );
}