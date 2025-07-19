using Microsoft.AspNetCore.Identity;
using ORAA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
        public DbSet<Collections> Collections { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.HasIndex(e => e.AppleId)
                    .IsUnique()
                    .HasFilter("[AppleId] IS NOT NULL");

                entity.HasIndex(e => e.GoogleId)
                    .IsUnique()
                    .HasFilter("[GoogleId] IS NOT NULL");
            });

            // FIXED: Admin relationship - no inheritance, just reference
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasOne(a => a.User)
                    .WithMany() // User doesn't need navigation back to Admin
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // Use Restrict to avoid cycles
            });

            // Jewelry relationships
            modelBuilder.Entity<Jewelery>()
                .HasOne(j => j.HandCraftMan)
                .WithMany(h => h.jeweleries)
                .HasForeignKey(j => j.HandCraftManId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Jewelery>()
                .HasOne(j => j.Material)
                .WithMany()
                .HasForeignKey(j => j.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Jewelery>()
                .HasOne(j => j.Affirmation)
                .WithMany()
                .HasForeignKey(j => j.AffirmationId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Jewelery>()
                .HasOne(j => j.Ritual)
                .WithMany()
                .HasForeignKey(j => j.RitualId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Jewelery>()
                .HasOne(j => j.Review)
                .WithMany()
                .HasForeignKey(j => j.ReviewId)
                .OnDelete(DeleteBehavior.SetNull);

            // Cart relationships - Use Restrict to avoid cycles
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade to Restrict

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade); // This is fine as it's not cyclic

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Jewelry)
                .WithMany()
                .HasForeignKey(ci => ci.JewelryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Crystal)
                .WithMany()
                .HasForeignKey(ci => ci.CrystalId)
                .OnDelete(DeleteBehavior.Restrict);

            // Favorite relationships - Use Restrict to avoid cycles
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade to Restrict

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Jewelry)
                .WithMany()
                .HasForeignKey(f => f.JewelryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Crystal)
                .WithMany()
                .HasForeignKey(f => f.CrystalId)
                .OnDelete(DeleteBehavior.Restrict);

            // Chat and Consultant relationships
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasOne(c => c.User)
                    .WithMany(u => u.Chats)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade to Restrict

                entity.HasOne(c => c.Consultant)
                    .WithMany(consultant => consultant.Chats)
                    .HasForeignKey(c => c.ConsultantId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);
            });

            // Consultant relationships
            modelBuilder.Entity<Consultant>(entity =>
            {
                entity.HasOne(c => c.User)
                    .WithMany()
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade to Restrict
            });

            // Notification relationships
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(n => n.User)
                    .WithMany(u => u.Inbox)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade to Restrict

                entity.HasOne(n => n.Consultant)
                    .WithMany(c => c.Notifications)
                    .HasForeignKey(n => n.ConsultantId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);
            });

            // Message relationships
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Gift relationships
            modelBuilder.Entity<Gift>(entity =>
            {
                entity.HasOne(g => g.Sender)
                    .WithMany(u => u.Gifts)
                    .HasForeignKey(g => g.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(g => g.Recipient)
                    .WithMany()
                    .HasForeignKey(g => g.RecipientId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);

                entity.HasOne(g => g.Jewelry)
                    .WithMany()
                    .HasForeignKey(g => g.JewelryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(g => g.Crystal)
                    .WithMany()
                    .HasForeignKey(g => g.CrystalId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);
            });

            // Purchase relationships
            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Blog relationships
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserDetails relationships
            modelBuilder.Entity<UserDetails>()
                .HasOne(ud => ud.User)
                .WithOne(u => u.UserDetails)
                .HasForeignKey<UserDetails>(ud => ud.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Changed from Cascade to Restrict

            // Affirmation relationships
            modelBuilder.Entity<Affirmation>()
                .HasOne(a => a.Consultant)
                .WithMany()
                .HasForeignKey(a => a.ConsultantId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProductDetails relationships
            modelBuilder.Entity<ProductDetails>()
                .HasOne(pd => pd.Jewelery)
                .WithMany()
                .HasForeignKey(pd => pd.JewelryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ProductDetails>()
                .HasOne(pd => pd.Crystal)
                .WithMany()
                .HasForeignKey(pd => pd.CrystalId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure decimal precision
            modelBuilder.Entity<Jewelery>()
                .Property(j => j.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Jewelery>()
                .Property(j => j.SalePrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cart>()
                .Property(c => c.Subtotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cart>()
                .Property(c => c.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.TotalPrice)
                .HasPrecision(18, 2);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
            }
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is User && e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity is User user)
                {
                    user.UpdateTimestamp();
                }
            }
        }
    }
}