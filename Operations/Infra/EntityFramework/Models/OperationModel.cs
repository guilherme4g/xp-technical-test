namespace Infra.EntityFramework.Models
{
    public class OperationModel
    {
        public Guid Id;

        public string Type;
        public Guid CurrencyId;
        public CurrencyModel Currency;
        public int Value;

        public Guid?   OwnerUserId;
        public string? OwnerUserName;
        public string? OwnerUserDocument;
        public string  OwnerAgency;
        public string  OwnerNumber;
        public string  OwnerDigit;
        public Guid OwnerBankId;

        public Guid?   BeneficiaryUserId;
        public string? BeneficiaryUserName;
        public string? BeneficiaryUserDocument;
        public string  BeneficiaryAgency;
        public string  BeneficiaryNumber;
        public string  BeneficiaryDigit;
        public Guid BeneficiaryBankId;
    }
}
