namespace ORAA.DTO
{
    public class PurchaseDTO
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        public ShippingAddressSummaryDTO ShippingAddress { get; set; }
        public List<CartItemSummaryDTO> CartItems { get; set; }
    }

}
