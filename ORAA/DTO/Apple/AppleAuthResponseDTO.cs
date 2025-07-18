namespace ORAA.DTO.Apple
{
    public class AppleAuthResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string AppleId { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Name { get; set; }
        public bool IsNewUser { get; set; }
        public DateTime AuthenticatedAt { get; set; } = DateTime.UtcNow;
    }
}
