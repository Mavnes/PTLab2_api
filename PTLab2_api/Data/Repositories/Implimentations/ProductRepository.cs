using Microsoft.EntityFrameworkCore;
using PTLab2_api.Data.Database;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Repositories.Interfaces;

namespace PTLab2_api.Data.Repositories.Implimentations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ShopDbContext _shopDbContext;
        public ProductRepository(ShopDbContext context) : base(context) { }
    }
}
