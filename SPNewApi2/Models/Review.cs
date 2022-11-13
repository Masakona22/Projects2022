using System;
using System.Collections.Generic;

namespace SPNewApi2.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string ReviewRating { get; set; } = null!;
        public string ReviewMessage { get; set; } = null!;
        public int UserId { get; set; }
        public int MerchId { get; set; }
        public int JobId { get; set; }

        public virtual Job Job { get; set; } = null!;
        public virtual Merchant Merch { get; set; } = null!;
        public virtual Client User { get; set; } = null!;
    }
}
