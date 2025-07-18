using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORAA.Request;
using ORAA.Services.Interfaces;

namespace ORAA.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CollectionsController : ControllerBase
{
    private readonly ICollectionsService _collectionsService;

    public CollectionsController(ICollectionsService collectionsService)
    {
        _collectionsService = collectionsService;
    }

    [HttpPost("create-collection")]
    //[Authorize]
    public async Task<IActionResult> CreateCollection(AddCollection request)
    {
        try
        {
            var response = await _collectionsService.CreateCollection(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("add-collection-jewelery/{jeweletyId}")]
    //[Authorize]
    public async Task<IActionResult> AddCollectionJewelery(int collectionId, int jeweletyId)
    {
        try
        {
            var response = await _collectionsService.AddCollectionJewelery(collectionId, jeweletyId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("add-collection-crystal/{crystalId}")]
    //[Authorize]
    public async Task<IActionResult> AddCollectionCrystal(int collectionId, int crystalId)
    {
        try
        {
            var response = await _collectionsService.AddCollectionCrystal(collectionId, crystalId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("get-all-collections")]
    //[Authorize]
    public async Task<IActionResult> GetAllCollections()
    {
        try
        {
            var response = await _collectionsService.GetAllCollections();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("get-collection/{collectionId}")]
    //[Authorize]
    public async Task<IActionResult> GetCollectionById(int collectionId)
    {
        try
        {
            var response = await _collectionsService.GetCollectionById(collectionId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("update-collection/{collectionId}")]
    //[Authorize]
    public async Task<IActionResult> UpdateCollection(int collectionId, AddCollection request)
    {
        try
        {
            var response = await _collectionsService.UpdateCollection(collectionId, request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("delete-collection/{collectionId}")]
    //[Authorize]
    public async Task<IActionResult> RemoveCollection(int collectionId)
    {
        try
        {
            var response = await _collectionsService.RemoveCollection(collectionId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("delete-collection-jewelery/{collectionId}")]
    //[Authorize]
    public async Task<IActionResult> RemoveCollectionJewelety(int collectionId)
    {
        try
        {
            var response = await _collectionsService.RemoveCollectionJewelety(collectionId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }
    
    [HttpDelete("delete-collection-crystal/{collectionId}")]
    //[Authorize]
    public async Task<IActionResult> RemoveCollectionCrystal(int collectionId)
    {
        try
        {
            var response = await _collectionsService.RemoveCollectionCrystal(collectionId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
        }
    }
}
