using FinancialsNice.Application.Dtos.Users;
using FinancialsNice.Domain.Enums;
using FinancialsNice.Rest.Swagger.Examples.Address;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.User;

public class UserRequestExample : IExamplesProvider<UserUpdate>
{
    public UserUpdate GetExamples()
    {
        return new UserUpdate(
            Name: "Nome do Usuário Atualizado",
            Email: "usuario.atualizado@email.com",
            BirthDate: new DateOnly(1990, 5, 15),
            Phone: "+55 11 98765-4321",
            ImgUrl: "https://example.com/user_updated.png",
            Address: new AddressUpdateExample().GetExamples(),
            Type: UserType.PERSON
        );
    }
}