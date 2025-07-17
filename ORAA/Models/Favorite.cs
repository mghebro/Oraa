namespace ORAA.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public string Items { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int? JewelryId { get; set; }
        public Jewelery Jewelry { get; set; }

        public int? CrystalId { get; set; }
        public Crystal Crystal { get; set; }
    }
}
