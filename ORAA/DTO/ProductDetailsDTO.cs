using System;
using System.ComponentModel.DataAnnotations;

namespace ORAA.DTO
{
    public class ProductDetailsDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int? JewelryId { get; set; }

        public int? CrystalId { get; set; }

        public int? AffirmationId { get; set; }

        public int? FavoriteId { get; set; }

        public int? CartId { get; set; }

        public int? RitualId { get; set; }
    }
}
