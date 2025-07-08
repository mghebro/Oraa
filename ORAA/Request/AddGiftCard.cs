namespace ORAA.Request
{
    public class AddGiftCard
    {
        public string Code { get; set; }
        public decimal InitialAmount { get; set; }
        public decimal CurrentBalance { get; set; }
        public int PurchasedBy { get; set; }
        public string RecipientEmail { get; set; }
        public string RecipientName { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string UsageHistory { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
