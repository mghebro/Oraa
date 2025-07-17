using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<AddCartItem> _validator;

        public CartService(DataContext context, IMapper mapper, IValidator<AddCartItem> validator)
        {
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        /// ADD CART   ///  ADD CART   ///  ADD CART   ///  ADD CART   ///  ADD CART
        public async Task<ApiResponse<Cart>> AddCartAsync(AddCart request)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == request.UserId);
            if (!userExists)
            {
                return new ApiResponse<Cart>
                {
                    Status = 404,
                    Data = null,
                    Message = "User not found."
                };
            }
            if (request.DiscountCodeId.HasValue)
            {
                var discountExists = await _context.DiscountCodes.AnyAsync(d => d.Id == request.DiscountCodeId.Value);
                if (!discountExists)
                {
                    return new ApiResponse<Cart>
                    {
                        Status = 404,
                        Data = null,
                        Message = "Discount not found."
                    };
                }
            }
            if (request.GiftCardId.HasValue)
            {
                var giftCardExists = await _context.GIftCards.AnyAsync(g => g.Id == request.GiftCardId.Value);
                if (!giftCardExists)
                {
                    return new ApiResponse<Cart>
                    {
                        Status = 404,
                        Data = null,
                        Message = "Gift card not found."
                    };
                }
            }
            if (request.Subtotal < 0 || request.DiscountAmount < 0 || request.Total < 0)
            {
                return new ApiResponse<Cart>
                {
                    Status = 400,
                    Data = null,
                    Message = "Subtotal, DiscountAmount, and Total must be non-negative."
                };
            }

            var calculatedTotal = request.Subtotal - request.DiscountAmount;
            if (calculatedTotal < 0) calculatedTotal = 0;

            if (request.Total != calculatedTotal)
            {
                return new ApiResponse<Cart>
                {
                    Status = 400,
                    Data = null,
                    Message = $"Total is invalid. Expected {calculatedTotal}, but got {request.Total}."
                };
            }

            var cart = _mapper.Map<Cart>(request);

            cart.Total = calculatedTotal;
            cart.ExpiresAt = DateTime.UtcNow.AddDays(10);  // EXPIRATION TIME SET TO 10 DAY
            cart.CreatedAt = DateTime.UtcNow;
            cart.UpdatedAt = DateTime.UtcNow;

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
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));

                return new ApiResponse<CartItem>
                {
                    Status = 400,
                    Data = null,
                    Message = $"Validation failed: {errors}"
                };
            }
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
            var jewelry = await _context.Jewelries.FindAsync(request.JewelryId);
            if (jewelry == null)
            {
                return new ApiResponse<CartItem>
                {
                    Status = 404,
                    Data = null,
                    Message = "Jewelry not found."
                };
            }

            var expectedTotalPrice = request.UnitPrice * request.Quantity;
            if (request.TotalPrice != expectedTotalPrice)
            {
                return new ApiResponse<CartItem>
                {
                    Status = 400,
                    Data = null,
                    Message = $"Invalid total price. Expected: {expectedTotalPrice}, Received: {request.TotalPrice}"
                };
            }

            var cartItem = _mapper.Map<CartItem>(request);

            cartItem.TotalPrice = expectedTotalPrice;
            cartItem.CreatedAt = DateTime.UtcNow;
            cartItem.UpdatedAt = DateTime.UtcNow;

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();

            return new ApiResponse<CartItem>
            {
                Status = 200,
                Data = cartItem,
                Message = "Cart item added successfully."
            };
        }
        /// UPDATE CART ITEM QUANTITY   ///  UPDATE CART ITEM QUANTITY   ///  UPDATE CART ITEM QUANTITY
        public async Task<ApiResponse<CartItem>> UpdateCartItemQuantityAsync(int cartItemId, int newQuantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return new ApiResponse<CartItem>
                {
                    Status = 404,
                    Data = null,
                    Message = "Cart item not found."
                };
            }

            if (newQuantity < 0)
            {
                return new ApiResponse<CartItem>
                {
                    Status = 400,
                    Data = null,
                    Message = "Quantity cannot be negative."
                };
            }

            if (newQuantity == 0)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();

                return new ApiResponse<CartItem>
                {
                    Status = 200,
                    Data = null,
                    Message = "Cart item deleted successfully because quantity was set to 0."
                };
            }

            cartItem.Quantity = newQuantity;
            cartItem.TotalPrice = cartItem.UnitPrice * newQuantity;
            cartItem.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return new ApiResponse<CartItem>
            {
                Status = 200,
                Data = cartItem,
                Message = "Cart item quantity updated successfully."
            };
        }
        /// DELETE CART ITEM   ///  DELETE CART ITEM   ///  DELETE CART ITEM   ///  DELETE CART ITEM
        public async Task<ApiResponse<string>> DeleteCartItemAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
            {
                return new ApiResponse<string>
                {
                    Status = 404,
                    Data = null,
                    Message = "Cart item not found."
                };
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return new ApiResponse<string>
            {
                Status = 200,
                Data = null,
                Message = "Cart item removed successfully."
            };
        }
    }
}
