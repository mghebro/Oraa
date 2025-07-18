namespace ORAA.DTO;

public class CollectionsDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string PhotoURL { get; set; }
    public JeweleryDTO? Jewelery { get; set; }
    public CrystalDTO? Crystal { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
