using System;

namespace Wiki.Payment.Common.POCO.ValueObject
{
    public class User
    {
        public int ClientId { get; private set; }
        public int ClientCode { get; private set; }
        public string Name { get; private set; }

        public User(int clientId, int clientCode, [NotNull] string name)
        {
            if (name == null)
            {
                throw new ArgumentException("argument:name_not_found");
            }
            ClientId = clientId;
            ClientCode = clientCode;
            Name = name;
        }
    }
}
