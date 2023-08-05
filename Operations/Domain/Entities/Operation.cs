namespace Domain.Entities
{
    public enum TransactionType
    {
        CREDIT,
        DEBIT
    }

    public class Operation
    {
        public Guid Id;
        public Account Owner;
        public Account Beneficiary;
        public Currency Currency;
        public TransactionType Type;
        public int Value;

        public Operation(Guid id, Account owner, Account beneficiary, Currency currency, TransactionType type, int value)
        {
            Id = id;
            Owner = owner;
            Beneficiary = beneficiary;
            Currency = currency;
            Type = type;
            Value = value;
        }

        public bool IsSameAccount()
        {
            return (Owner.User != null && Beneficiary.User != null && Owner.User.Id == Beneficiary.User.Id);
        }
    }
}
