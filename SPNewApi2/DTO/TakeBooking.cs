namespace SPNewApi2.DTO
{
    public class TakeBooking
    {
        // public int BookId { get; set; }
        // public string? BookStatus { get; set; }
        public DateTime BookDate { get; set; }
        public TimeSpan BookTime { get; set; }
       // public DateTime BookDatetime { get; set; }
        public int UserId { get; set; }
        public int MerchId { get; set; }

        public string BookMessage { get; set; } = null!;
    }
}
