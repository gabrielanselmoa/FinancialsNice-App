using FinancialsNice.Application.Dtos.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace FinancialsNice.Rest.Swagger.Examples.Auth;

public class AuthResponseExample : IExamplesProvider<LoggedResponse>
{
    public LoggedResponse GetExamples()
    {
        return new LoggedResponse()
        {
            Name = "João",
            Email = "joao@gmail.com",
            JwToken =
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
        };
    }
}