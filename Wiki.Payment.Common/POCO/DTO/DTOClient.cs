namespace Wiki.Payment.Common.POCO.DTO
{
   public class DTOClient
    {
        public int Id { get; set; }
        public int ClientCode { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
    public class DTOClients
    {
        public int[] Ids { get; set; }
    }
}
