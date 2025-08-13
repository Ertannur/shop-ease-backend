using ETicaret.Domain.Entities;
using ETicaret.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Contexts
{
    public class ETicaretDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public ETicaretDbContext(DbContextOptions<ETicaretDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Adress> Adresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Favorite>()
                .HasKey(x => new { x.ProductId, x.AppUserId });
            modelBuilder.Entity<Favorite>()
                .HasOne(x => x.AppUser)
                .WithMany(x => x.Favorites)
                .HasForeignKey(x => x.AppUserId);
            modelBuilder.Entity<Favorite>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Favorites)
                .HasForeignKey(x => x.ProductId);
            base.OnModelCreating(modelBuilder); 

          
        }
    }
}
