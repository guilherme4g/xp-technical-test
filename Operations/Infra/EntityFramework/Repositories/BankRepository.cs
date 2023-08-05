using Domain.Entities;
using Domain.Repositories;
using Infra.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra.EntityFramework.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BankRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        static Bank ToDomain(BankModel bank)
        {
            return new Bank(bank.Id, bank.Ispb, bank.Ispb, bank.Active, bank.Blocked);
        }

        public async Task<Bank> GetById(Guid id)
        {
            var bank = await _dbContext.Banks.FindAsync(id);

            if (bank == null) return null;
            

            return ToDomain(bank);
        }

        public async Task<Bank> GetByIspb(string ispb)
        {
            var bankFound = await _dbContext.Banks.Where(bank => bank.Ispb == ispb).FirstAsync();

            if (bankFound == null)
            {
                return null;
            }

            return ToDomain(bankFound);
        }
    }
}
