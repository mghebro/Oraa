namespace ORAA.Models
{
    public class Ritual
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public string Difficulty { get; set; }
        public int Duration { get; set; }
        public string Materials { get; set; }
        public string CrystalsNeeded { get; set; }
        public string JewelryRecommended { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public Jewelery? jewelery { get; set; }
        public Crystal? crystal { get; set; }
    }
}
