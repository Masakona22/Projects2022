using System;
using System.Collections.Generic;

namespace SPNewApi2.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Jobs = new HashSet<Job>();
            Quotations = new HashSet<Quotation>();
        }

        public int BookId { get; set; }
        public string? BookStatus { get; set; }
        public DateTime BookDate { get; set; }
        public TimeSpan BookTime { get; set; }
        public int UserId { get; set; }
        public int MerchId { get; set; }
        public string BookMessage { get; set; } = null!;

        public virtual Merchant Merch { get; set; } = null!;
        public virtual Client User { get; set; } = null!;
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Quotation> Quotations { get; set; }
    }
}
