namespace ORAA.Models;

public class Affirmation
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Category { get; set; }
    public string CrystalAlignment { get; set; }
    public string ChakraAlignment { get; set; }
    public string Author { get; set; }
    public int? Duration { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    //zenora
    public int ConsultantId { get; set; }
    public Consultant Consultant { get; set; }

    public int JeweleryId { get; set; }
    public Jewelery Jewelery { get; set; }

}
