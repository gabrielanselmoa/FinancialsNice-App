using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Services;
using FinancialsNice.Domain.Interfaces.Repositories;
using FinancialsNice.Infrastructure.Repositories;
using FinancialsNice.Infrastructure.Security.Services;

namespace FinancialsNice.Rest.Extensions;

public static class IocExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IPayerReceiverRepository, PayerReceiverRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        services.AddScoped<IWalletRepository, WalletRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ITransferenceRepository, TransferenceRepository>();
        
        return services;
    }  
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IPayerReceiverService, PayerReceiverService>();
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMinioService, MinioService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IEmailSenderService, EmailService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IGoalService, GoalService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<IPdfGenerator, PdfGeneratorService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<ITransferenceService, TransferenceService>();
        
        return services;
    }
}