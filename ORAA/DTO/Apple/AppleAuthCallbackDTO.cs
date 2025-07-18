namespace ORAA.DTO.Apple;

public class AppleAuthCallbackDTO
{
    public string AppleId { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Name { get; set; }
    public bool IsPrivateEmail { get; set; }
    public string? RefreshToken { get; set; }
    public string? AccessToken { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool EmailVerified { get; set; }
    public DateTime? AuthTime { get; set; }
    public int? ExpiresIn { get; set; }
    public string? TokenType { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
