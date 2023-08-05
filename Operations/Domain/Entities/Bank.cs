namespace Domain.Entities
{
    public class Bank
    {
        public Guid Id;
        public string Name;
        public string Ispb;
        public bool Active;
        public bool Blocked;

        public Bank(Guid id) { Id = id; }

        public Bank(Guid id, string name, string ispb, bool active, bool blocked)
        {
            Id = id;
            Name = name;
            Ispb = ispb;
            Active = active;
            Blocked = blocked;
        }

        public bool IsValid() { return Active && !Blocked; }
    }
}
