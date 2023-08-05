using Domain.Entities;
using Common.Validators;
using Domain.Repositories;
using Application.UseCases;
using System.ComponentModel.DataAnnotations;
namespace Interface.Controllers
{
    public class DebitOperationRequest
    {
        [Required]
        [ValidGuidAttribute]
        public Guid Id { get; private set; }

        [Required]
        [ValidGuidAttribute]
        public Guid CurrencyId { get; private set; }

        [Required]
        [ValidGuidAttribute]
        public Guid OwnerUserId { get; private set; }
        [Required]
        public string OwnerAgency { get; private set; }
        [Required]
        public string OwnerNumber { get; private set; }
        [Required]
        public string OwnerDigit { get; private set; }
        [Required]
        public Guid OwnerBankId { get; private set; }

        public string BeneficiaryUserName { get; private set; }
        public string BeneficiaryUserDocument { get; private set; }
        public string BeneficiaryAgency { get; private set; }
        public string BeneficiaryNumber { get; private set; }
        public string BeneficiaryDigit { get; private set; }
        public Guid BeneficiaryBankId { get; private set; }

        [Required]
        public int value { get; private set; }

        public DebitOperationRequest(
            Guid currencyId,
            Guid ownerUserId,
            string ownerAgency,
            string ownerNumber,
            string ownerDigit,
            Guid ownerBankId,
            string beneficiaryUserName,
            string beneficiaryUserDocument,
            string beneficiaryAgency,
            string beneficiaryNumber,
            string beneficiaryDigit,
            Guid beneficiaryBankId)
        {
            CurrencyId = currencyId;
            OwnerUserId = ownerUserId;
            OwnerAgency = ownerAgency;
            OwnerNumber = ownerNumber;
            OwnerDigit = ownerDigit;
            OwnerBankId = ownerBankId;
            BeneficiaryUserName = beneficiaryUserName;
            BeneficiaryUserDocument = beneficiaryUserDocument;
            BeneficiaryAgency = beneficiaryAgency;
            BeneficiaryNumber = beneficiaryNumber;
            BeneficiaryDigit = beneficiaryDigit;
            BeneficiaryBankId = beneficiaryBankId;
        }
    }

    public class DebitOperationController
    {
        private readonly DebitOperationUseCase _debitOperationUseCase;

        public DebitOperationController(IWalletRepository walletRepository, IOperationRepository operationRepository)
        {
            _debitOperationUseCase = new DebitOperationUseCase(walletRepository, operationRepository);
        }

        public async Task Execute(DebitOperationRequest request)
        {

            User user = new User(request.OwnerUserId);
            Currency currency = new Currency(request.CurrencyId);

            Bank ownerBank = new Bank(request.OwnerBankId);
            Bank beneficiaryBank = new Bank(request.BeneficiaryBankId);

            Account ownerAccount = new Account(request.OwnerAgency, request.OwnerNumber, request.OwnerDigit, ownerBank, user);
            Account beneficiaryAccount = new Account(request.BeneficiaryAgency, request.BeneficiaryNumber, request.BeneficiaryDigit, beneficiaryBank, null, request.BeneficiaryUserName, request.BeneficiaryUserDocument);

            Operation operation = new Operation(request.Id, ownerAccount, beneficiaryAccount, currency, TransactionType.DEBIT, request.value);

            await _debitOperationUseCase.Execute(operation);

        }
    }
}
