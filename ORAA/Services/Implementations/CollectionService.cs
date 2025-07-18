using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ORAA.Core;
using ORAA.Data;
using ORAA.DTO;
using ORAA.Models;
using ORAA.Request;
using ORAA.Services.Interfaces;
using ORAA.Validations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ORAA.Services.Implementations;

public class CollectionService : ICollectionsService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CollectionService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<CollectionsDTO>> CreateCollection(AddCollection request)
    {
        var collection = _mapper.Map<Collections>(request);
        var valdator = new CollectionValidator();
        var result = valdator.Validate(collection);

        if (!result.IsValid)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
            var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                StatusCodes.Status400BadRequest,
                $"errors: {errors}",
                null
            );
            return response;
        }
        else
        {
            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();

            var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                StatusCodes.Status200OK,
                null,
                _mapper.Map<CollectionsDTO>(collection)
            );
            return response;
        }
    }

    public async Task<ApiResponse<CollectionsDTO>> AddCollectionCrystal(int collectionId, int crystalId)
    {
        var collection = await _context.Collections
            .Include(c => c.Crystal)
            .Include(c => c.Jewelery)
            .FirstOrDefaultAsync(c => c.Id == collectionId);

        if (collection == null)
        {
            return ApiResponseFactory.CreateResponse<CollectionsDTO>(
                StatusCodes.Status404NotFound,
                "Collection not found",
                null
            );
        }
        else
        {
            if (collection.Crystal != null)
            {
                return ApiResponseFactory.CreateResponse<CollectionsDTO>(
                    StatusCodes.Status400BadRequest,
                    "Collection already has a crystal",
                    null
                );
            }
            else if (collection.Jewelery != null)
            {
                return ApiResponseFactory.CreateResponse<CollectionsDTO>(
                    StatusCodes.Status400BadRequest,
                    "Collection already has a jewelery, can't add crystal",
                    null
                );
            }
            else
            {
                var crystal = await _context.Crystals
                .FirstOrDefaultAsync(x => x.Id == crystalId);

                if (crystal == null)
                {
                    return ApiResponseFactory.CreateResponse<CollectionsDTO>(
                        StatusCodes.Status404NotFound,
                        "Crystal not found",
                        null
                    );
                }
                else
                {
                    collection.Crystal = crystal;
                    await _context.SaveChangesAsync();

                    var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                        StatusCodes.Status200OK,
                        null,
                        _mapper.Map<CollectionsDTO>(collection)
                    );
                    return response;
                }
            }           

        }
    }

    public async Task<ApiResponse<CollectionsDTO>> AddCollectionJewelery(int collectionId, int jeweletyId)
    {
        var collection = await _context.Collections
            .Include(c => c.Jewelery)
            .Include(c => c.Crystal)
            .FirstOrDefaultAsync(c => c.Id == collectionId);

        if (collection == null)
        {
            return ApiResponseFactory.CreateResponse<CollectionsDTO>(
                StatusCodes.Status404NotFound,
                "Collection not found",
                null
            );
        }
        else
        {
            if (collection.Crystal != null)
            {
                return ApiResponseFactory.CreateResponse<CollectionsDTO>(
                    StatusCodes.Status400BadRequest,
                    "Collection already has a crystal, can't add jewelery",
                    null
                );
            }
            else if (collection.Jewelery != null)
            {
                return ApiResponseFactory.CreateResponse<CollectionsDTO>(
                    StatusCodes.Status400BadRequest,
                    "Collection already has a jewelery",
                    null
                );
            }
            else
            {
                var jewelery = await _context.Jewelries
                .FirstOrDefaultAsync(x => x.Id == jeweletyId);

                if (jewelery == null)
                {
                    return ApiResponseFactory.CreateResponse<CollectionsDTO>(
                        StatusCodes.Status404NotFound,
                        "Jewelery not found",
                        null
                    );
                }
                else
                {
                    collection.Jewelery = jewelery;
                    await _context.SaveChangesAsync();

                    var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                        StatusCodes.Status200OK,
                        null,
                        _mapper.Map<CollectionsDTO>(collection)
                    );
                    return response;
                }
            }         

        }
    }

    public async Task<ApiResponse<List<CollectionsDTO>>> GetAllCollections()
    {
        var collections = await _context.Collections
            .Include(c => c.Jewelery)
            .Include(c => c.Crystal)
            .ToListAsync();

        var response = ApiResponseFactory.CreateResponse<List<CollectionsDTO>>(
                StatusCodes.Status200OK,
                null,
                _mapper.Map<List<CollectionsDTO>>(collections)
            );
        return response;
    }

    public async Task<ApiResponse<CollectionsDTO>> GetCollectionById(int collectionId)
    {
        var collection = await _context.Collections
            .Include(c => c.Jewelery)
            .Include(c => c.Crystal)
            .FirstOrDefaultAsync(x => x.Id == collectionId);

        if (collection == null)
        {
            var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                StatusCodes.Status404NotFound,
                "collection not found",
                null
            );
            return response;
        }
        else
        {
            var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                StatusCodes.Status200OK,
                null,
                _mapper.Map<CollectionsDTO>(collection)
            );
            return response;
        }
    }
    public async Task<ApiResponse<CollectionsDTO>> UpdateCollection(int collectionId, AddCollection request)
    {
        var collection = await _context.Collections
            .FirstOrDefaultAsync(x => x.Id == collectionId);

        if (collection == null)
        {
            var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                StatusCodes.Status404NotFound,
                "collection not found",
                null
            );
            return response;
        }
        else
        {
            var newCollection = _mapper.Map<Collections>(request);
            var valdator = new CollectionValidator();
            var result = valdator.Validate(newCollection);

            if (!result.IsValid)
            {
                string errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                    StatusCodes.Status400BadRequest,
                    $"errors: {errors}",
                    null
                );
                return response;
            }
            else
            {
                collection.Name = newCollection.Name;
                collection.Description = newCollection.Description;
                collection.PhotoURL = newCollection.PhotoURL;
                collection.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                    StatusCodes.Status200OK,
                    null,
                    _mapper.Map<CollectionsDTO>(collection)
                );
                return response;
            }
        }
    }

    public async Task<ApiResponse<CollectionsDTO>> RemoveCollection(int collectionId)
    {
        var collection = await _context.Collections
            .FirstOrDefaultAsync(x => x.Id == collectionId);

        if (collection == null)
        {
            var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                StatusCodes.Status404NotFound,
                "collection not found",
                null
            );
            return response;
        }
        else
        {
            _context.Collections.Remove(collection);
            await _context.SaveChangesAsync();
            var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                StatusCodes.Status200OK,
                "Collection removed successfully",
                _mapper.Map<CollectionsDTO>(collection)
            );
            return response;
        }
    }

    public async Task<ApiResponse<CollectionsDTO>> RemoveCollectionCrystal(int collectionId)
    {
        var collection = await _context.Collections
            .Include(c => c.Crystal)
            .FirstOrDefaultAsync(x => x.Id == collectionId);

        if (collection == null)
        {
            var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                StatusCodes.Status404NotFound,
                "collection not found",
                null
            );
            return response;
        }
        else
        {
            if (collection.Crystal == null)
            {
                var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                    StatusCodes.Status404NotFound,
                    "crystal not found",
                    null
                );
                return response;
            }
            else
            {
                collection.Crystal = null;
                await _context.SaveChangesAsync();
                var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                    StatusCodes.Status200OK,
                    "Crystal removed successfully",
                    _mapper.Map<CollectionsDTO>(collection)
                );
                return response;
            }
        }
    }

    public async Task<ApiResponse<CollectionsDTO>> RemoveCollectionJewelety(int collectionId)
    {
        var collection = await _context.Collections
            .Include(c => c.Jewelery)
            .FirstOrDefaultAsync(x => x.Id == collectionId);

        if (collection == null)
        {
            var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                StatusCodes.Status404NotFound,
                "collection not found",
                null
            );
            return response;
        }
        else
        {
            if (collection.Jewelery == null)
            {
                var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                    StatusCodes.Status404NotFound,
                    "jewelery not found",
                    null
                );
                return response;
            }
            else
            {
                collection.Jewelery = null;
                await _context.SaveChangesAsync();
                var response = ApiResponseFactory.CreateResponse<CollectionsDTO>(
                    StatusCodes.Status200OK,
                    "Jewelery removed successfully",
                    _mapper.Map<CollectionsDTO>(collection)
                );
                return response;
            }
        }
    }

}
