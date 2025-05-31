using FinancialsNice.Application.Dtos.ResultPattern;
using FinancialsNice.Application.Dtos.Wallets;
using FinancialsNice.Application.Interfaces.Services;
using FinancialsNice.Application.Mappers;
using FinancialsNice.Domain.Interfaces.Repositories;

namespace FinancialsNice.Application.Services;

public class WalletService(IWalletRepository walletRepository)
    : IWalletService
{
    public async Task<ResponseModel<WalletResponse>> GetAsync(Guid userId)
    {
        var response = new ResponseModel<WalletResponse>();
        var wallet = await walletRepository.GetAsync(userId);

        if (string.IsNullOrWhiteSpace(wallet.Name))
            return response.Fail(null, "Wallet not found!");
        
        var dto = WalletMapper.ToResponse(wallet);
        return response.Ok(dto, "Success")!;
    }
}