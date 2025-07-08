using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ORAA.Core;
using ORAA.Data;
using ORAA.DTO;
using ORAA.Enums;
using ORAA.Models;
using ORAA.Request;
using ORAA.Services.Interfaces;
using ORAA.SMTP;
using ORAA.Validations;

namespace ORAA.Services.Implementations
{
    public class UserService : IUserService
    {
       
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly UserManager<User> _userManager;



        public UserService(DataContext context, IMapper mapper, IJWTService jwtService, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<ApiResponse<UserDTO>> UpdateUser(int id, string changeParameter, string toChange)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                var response = new ApiResponse<UserDTO>
                {
                    Data = null,
                    Status = StatusCodes.Status404NotFound,
                    Message = "User not found"
                };
                return response;
            }

            var validParameters = new[] { "name", "lastname", "email", "password" };
            if (!validParameters.Contains(changeParameter.ToLower()))
            {
                var response = new ApiResponse<UserDTO>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Invalid parameter. Valid parameters are: name, lastname, email, password"
                };
                return response;
            }

            IdentityResult updateResult = null;

            switch (changeParameter.ToLower())
            {
                case "name":
                    user.FirstName = toChange;
                    updateResult = await _userManager.UpdateAsync(user);
                    break;

                case "lastname":
                    user.LastName = toChange;
                    updateResult = await _userManager.UpdateAsync(user);
                    break;

                case "email":

                    var emailToken = await _userManager.GenerateChangeEmailTokenAsync(user, toChange);
                    updateResult = await _userManager.ChangeEmailAsync(user, toChange, emailToken);

                    if (updateResult.Succeeded)
                    {
                        user.UserName = toChange;
                        updateResult = await _userManager.UpdateAsync(user);
                    }
                    break;

                case "password":
                    // Generate password reset token and update password
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    updateResult = await _userManager.ResetPasswordAsync(user, token, toChange);
                    break;
            }

