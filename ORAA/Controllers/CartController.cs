using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORAA.Core;
using ORAA.Models;
using ORAA.Request;
using ORAA.Services.Interfaces;
namespace ORAA.Controllers;

[Route("api/cart")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("add")]
    public async Task<ActionResult<ApiResponse<Cart>>> AddCart([FromBody] AddCart request)
    {
        var response = await _cartService.AddCartAsync(request);
        return StatusCode(response.Status, response);
    }

    [HttpPost("items/add")]
    public async Task<ActionResult<ApiResponse<CartItem>>> AddCartItem([FromBody] AddCartItem request)
    {
        var response = await _cartService.AddCartItemAsync(request);
        return StatusCode(response.Status, response);
    }

}
