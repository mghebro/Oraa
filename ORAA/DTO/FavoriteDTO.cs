namespace ORAA.DTO
{
    public class FavoriteDTO
    {
        public string Items { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
