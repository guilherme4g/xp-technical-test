namespace Domain.Entities
{
    public class Currency
    {
        public Guid Id;
        public string Name;
        public string Symbol;

        public Currency(Guid id)
        {
            Id = id;
        } 
        
        public Currency(Guid id, string name, string symbol)
        {
            Id = id;
            Name = name;
            Symbol = symbol;
        }
    }
}
