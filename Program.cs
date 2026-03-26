using Microsoft.AspNetCore.Identity;

namespace HouseRentingSystem
{
    public class Program
    {
        public class Program
        {
            public static void Main(string[] args)
            {
                var builder = WebApplication.CreateBuilder(args);

                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                builder.Services.AddDbContext<HouseRentingDbContext>(opt => opt.UseSqlServer(connectionString));
                builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<HouseRentingDbContext>()
                    .AddDefaultTokenProviders();

                builder.Services.ConfigureApplicationCookie(options =>
                {
                    options.LogoutPath = "/User/Login";
                    options.AccessDeniedPath = "/User/AccessDenied";
                });

                builder.Services.AddControllersWithViews();

                var app = builder.Build();

                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                 
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapStaticAssets();
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}")
                    .WithStaticAssets();

                app.Run();
            }
        }
    }