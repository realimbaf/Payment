namespace Wiki.Payment.Common.POCO.DTO
{
    public class DTOInsertPayment
    {
        public int ClientCode { get; set; }
        public int OperatorId { get; set; }
        public decimal Price { get; set; }
        public PaymentType Type { get; set; }
    }
}
