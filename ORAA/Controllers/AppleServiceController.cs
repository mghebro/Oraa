using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORAA.Models.Apple;
using ORAA.Services.Interfaces;

namespace ORAA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppleServiceController : ControllerBase
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly IAppleService _appleService;
        public AppleServiceController(IAppleService applePayment)
        {
            _appleService = applePayment;
        }

        [HttpGet("apple1")]
        public IActionResult StartAppleLogin()
        {
            var clientId = "com.your.bundle.id";
            var redirectUri = "https://localhost:7118/api/Auth/apple/callback";
            var scope = "name email";

            var url = $"https://appleid.apple.com/auth/authorize?" +
                      $"client_id={clientId}&" +
                      $"redirect_uri={Uri.EscapeDataString(redirectUri)}&" +
                      $"response_type=code&" +
                      $"response_mode=form_post&" +
                      $"scope={scope}";

            return Redirect(url);
        }

        [HttpPost("apple")]
        public async Task<IActionResult> AppleLogin([FromBody] AppleAuthRequest request)
        {
            var dataToReturn = _appleService.AppleLogin(request);

            return Ok(dataToReturn);
        }


        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody] AppleValidateRequest request)
        {
            var result = await _appleService.ValidateApplePaySessionAsync(request.ValidationUrl);
            return Content(result, "application/json");
        }
    }
}
