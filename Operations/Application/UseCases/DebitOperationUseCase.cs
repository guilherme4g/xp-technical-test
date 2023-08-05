using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases
{
    public class DebitOperationUseCase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly IOperationRepository _operationRepository;

        public DebitOperationUseCase(IWalletRepository walletRepository, IOperationRepository operationRepository)
        {
            _walletRepository = walletRepository;
            _operationRepository = operationRepository;
        }

        public async Task Execute(Operation operation)
        {
            Operation operationFound = await _operationRepository.GetById(operation.Id);

            if (operationFound != null)
            {
                throw new InvalidOperationException("Essa operação já foi processada.");
            }

            Wallet walletFound = await _walletRepository.GetByUserAndCurrency(operation.Owner.User, operation.Currency);

            if (walletFound == null)
            {
                throw new InvalidOperationException("Carteira não encontrada.");
            }

            if (walletFound.Balance < operation.Value)
            {
                throw new InvalidOperationException("Saldo Insuficiente");
            }


            await _operationRepository.Create(operation);

            await _walletRepository.UpdateBalance(walletFound.Id, operation.Value);
        }
    }
}
