using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Borrow.Data;
using System;
using Microsoft.AspNetCore.Identity;
using Borrow.Models.Identity;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<BorrowContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("BorrowContext") ?? throw new InvalidOperationException("Connection string 'BorrowContext' not found.")));

        //idnetity
        builder.Services.AddDbContext<BorrowContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("BorrowContext")));

        builder.Services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
        }).AddEntityFrameworkStores<BorrowContext>()
          .AddDefaultTokenProviders();


        // Add services to the container.
        builder.Services.AddControllersWithViews();

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
        app.UseAuthorization();
        app.UseAuthentication();
        //app.UseSession();


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
