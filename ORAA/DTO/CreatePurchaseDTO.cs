namespace ORAA.DTO
{
    public class CreatePurchaseDTO
    {
        public int UserDetailsId { get; set; }
        public List<int> CartItemIds { get; set; }
    }
}
