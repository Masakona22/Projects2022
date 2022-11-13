using System;
using System.Collections.Generic;

namespace SPNewApi2.Models
{
    public partial class Job
    {
        public Job()
        {
            Reviews = new HashSet<Review>();
        }

        public int JobId { get; set; }
        public string? JobStatus { get; set; }
        public DateTime? JobDatestart { get; set; }
        public TimeSpan? JobTimestart { get; set; }
        public DateTime JobDateend { get; set; }
        public TimeSpan? JobTimeend { get; set; }
        public int UserId { get; set; }
        public int MerchId { get; set; }
        public int BookId { get; set; }

        public virtual Booking Book { get; set; } = null!;
        public virtual Merchant Merch { get; set; } = null!;
        public virtual Client User { get; set; } = null!;
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
