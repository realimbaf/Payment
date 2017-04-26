using Wiki.Payment.Common.POCO.ValueObject;

namespace Wiki.Payment.Common.POCO
{
    public class Client
    {
        public User User { get; private set; }
        public string Phone { get; private set; }
        
        private Client([NotNull]User user, [NotNull]string phone)
        {
            this.User = user;
            this.Phone = phone;
            
        }

        public static Client CreateClient([NotNull] User user, string phone)
        {            
            return new Client(user,phone);
        }
    }
}
