using ETicaret.Application.Abstractions;
using ETicaret.Application.Abstractions.Storage.Azure;
using ETicaret.Application.DTOs.Images.Requests;
using ETicaret.Application.DTOs.Products.Results;
using ETicaret.Application.DTOs.Products.Requests;
using ETicaret.Domain.Entities;
using ETicaret.Domain.Enums;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Paggination;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services;

public class ProductService(ETicaretDbContext context, IHttpContextAccessor httpContextAccessor) : IProductService
{
    private async Task<AppUser?> CurrentUser()
    {
        var userName = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        AppUser? appUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        return appUser;
    }
    public async Task<GetProductResultDto> GetProductsAsync(string? category, int currentPage = 1, int pageSize = 8)
    {
        var products = context.Products.Include(x => x.Category)
            .Include(x => x.ProductTypes)
            .Include(x => x.Images)
            .AsSplitQuery()
            .Where(x => x.IsDeleted == false);

        if (!string.IsNullOrWhiteSpace(category))
        {
            products = products.Where(p => p.Category.Name == category);
        }

        var paginationResponse = await products.Where(new PaginationRequest(currentPage, pageSize), x => x.CreatedDate);
        var productViewModels = paginationResponse.Data.Select(x => new ProductViewModel()
        {
            Name = x.Name,
            ProductId = x.Id,
            Price = x.Price,
            ImageUrl = x.Images.Select(x => x.ImageUrl).FirstOrDefault(),
        });

        return new()
        {
            Products = productViewModels,
            HasNextPage = paginationResponse.HasNextPage,
            HasPreviousPage = paginationResponse.HasPreviousPage,
            TotalPage = paginationResponse.TotalPages,
            TotalCount = paginationResponse.TotalCount
        };
    }
    public async Task<GetProductResultDto> GetProductsByNameAsync(string? name, int currentPage = 1, int pageSize = 8)
       {
           var products = context.Products.Include(x=>x.ProductTypes)
               .Include(x=>x.Images)
               .AsSplitQuery()
               .Where(x => x.IsDeleted == false);
           if (!string.IsNullOrWhiteSpace(name))
           {
               products = products
                   .Where(x => EF.Functions.Like(
                       EF.Functions.Collate(x.Name, "SQL_Latin1_General_CP1_CI_AS"),
                       $"%{name}%"));

           }
           var paginationResponse = await products.Where(new PaginationRequest(currentPage, pageSize), x => x.CreatedDate);
           var productViewModels = paginationResponse.Data.Select(x => new ProductViewModel()
           {
               Name = x.Name,
               ProductId = x.Id,
               Price = x.Price,
               ImageUrl = x.Images.Select(x => x.ImageUrl).FirstOrDefault(),
           });
           return new()
           {
               Products = productViewModels,
               HasNextPage = paginationResponse.HasNextPage,
               HasPreviousPage = paginationResponse.HasPreviousPage,
               TotalPage = paginationResponse.TotalPages,
               TotalCount = paginationResponse.TotalCount
           };
       }

