using ORAA.Enums;

namespace ORAA.Models;

public class Jewelery
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public MARKET_TYPE MarketType { get; set; }
    public JEWELRY_FOR JEWELRY_FOR { get; set; }
    public int HandCraftManId { get; set; }
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
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public decimal AverageRating { get; set; }
    public int TotalReviews { get; set; }

    public List<Crystal> Crystal { get; set; }
    public HandCraftMan HandCraftMan { get; set; }
    public Material Material { get; set; }
    public int MaterialId { get; set; }
    public Affirmation Affirmation { get; set; }
    public int? AffirmationId { get; set; }
    public Ritual Ritual { get; set; }
    public int? RitualId { get; set; }

    public Review Review { get; set; }
    public int? ReviewId { get; set; }




}
