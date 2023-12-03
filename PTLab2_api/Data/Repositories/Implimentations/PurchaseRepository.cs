using Microsoft.EntityFrameworkCore;
using PTLab2_api.Data.Database;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Repositories.Interfaces;

namespace PTLab2_api.Data.Repositories.Implimentations
{
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(ShopDbContext context) : base(context) { }
    }
}
