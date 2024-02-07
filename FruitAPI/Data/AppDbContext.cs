using Microsoft.EntityFrameworkCore;
using FruitAPI.Models;

namespace FruitAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserInfo> UserInfo { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Fruit> Fruits { get; set; }
    }
}
