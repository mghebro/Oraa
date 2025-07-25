﻿using ORAA.Core;
using ORAA.Models;
using ORAA.Request;

namespace ORAA.Services.Interfaces;
public interface ICartService
{
    Task<ApiResponse<Cart>> AddCartAsync(AddCart request);
    Task<ApiResponse<CartItem>> AddCartItemAsync(AddCartItem request);
    Task<ApiResponse<string>> DeleteCartItemAsync(int cartItemId);
    Task<ApiResponse<CartItem>> UpdateCartItemQuantityAsync(int cartItemId, int newQuantity);
}
