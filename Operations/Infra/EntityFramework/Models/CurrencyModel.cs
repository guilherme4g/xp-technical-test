namespace Infra.EntityFramework.Models
{
    public class CurrencyModel
    {
        public Guid Id;
        public string Name;
        public string Symbol;

        public CurrencyModel(Guid id, string name, string symbol)
        {
            Id = id;
            Name = name;
            Symbol = symbol;
        }
    }
}
