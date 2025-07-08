using Microsoft.AspNetCore.Identity;
using ORAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ORAA.Models;


namespace ORAA.Data
{
    public class DataContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Affirmation> Affirmations { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<HandCraftMan> HandCraftMen { get; set; }
        public DbSet<Jewelery> Jewelries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Ritual> Rituals { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<GIftCard> GIftCards { get; set; }
        public DbSet<DiscountCode> DiscountCodes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Crystal> Crystals { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
