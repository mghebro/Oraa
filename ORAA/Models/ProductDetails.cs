namespace ORAA.Models;

public class ProductDetails 
{

    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal  Price { get; set; }
    public int Quantity { get; set; }


    public Jewelery? Jewelery { get; set; }
    public int? JewelryId { get; set; }
    public Crystal? Crystal { get; set; }
    public int? CrystalId { get; set; }
    public Affirmation? Affirmation { get; set; }
    public int? AffirmationId { get; set; }

    public Favorite? Favorite { get; set; }
    public int? FavoriteId { get; set; }

    public Cart? Cart { get; set; }
    public int? CartId { get; set; }

    public Ritual? Ritual { get; set; }
    public int? RitualId { get; set; }


}
