using ORAA.Core;
using ORAA.DTO;
using ORAA.Request;

namespace ORAA.Services.Interfaces;

public interface IFavorite_Service
{
    Task<ApiResponse<FavoriteDTO>> AddFavorite(AddFavorite request);
    Task<ApiResponse<List<FavoriteDTO>>> GetFavorites(FavoriteDTO favorites);
    Task<ApiResponse<bool>> DeleteFavorite(int id);
    Task<ApiResponse<bool>> DeleteAllFavorites(int userId);
    Task<ApiResponse<List<FavoriteDTO>>> SortByPrice(FavoriteDTO favorite);
    Task<ApiResponse<List<FavoriteDTO>>> SortByName(FavoriteDTO favorite);
    Task<ApiResponse<List<FavoriteDTO>>> SortByPriceDesc(FavoriteDTO favorite);
    Task<ApiResponse<List<FavoriteDTO>>> SortByNameDesc(FavoriteDTO favorite);

}
