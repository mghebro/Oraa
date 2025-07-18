
using ORAA.DTO;
using ORAA.Core;
using ORAA.Request;
namespace ORAA.Services.Interfaces;

public interface IProductDetails
{
    Task<ApiResponse<ProductDetailsDTO>> GetProductDetails(int productId);
    Task<ApiResponse<List<ProductDetailsDTO>>> GetAllProductDetails();
    Task<ApiResponse<bool>> UpdateProductDetails(int productId, AddProductDetailsRequest request);
    Task<ApiResponse<bool>> DeleteProductDetails(int productId);
    Task<ApiResponse<ProductDetailsDTO>> AddProductDetails(AddProductDetailsRequest request);
}
