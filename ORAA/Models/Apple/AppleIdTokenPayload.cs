namespace ORAA.Models.Apple
{
    public class AppleIdTokenPayload
    {
        public string iss { get; set; }
        public string sub { get; set; }            // Unique Apple user ID
        public string email { get; set; }
        public bool email_verified { get; set; }
        public long auth_time { get; set; }
        public string nonce_supported { get; set; }
    }
}
