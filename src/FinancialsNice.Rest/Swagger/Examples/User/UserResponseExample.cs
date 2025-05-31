using FinancialsNice.Application.Dtos.Cards;
using FinancialsNice.Application.Dtos.Roles;
using FinancialsNice.Application.Dtos.Users;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Rest.Swagger.Examples.Address;
using FinancialsNice.Rest.Swagger.Examples.Card;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.User;

public class UserResponseExample : IExamplesProvider<UserResponse>
{
    public UserResponse GetExamples()
    {
        return new UserResponse
        {
            Id = Guid.Parse("f0e9d8c7-b6a5-4321-fedc-ba9876543210"),
            Name = "Nome do Usuário",
            BirthDate = new DateOnly(1990, 5, 15),
            Email = "usuario.exemplo@email.com",
            Phone = "+55 11 98765-4321",
            ImgUrl = "https://example.com/user.png",
            CreatedAt = DateTime.UtcNow.AddMonths(-6),
            ModifiedAt = DateTime.UtcNow.AddDays(-1),
            Address = new AddressResponseExample().GetExamples(),
            Cards = new List<CardResponse>
            {
                new CardResponseExample().GetExamples()
            },
            Type = UserType.PERSON,
            Roles = new List<RoleResponse>
            {
                new RoleResponse { Id = 1, Name = "Owner" },
                new RoleResponse { Id = 2, Name = "Admin" }
            }
        };
    }
}