using System.ComponentModel.DataAnnotations;

namespace FinancialsNice.Application.Dtos.Wallets;

public record WalletUpdate(
    Guid? Id,
    [Range(0.0, double.MaxValue)] decimal? Balance
);
