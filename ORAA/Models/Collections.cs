namespace ORAA.Models
{
    public class Collections
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoURL {  get; set; }
        public Jewelery? Jewelery { get; set; }
        public Crystal? Crystal { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
