using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORAA.DTO;
using ORAA.Services.Interfaces;

namespace ORAA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHistoryController : ControllerBase
    {
        private readonly IOrderHistoryService _orderHistoryService;

        public OrderHistoryController(IOrderHistoryService orderHistoryService)
        {
            _orderHistoryService = orderHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderHistoryService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _orderHistoryService.GetOrderById(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            var orders = await _orderHistoryService.GetOrdersByStatus(status);
            return Ok(orders);
        }

        [HttpGet("summary/total-spent")]
        public async Task<IActionResult> GetTotalSpent()
        {
            var total = await _orderHistoryService.GetTotalSpent();
            return Ok(total);
        }

        [HttpGet("summary/total-orders")]
        public async Task<IActionResult> GetTotalOrders()
        {
            var count = await _orderHistoryService.GetTotalOrders();
            return Ok(count);
        }

        [HttpGet("summary/count/{status}")]
        public async Task<IActionResult> GetCountByStatus(string status)
        {
            var count = await _orderHistoryService.GetCountByStatus(status);
            return Ok(count);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            var created = await _orderHistoryService.CreateOrder(dto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDto dto)
        {
            var updated = await _orderHistoryService.UpdateOrder(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _orderHistoryService.DeleteOrder(id);
            return deleted ? Ok() : NotFound();
        }
    }
}
