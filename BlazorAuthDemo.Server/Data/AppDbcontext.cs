using BlazorAuthDemo.Shared.Models;
using Microsoft.EntityFrameworkCore;
namespace BlazorAuthDemo.Server.Data
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
    }
}
