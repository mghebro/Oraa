using ORAA.Core;
using ORAA.DTO;
using ORAA.Request;

namespace ORAA.Services.Interfaces;

public interface IUserService
{
    Task<ApiResponse<UserDTO>> RegisterUser(AddUser request);
    Task<ApiResponse<bool>> Verify(string email, string code);
    Task<ApiResponse<UserDTO>> GetProfile(int id);
    Task<ApiResponse<bool>> GetResetCode(string userEmail);
    Task<ApiResponse<UserDTO>> ResetPassword(string email, string code, string newPassword);
    Task<ApiResponse<UserToken>> Login(string email, string password);
    Task<ApiResponse<UserDTO>> UpdateUser(int id, string changeParametr, string toChange);

    Task<ApiResponse<UserDTO>> DeleteUser(int id);
}



