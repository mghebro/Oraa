namespace ORAA.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Items { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ShippingAmount { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderStatus { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZipCode { get; set; }
        public string ShippingCountry { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZipCode { get; set; }
        public string BillingCountry { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime? EstimatedDelivery { get; set; }
        public DateTime? ActualDelivery { get; set; }
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public int UserId { get; set; }
        public User User { get; set; }
        public UserDetails UserDetails { get; set; }
        public int UserDetailsId { get; set; }

        //public int ShippingAddressId { get; set; }
        //public ShippingAddresses ShippingAddress { get; set; }

        public int? GiftCardId { get; set; }
        public GIftCard GiftCard { get; set; }

        public int? DiscountCodeId { get; set; }
        public DiscountCode DiscountCode { get; set; }

        public List<CartItem> CartItems { get; set; }


    }
}
