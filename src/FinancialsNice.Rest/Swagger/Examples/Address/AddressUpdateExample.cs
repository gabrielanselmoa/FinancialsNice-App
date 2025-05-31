using FinancialsNice.Application.Dtos.Addresses;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Address;

public class AddressUpdateExample : IExamplesProvider<AddressUpdate>
{
    public AddressUpdate GetExamples()
    {
        return new AddressUpdate
        (
            Id: Guid.Parse("a1b2c3d4-e5f6-7890-1234-567890abcdef"),
            Street: "Rua de Exemplo para Criacao",
            City: "Cidade Nova",
            State: "SP",
            ZipCode: "01000-000",
            Neighborhood: "Centro",
            Complement: "Ao lado da praça"
        );
    }
}