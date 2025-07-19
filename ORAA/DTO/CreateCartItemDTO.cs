namespace ORAA.DTO
{
    public class CreateCartItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? Engraving { get; set; }
        public string? Size { get; set; }
        public string? CustomizationNotes { get; set; }
    }
}
