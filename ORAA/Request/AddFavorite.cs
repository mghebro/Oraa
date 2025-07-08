namespace ORAA.Request
{
    public class AddFavorite
    {
        public int UserId { get; set; }
        public string Items { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
