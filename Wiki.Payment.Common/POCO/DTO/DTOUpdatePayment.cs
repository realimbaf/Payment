using System;

namespace Wiki.Payment.Common.POCO.DTO
{
    public class DTOUpdatePayment
    {
        public int? OperatorId { get; set; }
        public int? ClientId { get; set; }
        public decimal? Price { get; set; }
        public int? ErpCode { get; set; }
        public PaymentType? Type { get; set; }
    }
}
