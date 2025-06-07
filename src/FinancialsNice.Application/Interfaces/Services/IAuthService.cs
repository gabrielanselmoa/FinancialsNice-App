using FinancialsNice.Application.Dtos.Auth;
using FinancialsNice.Domain.Design_Pattern;

namespace FinancialsNice.Application.Interfaces.Services;

public interface IAuthService
{
    Task<ResponseModel<LoggedResponse>> SignUp(SignUpRequest request);
    Task<ResponseModel<LoggedResponse>> SignIn(SignInRequest request);
    Task<ResponseModel<string>> ForgotPassword(EmailRequest request);
    Task<ResponseModel<string>> ResetPassword(SendPassword sendPassword);
    Task<ResponseModel<string>> ConfirmEmail(Guid id);
}