using ORAA.Core;
using ORAA.DTO;
using ORAA.Models.Apple;

namespace ORAA.Services.Interfaces
{
    public interface IAppleService
    {
        Task<ApiResponse<AppleTokenResponseDTO>> AppleLogin(AppleAuthRequest request);
        Task<string> ValidateApplePaySessionAsync(string validationUrl);
    }
}