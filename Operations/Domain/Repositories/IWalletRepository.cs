using Domain.Entities;

namespace Domain.Repositories
{
    public interface IWalletRepository
    {
        public Task<Wallet> GetById(Guid id);
        public Task<IEnumerable<Wallet>> GetByUser(User user);
        public Task<Wallet> GetByUserAndCurrency(User user, Currency currency);

        public Task UpdateBalance(Guid walletId, int balance);
    }
}
