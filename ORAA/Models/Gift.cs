namespace ORAA.Models
{
    public class Gift
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int? RecipientId { get; set; }
        public string RecipientEmail { get; set; }
        public int JewelryId { get; set; }
        public string Message { get; set; }
        public DateTime? ScheduledDelivery { get; set; }
        public string Status { get; set; }
        public bool GiftWrap { get; set; }
        public string GiftWrapType { get; set; }
        public int? OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Crystal Crystal { get; set; }

    }
}
