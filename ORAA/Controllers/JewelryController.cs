using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORAA.Core;
using ORAA.DTO;
using ORAA.Models;
using ORAA.Services.Interfaces;
namespace ORAA.Controllers;

[Route("api/jewelry")]
[ApiController]
public class JewelryController : ControllerBase
{
    private readonly IJewelryService _jewelryService;
    public JewelryController(IJewelryService jewelryService)
    {
        _jewelryService = jewelryService;
    }
    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<List<JeweleryDTO>>>> SearchJewelry([FromQuery] JewelerySearchRequest request)
    {
        var response = await _jewelryService.SearchJewelryAsync(request);
        return StatusCode(response.Status, response);
    }
}
