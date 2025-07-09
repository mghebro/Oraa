namespace ORAA.DTO;
public class JeweleryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Images { get; set; }
    public decimal Price { get; set; }
    public decimal? SalePrice { get; set; }
    public bool IsOnSale { get; set; }
    public decimal Weight { get; set; }
    public decimal Length { get; set; }
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public int StockQuantity { get; set; }
    public string ChakraAlignment { get; set; }
    public string CareInstructions { get; set; }
    public string Crystals { get; set; }
    public decimal AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
