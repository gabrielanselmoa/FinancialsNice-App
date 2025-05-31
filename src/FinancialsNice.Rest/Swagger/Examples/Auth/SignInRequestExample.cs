using FinancialsNice.Application.Dtos.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Auth;

public class SignInRequestExample : IExamplesProvider<SignInRequest>
{
    public SignInRequest GetExamples()
    {
        return new SignInRequest()
        {
            Email = "joao@gmail.com",
            Password = "12345678"
        };
    }
}