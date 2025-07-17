using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using ORAA.Core;
using ORAA.DTO;
using ORAA.Models.Apple;
using ORAA.Services.Interfaces;

namespace ORAA.Services.Implementations
{
    public class AppleService : IAppleService
    {
        private readonly HttpClient _httpClient;
        public AppleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ApiResponse<AppleTokenResponseDTO>> AppleLogin(AppleAuthRequest request)
        {
            var appleclientId = "com.ora.apple.login.dev";
            var clientSecret = "your-signed-jwt-secret"; // use JWT signed with your Apple key

            var parameters = new Dictionary<string, string>
        {
            {"client_id", appleclientId },
            {"client_secret", clientSecret },
            {"code", request.Code },
            {"grant_type", "authorization_code" },
            {"redirect_uri", request.RedirectUri }
        };

            var content = new FormUrlEncodedContent(parameters);
            var response = await _httpClient.PostAsync("https://appleid.apple.com/auth/token", content);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<AppleTokenResponse>(responseString);

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokenResponse.id_token);
            var payload = JsonSerializer.Deserialize<AppleIdTokenPayload>(JsonSerializer.Serialize(jwtToken.Payload));

            // Save or find user in DB
            var userEmail = payload.email;
            var userAppleId = payload.sub;

            var SuccessResponse = ApiResponseFactory.CreateResponse(
                StatusCodes.Status200OK,
                "Login successful",
                data: new AppleTokenResponseDTO
                {
                    Email = userEmail,
                    AppleId = userAppleId,
                    AccessToken = tokenResponse.access_token
                });


            return new ApiResponse<AppleTokenResponseDTO>
            {
                Data = SuccessResponse.Data,
                Status = SuccessResponse.Status,
                Message = SuccessResponse.Message
            };

        }

        public async Task<string> ValidateApplePaySessionAsync(string validationUrl)
        {
            var certPath = "Certificates/apple-pay-cert.p12"; 
            var certPassword = "your_password"; 

            var certificate = new X509Certificate2(certPath, certPassword, X509KeyStorageFlags.MachineKeySet);
            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(certificate);

            var client = new HttpClient(handler);

            var payload = new
            {
                merchantIdentifier = "merchant.com.yourdomain",
                displayName = "Your Store",
                initiative = "web",
                initiativeContext = "yourdomain.com"
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(validationUrl, content);
            return await response.Content.ReadAsStringAsync();
        }
    }
    
}
