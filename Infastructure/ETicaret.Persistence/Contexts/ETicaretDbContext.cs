using ETicaret.Domain.Entities;
using ETicaret.Domain.Entities.Common;
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
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Basket> Baskets { get; set; }

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
            
            modelBuilder.Entity<Order>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Basket>()
                .HasOne(x=>x.Order)
                .WithOne(x=> x.Basket)
                .HasForeignKey<Order>(x=>x.BasketId);
            
            modelBuilder.Entity<Adress>()
                .HasOne(a => a.User)
                .WithMany(u => u.Adresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull); 
            
            base.OnModelCreating(modelBuilder); 

          
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in datas)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in datas)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChanges();
        }
    }
}
