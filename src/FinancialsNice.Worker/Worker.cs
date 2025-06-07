using FinancialsNice.Domain.Enums;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ITransactionRepository _transactionRepository;

    public Worker(ILogger<Worker> logger, ITransactionRepository transactionRepository)
    {
        _logger = logger;
        _transactionRepository = transactionRepository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var transactions = await _transactionRepository.GetAllAsync();
            foreach (var t in transactions)
            {
                if (DateOnly.FromDateTime(DateTime.Now) > t.ScheduledAt &&
                    t.TransactionStatus == TransactionStatus.PENDING)
                {
                    t.TransactionStatus = TransactionStatus.COMPLETED;
                    await _transactionRepository.UpdateAsync(t.Id, t);
                    _logger.LogInformation("The transaction with ID {TransactionId} is currently marked as {Status}",
                        t.Id,
                        t.TransactionStatus);
                }
            }
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}