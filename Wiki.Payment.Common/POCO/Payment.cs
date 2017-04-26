using System;
using Wiki.Payment.Common.POCO.ValueObject;

namespace Wiki.Payment.Common.POCO
{
    public class Payment
    {
        public int Id { get; private set; }
        public User Operator { get; private set; }
        public User Client { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public PaymentType Type { get; private set; }
        public int ErpCode { get; private set; }
        public DateTime? LastModified { get; set; }

        private Payment(int id,[NotNull]User instructor,[NotNull]User client, decimal price, DateTime createdDate, PaymentType type,int erpCode, DateTime? lastModified)
        {
            Operator = instructor;
            Client = client;
            Price = price;
            CreatedDate = createdDate;
            Type = type;
            Id = id;
            ErpCode = erpCode;
            LastModified = lastModified;
        }


        public static Payment CreatePayment(int id,[NotNull]User instructor,[NotNull]User client, decimal price, DateTime createdDate,
            PaymentType type,int erpCode,DateTime? lastModified)
        {
            return new Payment(id,instructor,client,price,createdDate,type,erpCode,lastModified);
        }
    }
    public enum PaymentType
    {
        auto = 1,
        hand = 2
    }
}
