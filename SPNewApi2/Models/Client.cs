using System;
using System.Collections.Generic;

namespace SPNewApi2.Models
{
    public partial class Client
    {
        public Client()
        {
            Bookings = new HashSet<Booking>();
            Jobs = new HashSet<Job>();
            Quotations = new HashSet<Quotation>();
            Reviews = new HashSet<Review>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserSurname { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string UserContactdetails { get; set; } = null!;
        public string UserAddress { get; set; } = null!;
        public string UserProvince { get; set; } = null!;
        public string UserCity { get; set; } = null!;
        public string? UserStatus { get; set; }
        public string UserPassword { get; set; } = null!;

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Quotation> Quotations { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
