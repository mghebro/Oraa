namespace ORAA.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Engraving { get; set; }
        public string Size { get; set; }
        public string CustomizationNotes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
            

        public int UserId { get; set; }
        public User User { get; set; }

        //public int ShippingAddressId { get; set; }
        //public ShippingAddresses ShippingAddress { get; set; }

        public int? GiftCardId { get; set; }
        public GIftCard GiftCard { get; set; }

        public int? DiscountCodeId { get; set; }
        public DiscountCode DiscountCode { get; set; }

        public List<CartItem> CartItems { get; set; }


    }
}
