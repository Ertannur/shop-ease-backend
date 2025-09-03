using System.Web.UI;
using ETicaret.Application.Abstractions;
using ETicaret.Application.DTOs.Orders.Requests;
using ETicaret.Application.DTOs.Orders.Results;
using ETicaret.Application.DTOs.Products.Results;
using ETicaret.Domain.Entities;
using ETicaret.Persistence.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Services;

public class OrderService(ETicaretDbContext context, IBasketService basketService, IHttpContextAccessor httpContextAccessor, IEmailService emailService) : IOrderService
{
    private async Task<AppUser?> CurrentUser()
    {
        var userName = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
        AppUser? appUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        return appUser;
    }
    public async Task CreateOrderAsync(CreateOrderDto dto)
    {
        StringWriter writer = new StringWriter();
        HtmlTextWriter html = new HtmlTextWriter(writer);
        var basket = await basketService.GetUserActiveBasketAsync();
        await context.Order.AddAsync(new()
        {
            AdressId = dto.AdressId,
            Id = basket.Id,
            IsDeleted = false,
        });
        await context.SaveChangesAsync();
        var user = await CurrentUser();
        var value = await context.Order.Where(o=> o.Id== basket.Id).Select(x => new ListCurrentUserOrdersResultDto()
        {
            OrderId = x.Id,
            OrderDate = x.CreatedDate,
            Address = x.Adress.Address + " " + x.Adress.City + " " + x.Adress.District + " " +x.Adress.PostCode,
            Products = x.Basket.BasketItems
                .Select(p =>  new ProductResultDto()
                {
                    ProductId = p.ProductId,
                    TotalPrice = p.Quantity*p.Product.Price,
                    ProductName = p.Product.Name,
                    Quantity = p.Quantity
                })
        })
            .FirstOrDefaultAsync();
        html.RenderBeginTag(HtmlTextWriterTag.H1);
        html.WriteEncodedText("Siparişiniz Başarıyla Oluşturulmuştur.");
        html.RenderEndTag();
        html.WriteEncodedText(String.Format("Değerli {0}", user.FirstName + " " + user.LastName));
        html.WriteBreak();
        
        html.RenderBeginTag(HtmlTextWriterTag.P);
        html.WriteEncodedText($"Sipariş Tarihi: {value.OrderDate}");
        html.WriteBreak();
        html.WriteEncodedText($"Teslimat Adresi: {value.Address}");
        html.RenderEndTag();

        html.RenderBeginTag(HtmlTextWriterTag.Table);
        html.RenderBeginTag(HtmlTextWriterTag.Tr);
        html.RenderBeginTag(HtmlTextWriterTag.Th); html.WriteEncodedText("Ürün"); html.RenderEndTag();
        html.RenderBeginTag(HtmlTextWriterTag.Th); html.WriteEncodedText("Adet"); html.RenderEndTag();
        html.RenderBeginTag(HtmlTextWriterTag.Th); html.WriteEncodedText("Toplam Fiyat"); html.RenderEndTag();
        html.RenderEndTag();
        foreach (var product in value.Products)
        {
            html.RenderBeginTag(HtmlTextWriterTag.Tr);

            html.RenderBeginTag(HtmlTextWriterTag.Td);
            html.WriteEncodedText(product.ProductName);
            html.RenderEndTag();

            html.RenderBeginTag(HtmlTextWriterTag.Td);
            html.WriteEncodedText(product.Quantity.ToString());
            html.RenderEndTag();

            html.RenderBeginTag(HtmlTextWriterTag.Td);
            html.WriteEncodedText(product.TotalPrice.ToString("C")); 
            html.RenderEndTag();

            html.RenderEndTag(); 
        }
        html.RenderEndTag(); // Table bitişi
        html.Flush();
        string htmlString = writer.ToString();
        await emailService.SendEmailAsync(user.Email,"Sipariş Bilgilendirme",htmlString );
        
    }

    public async Task<IEnumerable<ListCurrentUserOrdersResultDto>> ListCurrentUserOrdersAsync()
    {
        var currentUser = await CurrentUser();
        if (currentUser is null)
        {
            return  new List<ListCurrentUserOrdersResultDto>();
        }
        var value = context.Order.Where(X=> X.Basket.UserId == currentUser.Id && X.IsDeleted == false).Select(x => new ListCurrentUserOrdersResultDto()
        {
            OrderId = x.Id,
            OrderDate = x.CreatedDate,
            Address = x.Adress.Address + " " + x.Adress.City + " " + x.Adress.District + " " +x.Adress.PostCode,
            Products = x.Basket.BasketItems
                .Select(p =>  new ProductResultDto()
                {
                    ProductId = p.ProductId,
                    TotalPrice = p.Quantity*p.Product.Price,
                    ProductName = p.Product.Name,
                    Quantity = p.Quantity
                })
        });
        return value;
    }
}