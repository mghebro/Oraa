namespace ORAA.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? JewelryId { get; set; }
        public int? HandCraftManId { get; set; }
        public int? ConsultantId { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Images { get; set; }
        public bool IsVerifiedPurchase { get; set; }
        public bool IsApproved { get; set; }
        public string ModerationNotes { get; set; }
        public int HelpfulVotes { get; set; }
        public int ReportedCount { get; set; }
        public string ResponseContent { get; set; }
        public int? RespondedBy { get; set; }
        public DateTime? RespondedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
