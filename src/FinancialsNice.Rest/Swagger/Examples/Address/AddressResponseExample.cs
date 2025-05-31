using FinancialsNice.Application.Dtos.Addresses;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Address;

public class AddressResponseExample : IExamplesProvider<AddressResponse>
{
    public AddressResponse GetExamples()
    {
        return new AddressResponse
        {
            Id = Guid.Parse("a1b2c3d4-e5f6-7890-1234-567890abcdef"),
            Street = "Rua Exemplo, 123",
            City = "Cidade Exemplo",
            State = "EX",
            ZipCode = "12345-678",
            Neighborhood = "Bairro Exemplo",
            Complement = "Apto 101"
        };
    }
}