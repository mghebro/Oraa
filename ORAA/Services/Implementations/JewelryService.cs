using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORAA.Core;
using ORAA.Data;
using ORAA.DTO;
using ORAA.Models;
using ORAA.Services.Interfaces;

namespace ORAA.Services.Implementations
{
    public class JewelryService : IJewelryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public JewelryService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// GET JEWELRY WITH FILTERS /// GET JEWELRY WITH FILTERS /// GET JEWELRY WITH FILTERS
        public async Task<ApiResponse<List<JeweleryDTO>>> SearchJewelryAsync(
            JewelerySearchRequest request
        )
        {
            var query = _context
                .Jewelries.Include(j => j.Material)
                .Include(j => j.HandCraftMan)
                .Include(j => j.Affirmation)
                .Include(j => j.Ritual)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                var name = request.Name.ToLower();
                query = query.Where(j =>
                    EF.Functions.Like(j.Name.ToLower(), $"%{name}%")
                    || EF.Functions.Like(j.Description.ToLower(), $"%{name}%")
                );
            }

            if (!string.IsNullOrWhiteSpace(request.Category))
            {
                var category = request.Category.ToLower();
                query = query.Where(j => EF.Functions.Like(j.Category.ToLower(), $"%{category}%"));
            }

            if (request.MinPrice.HasValue)
                query = query.Where(j => j.Price >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(j => j.Price <= request.MaxPrice.Value);

            if (request.MaterialId.HasValue)
                query = query.Where(j => j.MaterialId == request.MaterialId.Value);

            if (!string.IsNullOrWhiteSpace(request.ChakraAlignment))
            {
                var chakra = request.ChakraAlignment.ToLower();
                query = query.Where(j =>
                    EF.Functions.Like(j.ChakraAlignment.ToLower(), $"%{chakra}%")
                );
            }

            if (request.IsOnSale.HasValue)
                query = query.Where(j => j.IsOnSale == request.IsOnSale.Value);

            if (request.HandCraftManId.HasValue)
                query = query.Where(j => j.HandCraftManId == request.HandCraftManId.Value);

            var jewelries = await query.ToListAsync();

            if (!jewelries.Any())
            {
                return new ApiResponse<List<JeweleryDTO>>
                {
                    Status = 404,
                    Data = null,
                    Message = "No jewelries found with the given filters.",
                };
            }

            var dtoList = _mapper.Map<List<JeweleryDTO>>(jewelries);

            return new ApiResponse<List<JeweleryDTO>>
            {
                Status = 200,
                Data = dtoList,
                Message = "Search successful",
            };
        }
    }
}
