namespace Infra.EntityFramework.Models
{
    public class BankModel
    {
        public Guid Id;
        public string Name;
        public string Ispb;
        public bool Active;
        public bool Blocked;


        public BankModel(Guid id, string name, string ispb, bool active, bool blocked)
        {
            Id = id;
            Name = name;
            Ispb = ispb;
            Active = active;
            Blocked = blocked;
        }
    }
}
