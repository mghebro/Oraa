namespace ORAA.Models
{
    public class CartItem
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
        public Cart Cart { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int? JewelryId { get; set; }
        public Jewelery Jewelry { get; set; }

        public int? CrystalId { get; set; }
        public Crystal Crystal { get; set; }
        //add material
        public int? DiscountCodeId { get; set; }
        public DiscountCode DiscountCode { get; set; }

    }
}
