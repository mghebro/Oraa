using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<OrderHistory> orderHistories { get; set; }
        public DbSet<Collections> Collections { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserDetails)
                .WithOne(ud => ud.User)
                .HasForeignKey<UserDetails>(ud => ud.UserId);
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");
                entity.Property(e => e.AuthProvider)
                    .HasDefaultValue("Email")
                    .HasMaxLength(50);
                entity.HasIndex(e => e.AppleId)
                    .IsUnique()
                    .HasFilter("[AppleId] IS NOT NULL");
                entity.HasIndex(e => e.GoogleId)
                    .IsUnique()
                    .HasFilter("[GoogleId] IS NOT NULL");
            });

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.UserDetails)
                .WithMany()
                .HasForeignKey(p => p.UserDetailsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.GiftCard)
                .WithMany()
                .HasForeignKey(p => p.GiftCardId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.DiscountCode)
                .WithMany()
                .HasForeignKey(p => p.DiscountCodeId)
                .OnDelete(DeleteBehavior.Restrict);

           
            modelBuilder.Entity<Consultant>()
                .HasOne(c => c.Chat)
                .WithOne(ch => ch.Consultant)
                .HasForeignKey<Consultant>(c => c.ChatId);

            modelBuilder.Entity<Consultant>()
                .HasOne(c => c.Notification)
                .WithMany()
                .HasForeignKey("NotificationId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Consultant>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Jewelery>()
                .HasOne(j => j.HandCraftMan)
                .WithMany()
                .HasForeignKey(j => j.HandCraftManId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Jewelery>()
                .HasOne(j => j.Material)
                .WithMany()
                .HasForeignKey(j => j.MaterialId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<Jewelery>()
                .HasOne(j => j.Affirmation)
                .WithMany()
                .HasForeignKey("AffirmationId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Jewelery>()
                .HasOne(j => j.Ritual)
                .WithMany()
                .HasForeignKey("RitualId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Jewelery>()
                .HasOne(j => j.Review)
                .WithMany()
                .HasForeignKey("ReviewId")
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Gift>()
                .HasOne(g => g.Admin)
                .WithMany(a => a.Gifts)
                .HasForeignKey(g => g.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
