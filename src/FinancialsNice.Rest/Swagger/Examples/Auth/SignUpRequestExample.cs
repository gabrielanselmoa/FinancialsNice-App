using FinancialsNice.Application.Dtos.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Auth;

public class SignUpRequestExample : IExamplesProvider<SignUpRequest>
{
    public SignUpRequest GetExamples()
    {
        return new SignUpRequest()
        {
            Name = "João",
            Email = "joao@gmail.com",
            Password = "12345678"
        };
    }
}