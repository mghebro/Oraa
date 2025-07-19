using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORAA.DTO;
using ORAA.Services.Interfaces;

namespace ORAA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{

    private readonly IAccountService _accountService;
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    // ---------------- Shipping Addresses ----------------

    [HttpGet("get-addresses")]
    public async Task<IActionResult> GetAddresses([FromQuery] int userId)
    {
        var addresses = await _accountService.GetUserAddresses(userId);
        return Ok(addresses);
    }

    [HttpPost("add-addresses")]
    public async Task<IActionResult> AddAddress([FromQuery] int userId, [FromBody] ShippingAddressesDTO dto)
    {
        var added = await _accountService.AddShippingAddress(userId, dto);
        return CreatedAtAction(nameof(GetAddresses), new { userId }, added);
    }

    [HttpPut("update-addresses/{addressId}")]
    public async Task<IActionResult> UpdateAddress([FromQuery] int userId, int addressId, [FromBody] ShippingAddressesDTO dto)
    {
        var updated = await _accountService.UpdateShippingAddress(userId, addressId, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("delete-addresses/{addressId}")]
    public async Task<IActionResult> DeleteAddress([FromQuery] int userId, int addressId)
    {
        var deleted = await _accountService.DeleteShippingAddress(userId, addressId);
        if (!deleted) return NotFound();
        return NoContent();
    }

    // ---------------- Purchases ----------------
    [HttpGet("get-purchases")]
    public async Task<IActionResult> GetPurchases([FromQuery] int userId)
    {
        var result = await _accountService.GetPurchase(userId);
        return Ok(result);
    }

    [HttpPost("add-purchases")]
    public async Task<IActionResult> AddPurchase([FromQuery] int userId, [FromBody] CreatePurchaseDTO dto)
    {
        var created = await _accountService.AddPurchase(userId, dto);
        return CreatedAtAction(nameof(GetPurchases), new { userId }, created);
    }

    [HttpPut("update-purchases/{purchaseId}")]
    public async Task<IActionResult> UpdatePurchase([FromQuery] int userId, int purchaseId, [FromBody] CreatePurchaseDTO dto)
    {
        var updated = await _accountService.UpdatePurchase(userId, purchaseId, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("delete-purchases/{purchaseId}")]
    public async Task<IActionResult> DeletePurchase([FromQuery] int userId, int purchaseId)
    {
        var deleted = await _accountService.DeletePurchase(userId, purchaseId);
        if (!deleted) return NotFound();
        return NoContent();
    }



}