    public async Task<GetProductResultDto> GetFilteredProducts(string? type, int currentPage = 1, int pageSize = 8)
    {
        var products = context.Products.Include(x=>x.ProductTypes)
            .Include(x=>x.Images)
            .AsSplitQuery()
            .Where(x => x.IsDeleted == false);
        PaginationResponse<Product> paginationResponse;
        IEnumerable<ProductViewModel> productViewModels;
        switch (type)
        {
            case "new":
                paginationResponse = await products.Where(new PaginationRequest(currentPage, pageSize), x => x.CreatedDate);
                productViewModels = paginationResponse.Data.Select(x => new ProductViewModel()
                {
                    Name = x.Name,
                    ProductId = x.Id,
                    Price = x.Price,
                    ImageUrl = x.Images.Select(x => x.ImageUrl).FirstOrDefault(),
                });
                return new()
                {
                    Products = productViewModels,
                    HasNextPage = paginationResponse.HasNextPage,
                    HasPreviousPage = paginationResponse.HasPreviousPage,
                    TotalPage = paginationResponse.TotalPages,
                    TotalCount = paginationResponse.TotalCount
                };
            case "discounted":
                paginationResponse  = await products.Where(new PaginationRequest(currentPage, pageSize), x => x.Price);
                productViewModels = paginationResponse.Data.Select(x => new ProductViewModel()
                {
                    Name = x.Name,
                    ProductId = x.Id,
                    Price = x.Price,
                    ImageUrl = x.Images.Select(x => x.ImageUrl).FirstOrDefault(),
                });
                return new()
                {
                    Products = productViewModels,
                    HasNextPage = paginationResponse.HasNextPage,
                    HasPreviousPage = paginationResponse.HasPreviousPage,
                    TotalPage = paginationResponse.TotalPages,
                    TotalCount = paginationResponse.TotalCount
                };
            case "deals":
                paginationResponse = await products.Where(new PaginationRequest(currentPage, pageSize), x => x.CreatedDate);
                productViewModels = paginationResponse.Data.Select(x => new ProductViewModel()
                {
                    Name = x.Name,
                    ProductId = x.Id,
                    Price = x.Price,
                    ImageUrl = x.Images.Select(x => x.ImageUrl).FirstOrDefault(),
                });
                return new()
                {
                    Products = productViewModels,
                    HasNextPage = paginationResponse.HasNextPage,
                    HasPreviousPage = paginationResponse.HasPreviousPage,
                    TotalPage = paginationResponse.TotalPages,
                    TotalCount = paginationResponse.TotalCount
                };
            case  "weekly" : 
                var now = DateTime.UtcNow;
                var oneWeekAgo = now.AddDays(-7);
                products = products.Where(p=> p.CreatedDate <= oneWeekAgo);
                paginationResponse = await products.Where(new PaginationRequest(currentPage, pageSize), x => x.CreatedDate);
                productViewModels = paginationResponse.Data.Select(x => new ProductViewModel()
                {
                    Name = x.Name,
                    ProductId = x.Id,
                    Price = x.Price,
                    ImageUrl = x.Images.Select(x => x.ImageUrl).FirstOrDefault(),
                });
                return new()
                {
                    Products = productViewModels,
                    HasNextPage = paginationResponse.HasNextPage,
                    HasPreviousPage = paginationResponse.HasPreviousPage,
                    TotalPage = paginationResponse.TotalPages,
                    TotalCount = paginationResponse.TotalCount
                };
            case "best-sellers": 
                paginationResponse = await products.Where(new PaginationRequest(currentPage, pageSize), x => x.CreatedDate);
                productViewModels = paginationResponse.Data.Select(x => new ProductViewModel()
                {
                    Name = x.Name,
                    ProductId = x.Id,
                    Price = x.Price,
                    ImageUrl = x.Images.Select(x => x.ImageUrl).FirstOrDefault(),
                });
                return new()
                {
                    Products = productViewModels,
                    HasNextPage = paginationResponse.HasNextPage,
                    HasPreviousPage = paginationResponse.HasPreviousPage,
                    TotalPage = paginationResponse.TotalPages,
                    TotalCount = paginationResponse.TotalCount
                };
            default:
                paginationResponse = await products.Where(new PaginationRequest(currentPage, pageSize), x => x.CreatedDate);
                productViewModels = paginationResponse.Data.Select(x => new ProductViewModel()
                {
                    Name = x.Name,
                    ProductId = x.Id,
                    Price = x.Price,
                    ImageUrl = x.Images.Select(x => x.ImageUrl).FirstOrDefault(),
                });
                return new()
                {
                    Products = productViewModels,
                    HasNextPage = paginationResponse.HasNextPage,
                    HasPreviousPage = paginationResponse.HasPreviousPage,
                    TotalPage = paginationResponse.TotalPages,
                    TotalCount = paginationResponse.TotalCount
                };
        }

       
    }

