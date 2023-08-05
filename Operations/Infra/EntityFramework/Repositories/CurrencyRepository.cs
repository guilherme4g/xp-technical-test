using Domain.Entities;
using Domain.Repositories;
using Infra.EntityFramework.Models;

namespace Infra.EntityFramework.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CurrencyRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public static Currency ToDomain(CurrencyModel currency)
        {
            return new Currency(currency.Id, currency.Name, currency.Symbol);
        }

        public async Task<Currency> GetById(Guid id)
        {
            var currency = await _dbContext.Currencies.FindAsync(id);

            if (currency == null) return null;


            return ToDomain(currency);
        }
    }
}
