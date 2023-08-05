namespace Domain.Entities
{
    public class Wallet
    {
        public Guid Id;
        public string Name;
        public User User;
        public Currency Currency;
        public int Balance;
        public bool Standart;
        public bool Active;

        public Wallet(Guid id, string name, User user, Currency currency, int balance, bool standart, bool active)
        {
            Id = id;
            Name = name;
            User = user;
            Currency = currency;
            Balance = balance;
            Standart = standart;
            Active = active;
        }

        public bool IsDefault()
        {
            return Standart;
        }

        public bool IsValid()
        {
            return Active;
        }

        public bool HasFunds(int value) { 
            return Balance >= value;
        }
    }
}
