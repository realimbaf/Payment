namespace Wiki.Payment.Common.POCO.DTO
{
    public class DTOPayment
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ClientCode { get; set; }
        public string ClientName { get; set; }
        public int OperatorId { get; set; }
        public int OperatorCode { get; set; }
        public string OperatorName { get; set; }
        public decimal Price { get; set; }
        public string CreatedDate { get; set; }
        public PaymentType Type { get; set; }
        public int ErpCode { get; set; }
        public string LastModified { get; set; }
    }
}
