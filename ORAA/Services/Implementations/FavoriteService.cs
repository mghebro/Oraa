using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ORAA.Core;
using ORAA.Data;
using ORAA.DTO;
using ORAA.Models;
using ORAA.Request;
using ORAA.Services.Interfaces;

namespace ORAA.Services.Implementations
{
    public class FavoriteService : IFavorite_Service
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FavoriteService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<FavoriteDTO>> AddFavorite(AddFavorite request)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.Name == request.Name);

            if (favorite != null)
            {
                return new ApiResponse<FavoriteDTO>
                {
                    Status = 400,
                    Message = "Favorite with this name already exists.",
                    Data = null
                };
            }

            var favoriteToAdd = _mapper.Map<Favorite>(request);
            _context.Favorites.Add(favoriteToAdd);
            await _context.SaveChangesAsync();

            return new ApiResponse<FavoriteDTO>
            {
                Status = 200,
                Message = "Favorite added successfully.",
                Data = _mapper.Map<FavoriteDTO>(favoriteToAdd)
            };
        }

        public async Task<ApiResponse<bool>> DeleteAllFavorites(int userId)
        {
            var favorites = await _context.Favorites
                .Where(f => f.UserId == userId)
                .ToListAsync();

            if (favorites.Count == 0)
            {
                return new ApiResponse<bool>
                {
                    Status = 404,
                    Message = "No favorites found for this user.",
                    Data = false
                };
            }

            _context.Favorites.RemoveRange(favorites);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Status = 200,
                Message = "All favorites deleted successfully.",
                Data = true
            };
        }

        public async Task<ApiResponse<bool>> DeleteFavorite(int id)
        {
            var favorite = await _context.Favorites.FirstOrDefaultAsync(f => f.Id == id);

            if (favorite == null)
            {
                return new ApiResponse<bool>
                {
                    Status = 404,
                    Message = "Favorite not found.",
                    Data = false
                };
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>
            {
                Status = 200,
                Message = "Favorite deleted successfully.",
                Data = true
            };
        }

        public async Task<ApiResponse<List<FavoriteDTO>>> GetFavorites(FavoriteDTO favorites)
        {
            var favoriteList = await _context.Favorites
                .Where(f => f.UserId == favorites.UserId)
                .Select(f => _mapper.Map<FavoriteDTO>(f))
                .ToListAsync();

            if (favoriteList.Count == 0)
            {
                return new ApiResponse<List<FavoriteDTO>>
                {
                    Status = 404,
                    Message = "No favorites found for this user.",
                    Data = null
                };
            }

            return new ApiResponse<List<FavoriteDTO>>
            {
                Status = 200,
                Message = "Favorites retrieved successfully.",
                Data = favoriteList
            };
        }

        public async Task<ApiResponse<List<FavoriteDTO>>> SortByName(FavoriteDTO favorite)
        {
            var sortedList = await _context.Favorites
                .Where(f => f.UserId == favorite.UserId)
                .OrderBy(f => f.Name)
                .Select(f => _mapper.Map<FavoriteDTO>(f))
                .ToListAsync();

            return new ApiResponse<List<FavoriteDTO>>
            {
                Status = 200,
                Message = "Favorites sorted by name (ascending).",
                Data = sortedList
            };
        }

        public async Task<ApiResponse<List<FavoriteDTO>>> SortByNameDesc(FavoriteDTO favorite)
        {
            var sortedList = await _context.Favorites
                .Where(f => f.UserId == favorite.UserId)
                .OrderByDescending(f => f.Name)
                .Select(f => _mapper.Map<FavoriteDTO>(f))
                .ToListAsync();

            return new ApiResponse<List<FavoriteDTO>>
            {
                Status = 200,
                Message = "Favorites sorted by name (descending).",
                Data = sortedList
            };
        }

        public async Task<ApiResponse<List<FavoriteDTO>>> SortByPrice(FavoriteDTO favorite)
        {
            var sortedList = await _context.Favorites
                .Where(f => f.UserId == favorite.UserId)
                .Include(f => f.Jewelry)
                .OrderBy(f => f.Jewelry.Price)
                .Select(f => _mapper.Map<FavoriteDTO>(f))
                .ToListAsync();

            return new ApiResponse<List<FavoriteDTO>>
            {
                Status = 200,
                Message = "Favorites sorted by price (ascending).",
                Data = sortedList
            };
        }

        public async Task<ApiResponse<List<FavoriteDTO>>> SortByPriceDesc(FavoriteDTO favorite)
        {
            var sortedList = await _context.Favorites
                .Where(f => f.UserId == favorite.UserId)
                .Include(f => f.Jewelry)
                .OrderByDescending(f => f.Jewelry.Price)
                .Select(f => _mapper.Map<FavoriteDTO>(f))
                .ToListAsync();

            return new ApiResponse<List<FavoriteDTO>>
            {
                Status = 200,
                Message = "Favorites sorted by price (descending).",
                Data = sortedList
            };
        }
    }
}
