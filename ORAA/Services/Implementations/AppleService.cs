using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ORAA.Core;
using ORAA.Data;
using ORAA.DTO;
using ORAA.Enums;
using ORAA.Models;
using ORAA.Models.Apple;
using ORAA.Services.Interfaces;

namespace ORAA.Services.Implementations
{
    public class AppleService : IAppleService
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<User> _userManager;
        private readonly DataContext _context;
        private readonly IJWTService _jWTService;
        private readonly ILogger<AppleService> _logger;

        public AppleService(
            HttpClient httpClient,
            UserManager<User> userManager,
            DataContext context,
            IJWTService jWTService,
            ILogger<AppleService> logger)
        {
            _httpClient = httpClient;
            _userManager = userManager;
            _context = context;
            _jWTService = jWTService;
            _logger = logger;
        }

        public async Task<ApiResponse<AppleTokenResponseDTO>> AppleLogin(AppleAuthRequest request)
        {
            try
            {
                _logger.LogInformation("Processing Apple login for user: {AppleId}", request.AppleId);

                // The Node.js app has already exchanged the code for tokens
                // We just need to process the user data

                // Find or create user
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.AppleId == request.AppleId ||
                                             (u.Email == request.Email && u.EmailConfirmed && !string.IsNullOrEmpty(request.Email)));

                if (user == null)
                {
                    _logger.LogInformation("Creating new user for Apple ID: {AppleId}", request.AppleId);

                    // Create new user
                    user = new User
                    {
                        UserName = request.Email ?? $"apple_{request.AppleId}",
                        Email = request.Email,
                        AppleId = request.AppleId,
                        EmailConfirmed = request.EmailVerified,
                        IsVerified = true,
                        FirstName = request.Name?.Split(' ').FirstOrDefault() ?? "",
                        LastName = request.Name?.Split(' ').Skip(1).FirstOrDefault() ?? "",
                        Status = ACCOUNT_STATUS.VERIFIED,
                        Role = ROLES.USER,
                        LastLoginAt = DateTime.UtcNow, // FIXED: Set LastLoginAt for new users
                        IsActive = true
                    };

                    var createResult = await _userManager.CreateAsync(user);
                    if (!createResult.Succeeded)
                    {
                        _logger.LogError("Failed to create user: {Errors}",
                            string.Join(", ", createResult.Errors.Select(e => e.Description)));

                        return new ApiResponse<AppleTokenResponseDTO>
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Message = "Failed to create user account: " + string.Join(", ", createResult.Errors.Select(e => e.Description)),
                            Data = null
                        };
                    }

                    _logger.LogInformation("Successfully created new user with ID: {UserId}", user.Id);
                }
                else
                {
                    _logger.LogInformation("Found existing user: {UserId}", user.Id);

                    // Update existing user
                    if (string.IsNullOrEmpty(user.AppleId))
                    {
                        user.AppleId = request.AppleId;
                        _logger.LogInformation("Updated user {UserId} with AppleId", user.Id);
                    }

                    // FIXED: Always update LastLoginAt and UpdatedAt for existing users
                    user.LastLoginAt = DateTime.UtcNow;

                    // FIXED: Use try-catch to handle update errors
                    try
                    {
                        var updateResult = await _userManager.UpdateAsync(user);
                        if (!updateResult.Succeeded)
                        {
                            _logger.LogError("Failed to update user {UserId}: {Errors}",
                                user.Id, string.Join(", ", updateResult.Errors.Select(e => e.Description)));
                        }
                        else
                        {
                            _logger.LogInformation("Successfully updated LastLoginAt for user {UserId}", user.Id);
                        }
                    }
                    catch (Exception updateEx)
                    {
                        _logger.LogError(updateEx, "Exception while updating user {UserId}", user.Id);
                        throw;
                    }
                }

                // Generate your app's JWT token
                var userToken = _jWTService.GetUserToken(user);
                var refreshToken = _jWTService.GenerateRefreshToken();

                // FIXED: Update refresh token and save changes
                try
                {
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiresAtUtc = DateTime.UtcNow.AddDays(30);

                    var tokenUpdateResult = await _userManager.UpdateAsync(user);
                    if (!tokenUpdateResult.Succeeded)
                    {
                        _logger.LogWarning("Failed to update refresh token for user {UserId}: {Errors}",
                            user.Id, string.Join(", ", tokenUpdateResult.Errors.Select(e => e.Description)));
                    }
                    else
                    {
                        _logger.LogInformation("Successfully updated refresh token for user {UserId}", user.Id);
                    }
                }
                catch (Exception tokenEx)
                {
                    _logger.LogError(tokenEx, "Exception while updating refresh token for user {UserId}", user.Id);
                    // Don't throw here, continue with response
                }

                // Create the response DTO
                var appleTokenResponseDTO = new AppleTokenResponseDTO
                {
                    Email = user.Email,
                    AppleId = user.AppleId,
                    AccessToken = userToken.Token,
                    RefreshToken = refreshToken
                };

                _logger.LogInformation("Successfully processed Apple login for user: {UserId}, LastLoginAt: {LastLoginAt}",
                    user.Id, user.LastLoginAt);

                return new ApiResponse<AppleTokenResponseDTO>
                {
                    Data = appleTokenResponseDTO,
                    Status = StatusCodes.Status200OK,
                    Message = "Login successful"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AppleLogin: {Message}", ex.Message);

                return new ApiResponse<AppleTokenResponseDTO>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                };
            }
        }

        // Keep this method for Apple Pay validation if needed
        public async Task<string> ValidateApplePaySessionAsync(string validationUrl)
        {
            // Implementation for Apple Pay (different from Sign in with Apple)
            throw new NotImplementedException("Apple Pay validation not implemented in this example");
        }
    }
}