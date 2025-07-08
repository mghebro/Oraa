using AutoMapper;
using ORAA.DTO;
using ORAA.Models;
using ORAA.Request;

namespace ORAA.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Admin
            CreateMap<Admin, AddAdmin>().ReverseMap();
            CreateMap<Admin, AdminDTO>().ReverseMap();

            // Affirmation
            CreateMap<Affirmation, AddAffirmation>().ReverseMap();
            CreateMap<Affirmation, AffirmationDTO>().ReverseMap();

            // Blog
            CreateMap<Blog, AddBlog>().ReverseMap();
            CreateMap<Blog, BlogDTO>().ReverseMap();

            // Cart
            CreateMap<Cart, AddCart>().ReverseMap();
            CreateMap<Cart, CartDTO>().ReverseMap();

            // CartItem
            CreateMap<CartItem, AddCartItem>().ReverseMap();
            CreateMap<CartItem, CartItemDTO>().ReverseMap();

            // Chat
            CreateMap<Chat, AddChat>().ReverseMap();
            CreateMap<Chat, ChatDTO>().ReverseMap();

            // Consultant
            CreateMap<Consultant, AddConsultant>().ReverseMap();
            CreateMap<Consultant, ConsultantDTO>().ReverseMap();

            // Crystal
            CreateMap<Crystal, AddCrystal>().ReverseMap();
            CreateMap<Crystal, CrystalDTO>().ReverseMap();

            // DiscountCode
            CreateMap<DiscountCode, AddDiscountCode>().ReverseMap();
            CreateMap<DiscountCode, DiscountCodeDTO>().ReverseMap();

            // Favorite
            CreateMap<Favorite, AddFavorite>().ReverseMap();
            CreateMap<Favorite, FavoriteDTO>().ReverseMap();

            // Gift
            CreateMap<Gift, AddGift>().ReverseMap();
            CreateMap<Gift, GiftDto>().ReverseMap();

            // GIftCard
            CreateMap<GIftCard, AddGiftCard>().ReverseMap();

            // HandCraftMan
            CreateMap<HandCraftMan, AddHandCraftMan>().ReverseMap();
            CreateMap<HandCraftMan, HandCraftManDTO>().ReverseMap();

            // Jewelery
            CreateMap<Jewelery, AddJewelery>().ReverseMap();
            CreateMap<Jewelery, JeweleryDTO>().ReverseMap();

            // Jewels
         //   CreateMap<Jewels, AddJewels>().ReverseMap();
       //     CreateMap<Jewels, JewelsDTO>().ReverseMap();

            // Material
            CreateMap<Material, AddMaterial>().ReverseMap();
            CreateMap<Material, MaterialDTO>().ReverseMap();

            // Message
            CreateMap<Message, AddMessage>().ReverseMap();
            CreateMap<Message, MessageDTO>().ReverseMap();

            // Notification
            CreateMap<Notification, AddNotification>().ReverseMap();
            CreateMap<Notification, NotificationDTO>().ReverseMap();

            // Purchase
            CreateMap<Purchase, AddPurchase>().ReverseMap();
            CreateMap<Purchase, PurchaseDTO>().ReverseMap();

            // Review
            CreateMap<Review, AddReview>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();

            // Ritual
            CreateMap<Ritual, AddRitual>().ReverseMap();
            CreateMap<Ritual, RitualDTO>().ReverseMap();

            // User
            CreateMap<User, AddUser>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

        }
    }
}
