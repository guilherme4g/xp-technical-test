using Azure;
using Domain.Entities;
using Domain.Repositories;
using Infra.EntityFramework.Models;
using Interface.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Infra.EntityFramework.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WalletRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public static Wallet ToDomain(WalletModel wallet)
        {
            var user = new User(wallet.UserId);
            var currency = CurrencyRepository.ToDomain(wallet.Currency);

            return new Wallet(wallet.Id, wallet.Name, user, currency, wallet.Balance, wallet.Standart, wallet.Active);
        }

        public async Task<Wallet> GetById(Guid id)
        {
            var wallet = await _dbContext.Wallets.Include(wallet => wallet.Currency).FirstAsync(wallet => wallet.Id == id);

            if (wallet == null) return null;


            return ToDomain(wallet);
        }

        public async Task<IEnumerable<Wallet>> GetByUser(User user)
        {
            var walletsFound = await _dbContext.Wallets.Include(wallet => wallet.Currency).Where(wallet => wallet.UserId == user.Id).ToListAsync();

            List<Wallet> wallets = new List<Wallet>();

            foreach (var wallet in walletsFound)
            {
                wallets.Add(ToDomain(wallet));
            }

            return wallets;
        }

        public async Task<Wallet> GetByUserAndCurrency(User user, Currency currency)
        {
            var wallet = await _dbContext.Wallets.Include(wallet => wallet.Currency).FirstAsync(wallet => (wallet.UserId == user.Id && wallet.CurrencyId == currency.Id));

            if (wallet == null) return null;

            return ToDomain(wallet);
        }

        public async Task UpdateBalance(Guid id, int balance)
        {
            var wallet = await _dbContext.Wallets.Include(wallet => wallet.Currency).FirstAsync(wallet => wallet.Id == id);

            if (wallet == null) return;

            wallet.Balance = balance;

            await _dbContext.SaveChangesAsync();
        }
    }
}
