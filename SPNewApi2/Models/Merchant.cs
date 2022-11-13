using System;
using System.Collections.Generic;

namespace SPNewApi2.Models
{
    public partial class Merchant
    {
        public Merchant()
        {
            Bookings = new HashSet<Booking>();
            Jobs = new HashSet<Job>();
            Quotations = new HashSet<Quotation>();
            Reviews = new HashSet<Review>();
        }

        public int MerchId { get; set; }
        public string MerchName { get; set; } = null!;
        public string MerchSurname { get; set; } = null!;
        public string MerchEmail { get; set; } = null!;
        public string MerchPassword { get; set; } = null!;
        public string MerchType { get; set; } = null!;
        public string? MerchVerify { get; set; }
        public string? MerchStatus { get; set; }
        public string MerchAddress { get; set; } = null!;
        public string MerchCity { get; set; } = null!;
        public string MerchProvince { get; set; } = null!;
        public string MerchContactdetails { get; set; } = null!;
        public string? MerchFile { get; set; }
        public string? MerchProfilepicture { get; set; }
        public string? MerchPictures1 { get; set; }
        public string? MerchPictures2 { get; set; }
        public string? MerchPictures3 { get; set; }
        public string? MerchIdnumber { get; set; }
        public string? MerchTaxnumber { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
        public virtual ICollection<Quotation> Quotations { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
