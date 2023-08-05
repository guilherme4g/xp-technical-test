namespace Infra.EntityFramework.Models
{
    public class WalletModel
    {
        public Guid Id;
        public string Name;
        public Guid UserId;
        public Guid CurrencyId;
        public CurrencyModel Currency;
        public int Balance;
        public bool Standart;
        public bool Active;

        public WalletModel(Guid id, string name, Guid userId, Guid currencyId, int balance, bool standart, bool active)
        {
            Id = id;
            Name = name;
            UserId = userId;
            CurrencyId = currencyId;
            Balance = balance;
            Standart = standart;
            Active = active;
        }
    }
}
