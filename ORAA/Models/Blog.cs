namespace ORAA.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string Excerpt { get; set; }
        public int Author { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; }
        public string FeaturedImage { get; set; }
        public string Images { get; set; }
        public string Status { get; set; }
        public bool IsFeature { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public int ReadingTime { get; set; }
        public int ViewCount { get; set; }
        public List<Crystal>? RelatedCrystals { get; set; }
        public List<Jewelery>? RelatedJewelry { get; set; }
        public List<Ritual>? RelatedRitual { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

    }
}
