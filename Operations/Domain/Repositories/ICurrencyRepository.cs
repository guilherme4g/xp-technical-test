using Domain.Entities;

namespace Domain.Repositories
{
    public interface ICurrencyRepository
    {
        public Task<Currency> GetById(Guid id);
    }
}
