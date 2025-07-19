using ORAA.DTO;

namespace ORAA.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<ShippingAddressesDTO>> GetUserAddresses(int userId);
        Task<ShippingAddressesDTO> AddShippingAddress(int userId, ShippingAddressesDTO dto);
        Task<bool> UpdateShippingAddress(int userId, int addressId, ShippingAddressesDTO dto);
        Task<bool> DeleteShippingAddress(int userId, int addressId);

        // Purchase Methods
        Task<List<PurchaseDTO>> GetPurchase(int userId);
        Task<PurchaseDTO> AddPurchase(int userId, CreatePurchaseDTO dto);
        Task<bool> UpdatePurchase(int userId, int purchaseId, CreatePurchaseDTO dto);
        Task<bool> DeletePurchase(int userId, int purchaseId);
    }
}
