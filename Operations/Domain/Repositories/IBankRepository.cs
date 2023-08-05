using Domain.Entities;

namespace Domain.Repositories
{
    public interface IBankRepository
    {
        public Task<Bank> GetById(Guid id);
        public Task<Bank> GetByIspb(string ispb);
    }
}
