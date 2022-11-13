namespace SPNewApi2.DTO
{
    public class MerchantRegister
    {
  
        public string MerchName { get; set; } = null!;
        public string MerchSurname { get; set; } = null!;
        public string MerchEmail { get; set; } = null!;
        public string MerchPassword { get; set; } = null!;
        public string MerchType { get; set; } = null!;
   
        public string MerchAddress { get; set; } = null!;
        public string MerchCity { get; set; } = null!;
        public string MerchProvince { get; set; } = null!;
        public string MerchContactdetails { get; set; } = null!;
       // public string? MerchFile { get; set; }
    }
}
