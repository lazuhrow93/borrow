using Microsoft.EntityFrameworkCore;
using Borrow.Data;
using Microsoft.AspNetCore.Identity;
using Borrow.Models;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<BorrowContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BorrowContext")));

        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;

        }).AddEntityFrameworkStores<BorrowContext>()
          .AddDefaultTokenProviders();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();
    }
}