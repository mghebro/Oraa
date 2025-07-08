using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ORAA.Core;
using ORAA.Data;
using ORAA.Models;
using ORAA.Request;
using ORAA.Services.Interfaces;

namespace ORAA.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CartService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// ADD CART   ///  ADD CART   ///  ADD CART   ///  ADD CART   ///  ADD CART
        public async Task<ApiResponse<Cart>> AddCartAsync(AddCart request)
        {
            var cart = new Cart
            {
                UserId = request.UserId,
                Subtotal = request.Subtotal,
                DiscountAmount = request.DiscountAmount,
                Total = request.Total,
                DiscountCodeId = request.DiscountCodeId,
                GiftCardId = request.GiftCardId,
                ExpiresAt = request.ExpiresAt,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Carts.Add(cart);
           await _context.SaveChangesAsync();



            return new ApiResponse<Cart>
            {
                Status = 200,
                Data = cart,
                Message = "Cart added successfully."
            };
        }

        ///  ADD CART ITEM   ///  ADD CART ITEM   ///  ADD CART ITEM   ///  ADD CART ITEM
        public async Task<ApiResponse<CartItem>> AddCartItemAsync(AddCartItem request)
        {
            var cart = await _context.Carts.FindAsync(request.CartId);

            if (cart == null)
            {
                return new ApiResponse<CartItem>
                {
                    Status = 404,
                    Data = null,
                    Message = "Cart not found."
                };
            }

            var cartItem = new CartItem
            {
                CartId = request.CartId,
                JewelryId = request.JewelryId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                TotalPrice = request.TotalPrice,
                Engraving = request.Engraving,
                Size = request.Size,
                CustomizationNotes = request.CustomizationNotes,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

        

            return new ApiResponse<CartItem>
            {
                Status = 200,
                Data = cartItem,
                Message = "Cart item added successfully."
            };
        } 
    }
}
