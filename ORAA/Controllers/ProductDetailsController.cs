using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORAA.Request;
using ORAA.Services.Implementations;
using ORAA.Services.Interfaces;
using ORAA.DTO;
namespace ORAA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetails _ProductDetailsService;
        public ProductDetailsController(IProductDetails ProductDetailsService)
        {
            _ProductDetailsService = ProductDetailsService;
        }
        [HttpPost("create-collection")]
        public async Task<IActionResult> AddProductDetails(AddProductDetailsRequest request)
        {
            try
            {
                var response = await _ProductDetailsService.AddProductDetails(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-product-details/{productId}")]
        public async Task<IActionResult> GetProductDetails(int productId)
        {
            try
            {
                var response = await _ProductDetailsService.GetProductDetails(productId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("get-all-product-details")]
        public async Task<IActionResult> GetAllProductDetails()
        {
            try
            {
                var response = await _ProductDetailsService.GetAllProductDetails();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("update-product-details/{productId}")]
        public async Task<IActionResult> UpdateProductDetails(int productId, [FromBody] AddProductDetailsRequest request)
        {
            try
            {
                var response = await _ProductDetailsService.UpdateProductDetails(productId, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("delete-product-details/{productId}")]
        public async Task<ActionResult> DeleteProductDetails(int productId)
        {
            try
            {
                var response = await _ProductDetailsService.DeleteProductDetails(productId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
                
            }
        }
    }
}
