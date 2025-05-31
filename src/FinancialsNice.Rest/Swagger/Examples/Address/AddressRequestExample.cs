using FinancialsNice.Application.Dtos.Addresses;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Address;

public class AddressRequestExample : IExamplesProvider<AddressRequest>
{
    public AddressRequest GetExamples()
    {
        return new AddressRequest
        (
            Street: "Rua de Exemplo para Criacao",
            City: "Cidade Nova",
            State: "MA",
            ZipCode: "02000-000",
            Neighborhood: "Centro",
            Complement: "Ao lado da praça"
        );
    }
}