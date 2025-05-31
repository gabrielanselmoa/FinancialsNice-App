using FinancialsNice.Application.Dtos.Auth;
using FinancialsNice.Application.Dtos.Emails;
using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Domain.Entities;
using FinancialsNice.Domain.Interfaces.Repositories;
using EmailRequest = FinancialsNice.Application.Dtos.Auth.EmailRequest;

namespace FinancialsNice.Application.Services;

public class AuthService(
    ISecurityService securityService,
    IUserRepository userRepository,
    IEmailSenderService emailSenderService,
    IRoleRepository roleRepository)
    : IAuthService
{
    private async Task<bool> InternalEmailConfirmation(User user)
    {
        // Generation short expiring jwt token
        var token = securityService.GenerateJwtToken(user, 10, "min");

        // Defining dynamic variables
        string userName = user.Name!;
        string confirmationLink = $"http://localhost:3000/email-confirmation/{token}";

        // Default Message
        var defaultMessage = @"
<div style=""max-width: 600px; margin: 40px auto; background-color: #ffffff; padding: 30px; border-radius: 10px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1); font-family: Arial, sans-serif;"">
  <h2 style=""text-align: center; color: #2c3e50;"">📧 Confirmação de E-mail</h2>

  <p style=""color: #34495e; font-size: 16px;"">Olá, <strong>{{UserName}}</strong>,</p>

  <p style=""color: #34495e; font-size: 16px;"">
    Para ativar sua conta FinNice, confirme seu e-mail clicando no botão abaixo:
  </p>

  <div style=""text-align: center; margin: 30px 0;"">
    <a href=""{{ConfirmationLink}}"" style=""display: inline-block; padding: 14px 28px; background-color: #27ae60; color: #ffffff; text-decoration: none; border-radius: 6px; font-weight: bold; font-size: 16px;"">
      Confirmar E-mail
    </a>
  </div>

  <p style=""color: #34495e; font-size: 16px;"">
    Se você não criou uma conta na FinNice, pode ignorar este e-mail com segurança.
  </p>

  <p style=""color: #34495e; font-size: 16px; margin-top: 30px;"">
    Atenciosamente,<br><strong>Equipe FinNice</strong>
  </p>

  <p style=""font-size: 12px; color: #999999; text-align: center; margin-top: 40px;"">
    Este é um e-mail automático. Por favor, não responda a esta mensagem.
  </p>
</div>";

        string finalMessage = defaultMessage
            .Replace("{{UserName}}", userName)
            .Replace("{{ConfirmationLink}}", confirmationLink);

        // Triggering Email Sender
        var emailRequest = new EmailSenderRequest(user.Email, "Email Confirmation", finalMessage);
        var result = await emailSenderService.SendEmailAsync(emailRequest);
        if (!result.Success) return false;
        return true;
    }

    private async Task<bool> InternalWelcomeUser(User user)
    {
        // Defining dynamic variables
        string userName = user.Name!;
        string platformLink = $"http://localhost:3000";

        // Default Message
        var welcomeMessage = @"
<div style=""max-width: 600px; margin: 40px auto; background-color: #ffffff; padding: 30px; border-radius: 10px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1); font-family: Arial, sans-serif;"">
  <h2 style=""text-align: center; color: #2c3e50;"">🎉 Bem-vindo à FinNice!</h2>

  <p style=""color: #34495e; font-size: 16px;"">Olá <strong>{{UserName}}</strong>,</p>

  <p style=""color: #34495e; font-size: 16px;"">
    É um prazer ter você com a gente! A partir de agora, você faz parte de uma plataforma que busca transformar a forma como você gerencia suas finanças.
  </p>

  <p style=""color: #34495e; font-size: 16px;"">
    Explore recursos inteligentes, acompanhe seus gastos, organize suas metas e aproveite uma experiência feita para facilitar o seu dia a dia financeiro.
  </p>

  <div style=""text-align: center; margin: 30px 0;"">
    <a href=""{{PlatformLink}}"" style=""display: inline-block; padding: 14px 28px; background-color: #2ecc71; color: #ffffff; text-decoration: none; border-radius: 6px; font-weight: bold; font-size: 16px;"">
      Acessar Plataforma
    </a>
  </div>

  <p style=""color: #34495e; font-size: 16px;"">
    Se precisar de ajuda ou tiver dúvidas, estamos aqui para te apoiar.
  </p>

  <p style=""color: #34495e; font-size: 16px; margin-top: 30px;"">
    Um forte abraço,<br><strong>Equipe FinNice 💚</strong>
  </p>

  <p style=""font-size: 12px; color: #999999; text-align: center; margin-top: 40px;"">
    Este é um e-mail automático. Por favor, não responda esta mensagem.
  </p>
</div>";

        string finalMessage = welcomeMessage
            .Replace("{{UserName}}", userName)
            .Replace("{{PlatformLink}}", platformLink);

        // Triggering Email Sender
        var emailRequest = new EmailSenderRequest(user.Email, "FinNice App Onboard", finalMessage);
        var result = await emailSenderService.SendEmailAsync(emailRequest);
        if (!result.Success) return false;
        return true;
    }

    public async Task<ResponseModel<LoggedResponse>> SignUp(SignUpRequest request)
    {
        var response = new ResponseModel<LoggedResponse>();
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user != null)
        {
            response.Data = null;
            response.Message = "User already exists!";
            response.Success = false;
            return response;
        }

        // Converting to Domain + hashingPassword
        var userDomain = new User()
        {
            Name = request.Name,
            Email = request.Email,
        };

        userDomain.Wallet = new Wallet()
        {
            Name = "Wallet",
            Balance = 0,
            OwnerId = userDomain.Id,
            Owner = userDomain,
        };

        var role = await roleRepository.GetByNameAsync("user-default");
        if (role == null)
        {
            role = await roleRepository.SeedDatabase();
            userDomain.Roles.Add(role);
        }
        else
        {
            userDomain.Roles.Add(role);
        }
        
        userDomain.HashedPassword = securityService.HashPassword(userDomain, request.Password);
        await userRepository.CreateAsync(userDomain);
        var token = securityService.GenerateJwtToken(userDomain, 1, "Days");

        await InternalWelcomeUser(userDomain);
        await InternalEmailConfirmation(userDomain);

        response.Data = new LoggedResponse { Name = userDomain.Name, Email = userDomain.Email, JwToken = token };
        response.Message = "Success";
        response.Success = true;
        return response;
    }

    public async Task<ResponseModel<LoggedResponse>> SignIn(SignInRequest request)
    {
        var response = new ResponseModel<LoggedResponse>();
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            response.Data = null;
            response.Message = "Wrong e-mail or password!";
            response.Success = false;
            return response;
        }

        var isPasswordCorrect = securityService.VerifyPassword(user, request.Password, user.HashedPassword);
        if (!isPasswordCorrect)
        {
            response.Data = null;
            response.Message = "Wrong e-mail or password!";
            response.Success = false;
            return response;
        }

        var token = securityService.GenerateJwtToken(user, 1, "Days");
        response.Data = new LoggedResponse { Name = user.Name!, Email = user.Email, JwToken = token };
        response.Message = "Success";
        response.Success = true;
        return response;
    }

    public async Task<ResponseModel<string>> ForgotPassword(EmailRequest request)
    {
        var response = new ResponseModel<string>();
        var user = userRepository.GetByEmailAsync(request.Email).Result;
        if (user?.Name == null)
        {
            response.Data = null;
            response.Message = "Owner not found!";
            response.Success = false;
            return response;
        }

        // Generation short expiring jwt token
        var token = securityService.GenerateJwtToken(user, 10, "min");

        // Defining dynamic variables
        string userName = user.Name;
        string resetLink = $"http://localhost:3000/auth/reset-password?token={token}";

        // Treating default message
        var defaultMessage = @"
<div style=""max-width: 600px; margin: 40px auto; background-color: #ffffff; padding: 30px; border-radius: 10px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1); font-family: Arial, sans-serif;"">
  <h2 style=""text-align: center; color: #2c3e50;"">🔐 Solicitação de Redefinição de Senha</h2>

  <p style=""color: #34495e; font-size: 16px;"">Olá, <strong>{{UserName}}</strong>,</p>

  <p style=""color: #34495e; font-size: 16px;"">
    Detectamos uma solicitação para redefinir a senha da sua conta FinNice. Se você fez essa solicitação, clique no botão abaixo para continuar com o processo de redefinição de forma segura:
  </p>

  <div style=""text-align: center; margin: 30px 0;"">
    <a href=""{{ResetLink}}"" style=""display: inline-block; padding: 14px 28px; background-color: #3498db; color: #ffffff; text-decoration: none; border-radius: 6px; font-weight: bold; font-size: 16px;"">
      Redefinir minha senha
    </a>
  </div>

  <p style=""color: #34495e; font-size: 16px;"">
    🔒 Este link é válido por <strong>10 minutos</strong> e só poderá ser utilizado uma única vez.
  </p>

  <p style=""color: #34495e; font-size: 16px;"">
    Caso você <strong>não tenha solicitado</strong> essa redefinição, recomendamos que ignore este e-mail com segurança. Nenhuma ação será realizada em sua conta.
  </p>

  <p style=""color: #34495e; font-size: 16px;"">
    Se precisar de ajuda ou tiver qualquer dúvida, nossa equipe está à disposição para te ajudar.
  </p>

  <p style=""color: #34495e; font-size: 16px; margin-top: 30px;"">
    Atenciosamente,<br><strong>Equipe FinNice</strong>
  </p>

  <p style=""font-size: 12px; color: #999999; text-align: center; margin-top: 40px;"">
    Este é um e-mail automático. Por favor, não responda a esta mensagem.
  </p>
</div>";

        string finalMessage = defaultMessage
            .Replace("{{UserName}}", userName)
            .Replace("{{ResetLink}}", resetLink);

        // Triggering Email Sender
        var emailRequest = new EmailSenderRequest(request.Email, "Password Reset", finalMessage);
        var result = await emailSenderService.SendEmailAsync(emailRequest);

        response.Data = "Ok";
        response.Message = result.Message;
        response.Success = result.Success;
        return response;
    }

    public async Task<ResponseModel<string>> ResetPassword(SendPassword sendPassword)
    {
        var response = new ResponseModel<string>();
        if (string.IsNullOrEmpty(sendPassword.Password) || string.IsNullOrEmpty(sendPassword.Token))
        {
            response.Message = "Password or Token are invalid!";
            response.Success = false;
            return response;
        }

        var userId = securityService.GetUserIdFromToken(sendPassword.Token);
        if (!userId.HasValue)
        {
            response.Message = "Owner not authorized!";
            response.Success = false;
            return response;
        }

        var user = await userRepository.GetByIdAsync(userId.Value);
        if (user == null)
        {
            response.Message = "Owner not found!";
            response.Success = false;
            return response;
        }

        var hashedPassword = securityService.HashPassword(user, sendPassword.Password);
        user.HashedPassword = hashedPassword;
        await userRepository.UpdateAsync(userId.Value, user);

        response.Data = "Ok";
        response.Message = "Your password has been updated successfully!";
        response.Success = true;
        return response;
    }

    public async Task<ResponseModel<string>> ConfirmEmail(Guid id)
    {
        var response = new ResponseModel<string>();
        var user = userRepository.GetByIdAsync(id).Result;
        if (user == null)
        {
            response.Message = "Owner not found!";
            response.Success = false;
            return response;
        }

        // Generation short expiring jwt token
        var token = securityService.GenerateJwtToken(user, 10, "min");

        // Defining dynamic variables
        string userName = user.Name!;
        string confirmationLink = $"http://localhost:3000/email-confirmation/{token}";

        // Default Message
        var defaultMessage = @"
<div style=""max-width: 600px; margin: 40px auto; background-color: #ffffff; padding: 30px; border-radius: 10px; box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1); font-family: Arial, sans-serif;"">
  <h2 style=""text-align: center; color: #2c3e50;"">📧 Confirmação de E-mail</h2>

  <p style=""color: #34495e; font-size: 16px;"">Olá, <strong>{{UserName}}</strong>,</p>

  <p style=""color: #34495e; font-size: 16px;"">
    Para ativar sua conta FinNice, confirme seu e-mail clicando no botão abaixo:
  </p>

  <div style=""text-align: center; margin: 30px 0;"">
    <a href=""{{ConfirmationLink}}"" style=""display: inline-block; padding: 14px 28px; background-color: #27ae60; color: #ffffff; text-decoration: none; border-radius: 6px; font-weight: bold; font-size: 16px;"">
      Confirmar E-mail
    </a>
  </div>

  <p style=""color: #34495e; font-size: 16px;"">
    Se você não criou uma conta na FinNice, pode ignorar este e-mail com segurança.
  </p>

  <p style=""color: #34495e; font-size: 16px; margin-top: 30px;"">
    Atenciosamente,<br><strong>Equipe FinNice</strong>
  </p>

  <p style=""font-size: 12px; color: #999999; text-align: center; margin-top: 40px;"">
    Este é um e-mail automático. Por favor, não responda a esta mensagem.
  </p>
</div>";

        string finalMessage = defaultMessage
            .Replace("{{UserName}}", userName)
            .Replace("{{ConfirmationLink}}", confirmationLink);

        // Triggering Email Sender
        var emailRequest = new EmailSenderRequest(user.Email, "Email Confirmation", finalMessage);
        var result = await emailSenderService.SendEmailAsync(emailRequest);

        response.Data = result.Data;
        response.Message = result.Message;
        response.Success = result.Success;
        return response;
    }
}