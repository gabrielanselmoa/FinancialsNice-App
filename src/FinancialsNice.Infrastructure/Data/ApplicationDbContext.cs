using FinancialsNice.Domain.Entities;
using FinancialsNice.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FinancialsNice.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){}

    public DbSet<User> Users { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PayerReceiver> PayerReceivers { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Transference> Transferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // ----------------- APPLY CONFIGURATION ----------------------
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PayerReceiverConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new GoalConfiguration());
        modelBuilder.ApplyConfiguration(new WalletConfiguration());
        modelBuilder.ApplyConfiguration(new TransferenceConfiguration());
    }
}