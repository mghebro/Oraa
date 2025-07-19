using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ORAA.Data;
using ORAA.DTO;
using ORAA.Models;
using ORAA.Services.Interfaces;

namespace ORAA.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly UserManager<User> _userManager;

        public AccountService(DataContext context, IMapper mapper, IJWTService jwtService, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        public async Task<List<ShippingAddressesDTO>> GetUserAddresses(int userId)
        {
            var addresses = await _context.UserDetails
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<ShippingAddressesDTO>>(addresses);
        }

        public async Task<ShippingAddressesDTO> AddShippingAddress(int userId, ShippingAddressesDTO dto)
        {
            var address = _mapper.Map<UserDetails>(dto);
            address.UserId = userId;

            _context.UserDetails.Add(address);
            await _context.SaveChangesAsync();


            await _context.Entry(address).Reference(a => a.User).LoadAsync();

            return _mapper.Map<ShippingAddressesDTO>(address);
        }

        public async Task<bool> UpdateShippingAddress(int userId, int addressId, ShippingAddressesDTO dto)
        {
            var address = await _context.UserDetails
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == addressId && a.UserId == userId);

            if (address == null)
                return false;

            address.Gender = dto.Gender;
            address.Avatar = dto.Avatar;
            address.Street = dto.Street;
            address.City = dto.City;
            address.State = dto.State;
            address.ZipCode = dto.ZipCode;
            address.Country = dto.Country;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteShippingAddress(int userId, int addressId)
        {
            var address = await _context.UserDetails
                .FirstOrDefaultAsync(a => a.Id == addressId && a.UserId == userId);

            if (address == null)
                return false;

            _context.UserDetails.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<PurchaseDTO>> GetPurchase(int userId)
        {
            var purchases = await _context.Purchases
                .Where(p => p.UserId == userId)
                .Include(p => p.CartItems)
                .Include(p => p.UserDetails)
                .ToListAsync();

            return _mapper.Map<List<PurchaseDTO>>(purchases);
        }

        public async Task<PurchaseDTO> AddPurchase(int userId, CreatePurchaseDTO dto)
        {
            var addressExists = await _context.UserDetails
                .AnyAsync(a => a.Id == dto.UserDetailsId && a.UserId == userId);

            if (!addressExists)
                throw new Exception("Invalid Shipping Address.");

            var cartItems = await _context.CartItems
                .Where(ci => dto.CartItemIds.Contains(ci.Id) && ci.Cart.UserId == userId)
                .ToListAsync();

            if (cartItems.Count != dto.CartItemIds.Count)
                throw new Exception("Some cart items are invalid or not owned by user.");

            var totalPrice = cartItems.Sum(ci => ci.TotalPrice);

            var purchase = new Purchase
            {
                UserId = userId,
                UserDetailsId = dto.UserDetailsId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Total = totalPrice,
                CartItems = cartItems
            };

            foreach (var item in cartItems)
            {
                item.CartId = 0;
                item.UpdatedAt = DateTime.UtcNow;
            }

            _context.Purchases.Add(purchase);
            await _context.SaveChangesAsync();

            var saved = await _context.Purchases
                .Include(p => p.CartItems)
                .Include(p => p.UserDetails)
                .FirstOrDefaultAsync(p => p.Id == purchase.Id);

            return _mapper.Map<PurchaseDTO>(saved);
        }

        public async Task<bool> UpdatePurchase(int userId, int purchaseId, CreatePurchaseDTO dto)
        {
            var purchase = await _context.Purchases
                .Include(p => p.CartItems)
                .FirstOrDefaultAsync(p => p.Id == purchaseId && p.UserId == userId);

            if (purchase == null)
                return false;

            var addressExists = await _context.UserDetails
                .AnyAsync(a => a.Id == dto.UserDetailsId && a.UserId == userId);

            if (!addressExists)
                throw new Exception("Invalid Shipping Address.");

            var cartItems = await _context.CartItems
                .Where(ci => dto.CartItemIds.Contains(ci.Id) && ci.Cart.UserId == userId)
                .ToListAsync();

            if (cartItems.Count != dto.CartItemIds.Count)
                throw new Exception("Some cart items are invalid or not owned by user.");

            var totalPrice = cartItems.Sum(ci => ci.TotalPrice);

            purchase.UpdatedAt = DateTime.UtcNow;
            purchase.UserDetailsId = dto.UserDetailsId;
            purchase.Total = totalPrice;
            purchase.CartItems = cartItems;

            foreach (var item in cartItems)
            {
                item.CartId = 0;
                item.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePurchase(int userId, int purchaseId)
        {
            var purchase = await _context.Purchases
                .FirstOrDefaultAsync(p => p.Id == purchaseId && p.UserId == userId);

            if (purchase == null)
                return false;

            _context.Purchases.Remove(purchase);
            await _context.SaveChangesAsync();
            return true;
        }

    }

}

