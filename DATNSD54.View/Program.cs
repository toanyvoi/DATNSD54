using DATNSD54.View.IService;
using DATNSD54.View.IService.Service;
using DATNSD54.DAO.Data;
using Microsoft.EntityFrameworkCore;

namespace DATNSD54.View
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
            builder.Services.AddTransient<AuthTokenHandler>();
            builder.Services.AddHttpClient("MyAPI", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7218/");
            }).AddHttpMessageHandler<AuthTokenHandler>(); 
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IProductDetailService, ProductDetailService>();
            builder.Services.AddScoped<IPayService, PayService>();
            builder.Services.AddScoped<IHomeService, HomeSevice>();

            // C?u hình Session ?? l?u token
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(7); // Session tồn tại 7 ngày
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddAuthentication("CookieAuth") // Đặt tên scheme là CookieAuth
    .AddCookie("CookieAuth", options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.LoginPath = "/Auth/Login"; // Đường dẫn đến trang Login
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<DbContextApp>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
           
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
