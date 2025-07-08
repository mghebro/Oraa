namespace ORAA.Models;

public class Material
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Origin { get; set; }
    public decimal Hardness { get; set; }
    public string Color { get; set; }
    public string Transparency { get; set; }
    public string MetaphysicalProperties { get; set; } // JSON array
    public string CareInstructions { get; set; }
    public decimal PricePerGram { get; set; }
    public string Supplier { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
