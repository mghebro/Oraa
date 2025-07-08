namespace ORAA.Request
{
    public class AddCrystal
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Specialties { get; set; }
        public int Experience { get; set; }
        public string Location { get; set; }
        public string Avatar { get; set; }
        public string Portfolio { get; set; }
        public decimal Rating { get; set; }
        public int TotalReviews { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
