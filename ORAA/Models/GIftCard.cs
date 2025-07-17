namespace ORAA.Models
{
    public class GIftCard
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal CurrentBalance { get; set; }
        public int PurchasedById { get; set; }
        //recipient tel and location
        public string RecipientEmail { get; set; }
        public string RecipientName { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string UsageHistory { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public User PurchasedBy { get; set; }

    }
}
