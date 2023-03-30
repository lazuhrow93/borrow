using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Borrow.Models;
using Borrow.Data;

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
         options.UseSqlServer(
         Configuration.GetConnectionString("BorrowContext")));
    }
}