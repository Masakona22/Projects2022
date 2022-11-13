using System;
using System.Collections.Generic;

namespace SPNewApi2.Models
{
    public partial class Quotation
    {
        public int QuotId { get; set; }
        public int BookId { get; set; }
        public int MerchId { get; set; }
        public int UserId { get; set; }
        public string QuotAmount { get; set; } = null!;
        public string QuotDescription { get; set; } = null!;

        public virtual Booking Book { get; set; } = null!;
        public virtual Merchant Merch { get; set; } = null!;
        public virtual Client User { get; set; } = null!;
    }
}
