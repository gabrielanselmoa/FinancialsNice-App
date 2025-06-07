using FinancialsNice.Application.Dtos.Wallets;
using FinancialsNice.Domain.Design_Pattern;

namespace FinancialsNice.Application.Interfaces.Services;

public interface IWalletService
{
    Task<ResponseModel<WalletResponse>> GetAsync(Guid userId);
}
