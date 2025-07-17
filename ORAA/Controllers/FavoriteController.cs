using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORAA.Core;
using ORAA.DTO;
using ORAA.Request;
using ORAA.Services.Interfaces;

namespace ORAA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavorite_Service _favoriteService;

        public FavoriteController(IFavorite_Service favoriteService)
        {
            _favoriteService = favoriteService;
        }


        [HttpPost("add-favorite")]
        public async Task<ActionResult<ApiResponse<FavoriteDTO>>> AddFavorite([FromBody] AddFavorite request)
        {
            var response = await _favoriteService.AddFavorite(request);
            return Ok(response);
        }

        [HttpGet("get-favorites")]

        public async Task<ActionResult<ApiResponse<List<FavoriteDTO>>>> GetFavorites([FromQuery] FavoriteDTO favorites)
        {
            var response = await _favoriteService.GetFavorites(favorites);
            return Ok(response);
        }

        [HttpDelete("delete-all-favorites/{userId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAllFavorites(int userId)
        {
            var response = await _favoriteService.DeleteAllFavorites(userId);
            return Ok(response);
        }
        [HttpDelete("delete-favorite/{id}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFavorite(int id)
        {
            var response = await _favoriteService.DeleteFavorite(id);
            return Ok(response);
        }
        [HttpGet("sort-by-price")]
        public async Task<ActionResult<ApiResponse<List<FavoriteDTO>>>> SortByPrice([FromQuery] FavoriteDTO favorite)
        {
            var response = await _favoriteService.SortByPrice(favorite);
            return Ok(response);
        }
        [HttpGet("sort-by-name")]
        public async Task<ActionResult<ApiResponse<List<FavoriteDTO>>>> SortByName([FromQuery] FavoriteDTO favorite)
        {
            var response = await _favoriteService.SortByName(favorite);
            return Ok(response);
        }
        [HttpGet("sort-by-price-desc")]
        public async Task<ActionResult<ApiResponse<List<FavoriteDTO>>>> SortByPriceDesc([FromQuery] FavoriteDTO favorite)
        {
            var response = await _favoriteService.SortByPriceDesc(favorite);
            return Ok(response);
        }
        [HttpGet("sort-by-name-desc")]
        public async Task<ActionResult<ApiResponse<List<FavoriteDTO>>>> SortByNameDesc([FromQuery] FavoriteDTO favorite)
        {
            var response = await _favoriteService.SortByNameDesc(favorite);
            return Ok(response);
        }
      
    }
}
