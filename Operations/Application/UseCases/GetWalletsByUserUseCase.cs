using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases
{
    public class GetWalletsByUserUseCase
    {
        private readonly IWalletRepository _walletRepository;
        public GetWalletsByUserUseCase(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<IEnumerable<Wallet>> Execute(User user)
        {
            IEnumerable<Wallet> wallets = await this._walletRepository.GetByUser(user);
            return wallets;
        }
    }
}
