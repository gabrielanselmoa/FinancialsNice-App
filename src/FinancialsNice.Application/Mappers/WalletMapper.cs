using FinancialsNice.Application.Dtos.Wallets;
using FinancialsNice.Domain.Entities;

namespace FinancialsNice.Application.Mappers;

public static class WalletMapper
{
    public static WalletResponse ToResponse(Wallet wallet)
    {
        return new WalletResponse
        {
            Id = wallet.Id,
            Name = wallet.Name,
            Balance = wallet.Balance,
            CreatedAt = wallet.CreatedAt,
            ModifiedAt = wallet.ModifiedAt,
        };
    }
}