            if (updateResult?.Succeeded == true)
            {
                var response = new ApiResponse<UserDTO>
                {
                    Data = _mapper.Map<UserDTO>(user),
                    Status = StatusCodes.Status200OK,
                    Message = $"{changeParameter} updated successfully"
                };
                return response;
            }
            else
            {
                var errorMessages = updateResult?.Errors?.Any() == true
                    ? string.Join(", ", updateResult.Errors.Select(e => e.Description))
                    : "Update failed";

                var response = new ApiResponse<UserDTO>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = $"Update failed: {errorMessages}"
                };
                return response;
            }
        }

        public async Task<ApiResponse<UserDTO>> DeleteUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                var response = new ApiResponse<UserDTO>
                {
                    Data = null,
                    Status = StatusCodes.Status404NotFound,
                    Message = "User not found"
                };

                return response;
            }
            else
            {
                _context.Users.Remove(user);
                _context.SaveChanges();

                var response = new ApiResponse<UserDTO>
                {
                    Data = _mapper.Map<UserDTO>(user),
                    Status = StatusCodes.Status200OK,
                    Message = "User deleted"
                };

                return response;
            }
        }

        public async Task<ApiResponse<UserDTO>> RegisterUser(AddUser request)
        {
            var userExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (userExists != null) // If user already exists, return conflict
            {
                var response = new ApiResponse<UserDTO>
                {
                    Data = null,
                    Status = StatusCodes.Status409Conflict,
                    Message = "User already exists"
                };
                return response;
            }

            var user = _mapper.Map<User>(request);

            // Set UserName to Email (required by Identity)
            user.UserName = user.Email;

            var validator = new UserValidator();
            var result = validator.Validate(user);

            if (!result.IsValid)
            {
                var errorResponse = new ApiResponse<UserDTO>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Invalid User Information",
                };
                return errorResponse;
            }

            // Generate verification code
            Random rand = new Random();
            string randomCode = rand.Next(10000, 99999).ToString();
            user.VerificationCode = randomCode;

            // Send verification email
            SMTPService smtpService = new SMTPService();
            smtpService.SendEmail(user.Email, "Verification", $"<p>{user.VerificationCode}</p>");

            // Create user using UserManager
            var createResult = await _userManager.CreateAsync(user, request.Password);

            if (createResult.Succeeded)
            {
                // User created successfully
                var successResponse = new ApiResponse<UserDTO>
                {
                    Data = _mapper.Map<UserDTO>(user),
                    Status = StatusCodes.Status200OK,
                    Message = "User registered successfully. Please check your email for verification code.",
                };
                return successResponse;
            }
            else
            {
                // User creation failed
                var errorMessages = string.Join(", ", createResult.Errors.Select(e => e.Description));
                var failureResponse = new ApiResponse<UserDTO>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = $"User registration failed: {errorMessages}",
                };
                return failureResponse;
            }
        }

        public async Task<ApiResponse<bool>> Verify(string email, string code)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                var notfoundRequest = new ApiResponse<bool>
                {
                    Data = false,
                    Message = "User Not Found",
                    Status = StatusCodes.Status404NotFound,
                };

                return notfoundRequest;
            }
            else
            {
                if (user.VerificationCode == code)
                {
                    user.Status = ACCOUNT_STATUS.VERIFIED;
                    user.VerificationCode = null;

                    _context.SaveChanges();
                    var SuccesResponse = new ApiResponse<bool>
                    {
                        Data = true,
                        Message = "User Verified",
                        Status = StatusCodes.Status200OK,
                    };
                    return SuccesResponse;
                }
                else
                {
                    var BadRequestResponse = new ApiResponse<bool>
                    {
                        Data = false,
                        Message = "Wrong Verification Code",
                        Status = StatusCodes.Status400BadRequest,
                    };
                    return BadRequestResponse;
                }
            }
        }

        public async Task<ApiResponse<UserDTO>> GetProfile(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                var response = new ApiResponse<UserDTO>
                {
                    Data = null,
                    Message = "User Not Found",
                    Status = StatusCodes.Status404NotFound,
                };

                return response;
            }
            else
            {
                if (user.Status == ACCOUNT_STATUS.VERIFIED)
                {
                    var Succesresponse = new ApiResponse<UserDTO>
                    {
                        Data = _mapper.Map<UserDTO>(user),
                        Message = null,
                        Status = StatusCodes.Status200OK,
                    };

                    return Succesresponse;
                }
                else
                {
                    var response = new ApiResponse<UserDTO>
                    {
                        Data = null,
                        Message = "User Not Verified",
                        Status = StatusCodes.Status400BadRequest,
                    };

                    return response;
                }
            }
        }

        public async Task<ApiResponse<bool>> GetResetCode(string userEmail)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
            {
                var response = new ApiResponse<bool>
                {
                    Data = false,
                    Message = "User Not Found",
                    Status = StatusCodes.Status404NotFound,
                };

                return response;
            }

            else
            {
                if (user.Status == ACCOUNT_STATUS.VERIFIED)
                {
                    Random rand = new Random();
                    string randomCode = rand.Next(10000, 99999).ToString();

                    user.PasswordResetCode = randomCode;

                    SMTPService smtpService = new SMTPService();

                    smtpService.SendEmail(user.Email, "Reset Code", $"<p>{user.PasswordResetCode}</p>");

                    _context.SaveChanges();

                    var response = new ApiResponse<bool>
                    {
                        Data = true,
                        Status = StatusCodes.Status200OK,
                        Message = "Code Sent Successfully",
                    };

                    return response;
                }
                else
                {
                    var response = new ApiResponse<bool>
                    {
                        Data = false,
                        Message = "User Not Verified",
                        Status = StatusCodes.Status400BadRequest,
                    };

                    return response;
                }
            }
        }

        public async Task<ApiResponse<UserDTO>> ResetPassword(string email, string code, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                var response = new ApiResponse<UserDTO>
                {
                    Data = null,
                    Message = "User Not Found",
                    Status = StatusCodes.Status404NotFound,
                };
                return response;
            }

            if (user.PasswordResetCode != code)
            {
                var response = new ApiResponse<UserDTO>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Invalid Code",
                };
                return response;
            }

            // Generate password reset token and reset password
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetResult = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (resetResult.Succeeded)
            {
                // Clear the reset code
                user.PasswordResetCode = null;
                await _context.SaveChangesAsync();

                var response = new ApiResponse<UserDTO>
                {
                    Data = null,
                    Status = StatusCodes.Status200OK,
                    Message = "Password Reset Successfully",
                };
                return response;
            }
            else
            {
                var errorMessages = string.Join(", ", resetResult.Errors.Select(e => e.Description));
                var response = new ApiResponse<UserDTO>
                {
                    Data = null,
                    Status = StatusCodes.Status400BadRequest,
                    Message = $"Password reset failed: {errorMessages}",
                };
                return response;
            }
        }

        public async Task<ApiResponse<UserToken>> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                var response = new ApiResponse<UserToken>
                {
                    Data = null,
                    Message = "User Not Found",
                    Status = StatusCodes.Status404NotFound,
                };
                return response;
            }

            // Use UserManager to verify password instead of BCrypt
            var passwordCheckResult = await _userManager.CheckPasswordAsync(user, password);

            if (passwordCheckResult && user.Status == ACCOUNT_STATUS.VERIFIED)
            {
                var response = new ApiResponse<UserToken>
                {
                    Data = _jwtService.GetUserToken(user),
                    Status = StatusCodes.Status200OK,
                    Message = "Login successful"
                };
                return response;
            }
            else if (passwordCheckResult && user.Status == ACCOUNT_STATUS.CODE_SENT)
            {
                var response = new ApiResponse<UserToken>
                {
                    Data = null,
                    Status = StatusCodes.Status403Forbidden,
                    Message = "User Not Verified",
                };
                return response;
            }
            else if (!passwordCheckResult)
            {
                var response = new ApiResponse<UserToken>
                {
                    Data = null,
                    Status = StatusCodes.Status401Unauthorized,
                    Message = "Wrong Password!",
                };
                return response;
            }
            else
            {
                var response = new ApiResponse<UserToken>
                {
                    Data = null,
                    Message = "Login failed",
                    Status = StatusCodes.Status400BadRequest,
                };
                return response;
            }
        }
    }
}

