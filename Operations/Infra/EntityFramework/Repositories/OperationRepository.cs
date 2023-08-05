using Domain.Entities;
using Domain.Repositories;

namespace Infra.EntityFramework.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OperationRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Task Create(Operation operation)
        {
            throw new NotImplementedException();
        }

        public Task<Operation> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Operation>> GetOperationsByUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
