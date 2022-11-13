namespace SPNewApi2.DTO
{
    public class ReviewTake
    {
        public string ReviewRating { get; set; } = null!;
        public string ReviewMessage { get; set; } = null!;

        public int UserId { get; set; }
        public int MerchId { get; set; }
       // public int JobId { get; set; }
    }
}
