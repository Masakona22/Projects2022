namespace SPNewApi2.DTO
{
    public class Addquot
    {
       
        public int BookId { get; set; }
        public int MerchId { get; set; }
        public int UserId { get; set; }
        public string QuotAmount { get; set; } = null!;
        public string QuotDescription { get; set; } = null!;
    }
}
