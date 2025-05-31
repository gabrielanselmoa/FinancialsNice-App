using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Dtos.Wallets;

namespace FinancialsNice.Application.Interfaces.Services;

public interface IWalletService
{
    Task<ResponseModel<WalletResponse>> GetAsync(Guid userId);
}
