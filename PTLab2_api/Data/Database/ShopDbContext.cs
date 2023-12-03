using Microsoft.EntityFrameworkCore;
using PTLab2_api.Data.Models;

namespace PTLab2_api.Data.Database
{
    public class ShopDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Purchase> Purchases { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
