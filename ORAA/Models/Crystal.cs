namespace ORAA.Models
{
    public class Crystal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ScientificName { get; set; }
        public string Description { get; set; }
        public string Origin { get; set; }
        public string Color { get; set; }
        public decimal Hardness { get; set; }
        public string Formation { get; set; }
        public string Images { get; set; }
        public string Rarity { get; set; }
        public decimal PricePerCarat { get; set; }
        public string CareInstructions { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public List<Jewelery> Jewelery { get; set; }
    }
}
