namespace ORAA.DTO;
public class JewelerySearchRequest
{
    public string? Name { get; set; }
    public string? Category { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MaterialId { get; set; }
    public string? ChakraAlignment { get; set; }
    public bool? IsOnSale { get; set; }
    public int? HandCraftManId { get; set; }
}
