namespace FinancialsNice.Application.Utils;

public static class EmailMessages
{
    public static string EmailConfirmationMessage => @"
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
    public static string WelcomeMessage => @"
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
    public static string ForgotPasswordMessage =>  @"
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
    
}