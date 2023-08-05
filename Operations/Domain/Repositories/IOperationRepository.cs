using Domain.Entities;

namespace Domain.Repositories
{
    public interface IOperationRepository
    {
        public Task Create(Operation operation);
        public Task<IEnumerable<Operation>> GetOperationsByUser(User user);
        public Task<Operation> GetById(Guid id);
        
    }
}
