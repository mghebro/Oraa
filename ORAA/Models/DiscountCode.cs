namespace ORAA.Models
{
    public class DiscountCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal Value { get; set; }
        public decimal MinimumOrderAmount { get; set; }
        public decimal? MaximumDiscountAmount { get; set; }
        public int? UsageLimit { get; set; }
        public int UsageCount { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ApplicableCategories { get; set; }
        public string ApplicableProducts { get; set; }
        public string ExcludedCategories { get; set; }
        public string ExcludedProducts { get; set; }
        public bool IsFirstTimeUser { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public List<CartItem> CartItems { get; set; }
        public List<Purchase> Purchases { get; set; }

    }
}
