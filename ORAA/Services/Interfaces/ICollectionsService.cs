using ORAA.Core;
using ORAA.DTO;
using ORAA.Request;

namespace ORAA.Services.Interfaces;

public interface ICollectionsService
{
    Task<ApiResponse<CollectionsDTO>> CreateCollection(AddCollection request);
    Task<ApiResponse<CollectionsDTO>> AddCollectionJewelery(int collectionId, int jeweletyId);
    Task<ApiResponse<CollectionsDTO>> AddCollectionCrystal(int collectionId, int crystalId);
    Task<ApiResponse<List<CollectionsDTO>>> GetAllCollections();
    Task<ApiResponse<CollectionsDTO>> GetCollectionById(int collectionId);
    Task<ApiResponse<CollectionsDTO>> UpdateCollection(int collectionId, AddCollection request);
    Task<ApiResponse<CollectionsDTO>> RemoveCollection(int collectionId);
    Task<ApiResponse<CollectionsDTO>> RemoveCollectionJewelety(int collectionId);
    Task<ApiResponse<CollectionsDTO>> RemoveCollectionCrystal(int collectionId);
}
