namespace ORAA.Models.Apple;

public class AppleAuthRequest
{
    // Remove the authorization code since it's already been used
    // public string Code { get; set; }           
    // public string RedirectUri { get; set; }    

    public string AppleId { get; set; }         // Apple's unique user identifier
    public string? Email { get; set; }          // User's email (if provided)
    public string? Name { get; set; }           // User's name (only on first sign-in)
    public bool IsPrivateEmail { get; set; }    // Whether email is a private relay
    public string? RefreshToken { get; set; }   // Refresh token from Apple
    public string? AccessToken { get; set; }    // Access token from Apple

    // Additional processed data from the ID token
    public bool EmailVerified { get; set; }     // Email verification status
    public string? AuthTime { get; set; }       // When the user authenticated
    public string? TokenType { get; set; }      // Token type (usually "Bearer")
    public int? ExpiresIn { get; set; }
    public AppleAuthRequest()
    {
        AppleId = string.Empty;
        IsPrivateEmail = false;
        EmailVerified = false;
    }// Token expiration time
}
