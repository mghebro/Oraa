namespace ORAA.Request;

public class AddAffirmation
{
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
}
