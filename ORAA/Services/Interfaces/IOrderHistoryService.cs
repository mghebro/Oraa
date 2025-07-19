using Microsoft.OpenApi.Models;
using ORAA.Core;
using ORAA.DTO;
using ORAA.Models;

namespace ORAA.Services.Interfaces
{
    public interface IOrderHistoryService
    {
        Task<List<OrderHistoryDto>> GetAllOrders();
        Task<OrderHistoryDto> GetOrderById(int id);
        Task<List<OrderHistoryDto>> GetOrdersByStatus(string status);
        Task<decimal> GetTotalSpent();
        Task<int> GetTotalOrders();
        Task<int> GetCountByStatus(string status);
        Task<OrderHistoryDto> CreateOrder(CreateOrderDto dto);
        Task<OrderHistoryDto> UpdateOrder(int id, UpdateOrderDto dto);
        Task<bool> DeleteOrder(int id);


    }
}
