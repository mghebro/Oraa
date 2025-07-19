using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ORAA.Core;
using ORAA.Data;
using ORAA.DTO;
using ORAA.Models;
using ORAA.Request;
using ORAA.Services.Interfaces;
using ORAA.Validations;

namespace ORAA.Services.Implementations
{
    public class ProductDetailsService : IProductDetails
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductDetailsService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<ProductDetailsDTO>> AddProductDetails(AddProductDetailsRequest request)
        {
            try
            {
                var productDetails = _mapper.Map<ProductDetails>(request);
                var validator = new ProductDetailsValidator();
                var validationResult = validator.Validate(productDetails);

                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return new ApiResponse<ProductDetailsDTO>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = $"Validation failed: {errors}",
                        Data = null
                    };
                }

                _context.ProductDetails.Add(productDetails);
                await _context.SaveChangesAsync();

                return new ApiResponse<ProductDetailsDTO>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Product details added successfully",
                    Data = _mapper.Map<ProductDetailsDTO>(productDetails)
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductDetailsDTO>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<bool>> DeleteProductDetails(int productId)
        {
            try
            {
                var productDetails = await _context.ProductDetails.FindAsync(productId);
                if (productDetails == null)
                {
                    return new ApiResponse<bool>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Product details not found",
                        Data = false
                    };
                }

                _context.ProductDetails.Remove(productDetails);
                await _context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Product details deleted successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Internal server error: {ex.Message}",
                    Data = false
                };
            }
        }

        public async Task<ApiResponse<List<ProductDetailsDTO>>> GetAllProductDetails()
        {
            try
            {
                var productDetailsList = await _context.ProductDetails
                    .Include(pd => pd.Jewelery)
                    .Include(pd => pd.Crystal)
                    .Include(pd => pd.Affirmation)
                    .Include(pd => pd.Favorite)
                    .Include(pd => pd.Cart)
                    .Include(pd => pd.Ritual)
                    .ToListAsync();

                var productDetailsDTOs = _mapper.Map<List<ProductDetailsDTO>>(productDetailsList);

                return new ApiResponse<List<ProductDetailsDTO>>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Product details retrieved successfully",
                    Data = productDetailsDTOs
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<ProductDetailsDTO>>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<ProductDetailsDTO>> GetProductDetails(int productId)
        {
            try
            {
                var productDetails = await _context.ProductDetails
                    .Include(pd => pd.Jewelery)
                    .Include(pd => pd.Crystal)
                    .Include(pd => pd.Affirmation)
                    .Include(pd => pd.Favorite)
                    .Include(pd => pd.Cart)
                    .Include(pd => pd.Ritual)
                    .FirstOrDefaultAsync(pd => pd.Id == productId);

                if (productDetails == null)
                {
                    return new ApiResponse<ProductDetailsDTO>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Product details not found",
                        Data = null
                    };
                }

                return new ApiResponse<ProductDetailsDTO>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Product details retrieved successfully",
                    Data = _mapper.Map<ProductDetailsDTO>(productDetails)
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductDetailsDTO>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Internal server error: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<bool>> UpdateProductDetails(int productId, AddProductDetailsRequest request)
        {
            try
            {
                var existingProductDetails = await _context.ProductDetails.FindAsync(productId);
                if (existingProductDetails == null)
                {
                    return new ApiResponse<bool>
                    {
                        Status = StatusCodes.Status404NotFound,
                        Message = "Product details not found",
                        Data = false
                    };
                }

                var updatedProductDetails = _mapper.Map<ProductDetails>(request);
                var validator = new ProductDetailsValidator();
                var validationResult = validator.Validate(updatedProductDetails);

                if (!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return new ApiResponse<bool>
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Message = $"Validation failed: {errors}",
                        Data = false
                    };
                }

                // Update properties
                existingProductDetails.Title = updatedProductDetails.Title;
                existingProductDetails.Description = updatedProductDetails.Description;
                existingProductDetails.Price = updatedProductDetails.Price;
                existingProductDetails.Quantity = updatedProductDetails.Quantity;

                _context.ProductDetails.Update(existingProductDetails);
                await _context.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    Status = StatusCodes.Status200OK,
                    Message = "Product details updated successfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Message = $"Internal server error: {ex.Message}",
                    Data = false
                };
            }
        }
    }
}