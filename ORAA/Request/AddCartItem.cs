
namespace ORAA.Request;
public class AddCartItem
{
    public int CartId { get; set; }
    public int JewelryId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public string Engraving { get; set; }
    public string Size { get; set; }
    public string CustomizationNotes { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime UpdatedAt { get; set; }
}
