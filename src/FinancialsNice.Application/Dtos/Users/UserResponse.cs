using System.Text.Json.Serialization;
using FinancialsNice.Application.Dtos.Addresses;
using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Application.Dtos.Goals;
using FinancialsNice.Application.Dtos.Roles;
using FinancialsNice.Application.Dtos.Wallets;
using FinancialsNice.Domain.Enums;

namespace FinancialsNice.Application.Dtos.Users
{
    public record UserResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } =  string.Empty;
        public DateOnly? BirthDate { get; init; }
        public string Email { get; init; } =  string.Empty;
        public string? Phone { get; init; }
        public string? ImgUrl { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime ModifiedAt { get; init; }
        public AddressResponse? Address { get; init; }
        public ICollection<CardResponse>? Cards { get; init; }
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType? Type { get; init; }
        
        public WalletResponse Wallet { get; init; } = null!;
        public ICollection<GoalResponse>? Goals { get; init; } = new List<GoalResponse>();
        public ICollection<RoleResponse> Roles { get; init; } = new List<RoleResponse>();
    }
}