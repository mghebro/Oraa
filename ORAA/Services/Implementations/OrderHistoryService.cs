using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ORAA.Data;
using ORAA.DTO;
using ORAA.Enums;
using ORAA.Models;
using ORAA.Services.Interfaces;
using Stripe.Climate;
using System.Collections.Generic;

namespace ORAA.Services.Implementations
{
    public class OrderHistoryService : IOrderHistoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly UserManager<User> _userManager;

        public OrderHistoryService(DataContext context, IMapper mapper, IJWTService jwtService, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<List<OrderHistoryDto>> GetAllOrders()
        {
            var orders = await _context.orderHistories.ToListAsync();
            return _mapper.Map<List<OrderHistoryDto>>(orders);
        }

        public async Task<OrderHistoryDto?> GetOrderById(int id)
        {
            var order = await _context.orderHistories.FirstOrDefaultAsync(o => o.Id == id);
            return order != null ? _mapper.Map<OrderHistoryDto>(order) : null;
        }

        public async Task<List<OrderHistoryDto>> GetOrdersByStatus(string status)
        {
            if (!Enum.TryParse<ORDER_STATUS>(status, true, out var parsedStatus))
                throw new ArgumentException("Invalid status");

            var filtered = await _context.orderHistories
                                         .Where(o => o.Status == parsedStatus)
                                         .ToListAsync();

            return _mapper.Map<List<OrderHistoryDto>>(filtered);
        }

        public async Task<decimal> GetTotalSpent()
        {
            return await _context.orderHistories
                                 .Where(o => o.Status == ORDER_STATUS.DELIVERED)
                                 .SumAsync(o => o.Price);
        }

        public async Task<int> GetTotalOrders()
        {
            return await _context.orderHistories.CountAsync();
        }

        public async Task<int> GetCountByStatus(string status)
        {
            if (!Enum.TryParse<ORDER_STATUS>(status, true, out var parsedStatus))
                throw new ArgumentException("Invalid status");

            return await _context.orderHistories.CountAsync(o => o.Status == parsedStatus);
        }

        public async Task<OrderHistoryDto> CreateOrder(CreateOrderDto dto)
        {
            var order = _mapper.Map<OrderHistory>(dto);
            _context.orderHistories.Add(order);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderHistoryDto>(order);
        }

        public async Task<OrderHistoryDto?> UpdateOrder(int id, UpdateOrderDto dto)
        {
            var order = await _context.orderHistories.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return null;

            _mapper.Map(dto, order);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderHistoryDto>(order);
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = await _context.orderHistories.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return false;

            _context.orderHistories.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}