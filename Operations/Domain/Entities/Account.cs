namespace Domain.Entities
{
    public class Account
    {
        public User? User;
        public string? UserName;
        public string? UserDocument;
        public string Agency;
        public string Number;
        public string Digit;
        public Bank Bank;

        
        public Account(string agency, string number, string digit, Bank bank, User? user = null, string? userName = null, string? userDocument = null) {
            User = user;
            Agency = agency;
            Number = number;
            Digit = digit;
            Bank = bank;
            User = user;
            UserName = userName;
            UserDocument = userDocument;
        }
    }
}