    public async Task<GetProductByIdResultDto> GetProductByIdAsync(Guid id)
    {
        var colorTypeEnum = typeof(ColorType);
        var productDto = await context.Products
            .AsSplitQuery()
            .Where(p => p.Id == id)
            .Select(p => new GetProductByIdResultDto
            {
                ProductId = p.Id.ToString(),
                Title = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.ProductTypes
                    .Where(pt => pt.Stock != null)
                    .Sum(pt => (int?)pt.Stock.Quantity) ?? 0,
                Images = p.Images
                    .Select(i => i.ImageUrl)
                    .ToList(),
                Details = p.ProductTypes
                    .Select(pt => new ProductDetail
                    {
                        ProductDetailId = pt.Id,
                        Color = pt.Color != null
                            ? pt.Color.ColorType.ToString()
                            : null,
                        Size = pt.Size,
                        Stock = pt.Stock != null
                            ? pt.Stock.Quantity
                            : 0
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        if (productDto == null)
        {
            return new();
        }

        return productDto;
    }

    //eklendi
    public async Task<AddProductResultDto> AddProductAsync(AddProductRequest dto)
    {
        Guid productId = Guid.NewGuid();
        var product = new Product
        {
            Id = productId,
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            IsDeleted = false
        };
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return new AddProductResultDto() { Message = "Ürün eklendi", Success = true, ProductId = productId };
    }
    
    public async Task<AddFavoriteProductResultDto> AddFavoritesAsync(Guid productId)
    {
        var currentUser = await CurrentUser();
        if (currentUser == null)
        {
            return new()
            {
                Success = false,
                Message = "Kullanıcı Bilgisi Bulunamadı",
                ProductId = null
            };
        }
        var currentProduct = await context.Products.FindAsync(productId);
        if (currentProduct == null)
            return new()
            {
                Success = false,
                Message = "Ürün Bilgisi Bulunamadı",
                ProductId = null
            };
        // Favoride var mı kontrol et
        bool exists = await context.Favorites
            .AnyAsync(f => f.AppUserId == currentUser.Id && f.ProductId == productId);

        if (exists)
            return new ()
            {   
                Success = false,
                Message = "Ürün Hali Hazırda Favorilerde Bulunmaktadır",
                ProductId = productId
            };

        var favorite = new Favorite
        {
            AppUserId = currentUser.Id,
            ProductId = productId,
        };

        await context.Favorites.AddAsync(favorite);
        await context.SaveChangesAsync();
        return new()
        {
            Success = true,
            Message = "Ürün Başarıyla Favorilere Eklenmiştir",
            ProductId = productId
        };
    }

    public async Task<bool> RemoveFavoritesAsync(Guid productId)
    {
        AppUser? currentUser = await CurrentUser();
        if (currentUser == null)
            return false;
        var favorite =  await context.Favorites.FirstOrDefaultAsync(f => f.AppUserId == currentUser.Id && f.ProductId == productId);
        if (favorite == null)
            return false;
        context.Favorites.Remove(favorite);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<GetFavoriteProductDto>> GetFavoritesAsync()
    {
        AppUser? currentUser = await CurrentUser();
        if (currentUser == null)
            return new List<GetFavoriteProductDto>();
        var getFavoriteProductDto = await context.Favorites
            .AsSplitQuery()
            .Where(f => f.AppUserId == currentUser.Id)
            .Select(x=> new GetFavoriteProductDto()
            {
                ProductId = x.ProductId,
                Price = x.Product.Price,
                Title = x.Product.Name,
                ImageUrl = x.Product.Images.Select(x => x.ImageUrl).FirstOrDefault(),
            }).ToListAsync();
            
        return getFavoriteProductDto;
    }

   


    public async Task<IEnumerable<GetProductResultDto>> GetDiscountProductsAsync(int currentPage = 1, int pageSize = 8)
    {
        var products = context.Products
            .Include(x => x.ProductTypes)
            .Include(x => x.Images)
            .AsSplitQuery()
            .Where(x => x.IsDeleted == false);
        return new List<GetProductResultDto>();
    }
}
