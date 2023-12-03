using PTLab2_api.Data.Repositories.Implimentations;
using PTLab2_api.Data.Repositories.Interfaces;

namespace PTLab2_api.Data.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepository Products { get; private set; }

        public IPurchaseRepository Purchases { get; private set; }

        public ISaleRepository Sales { get; private set; }

        public IUserRepository Users { get; private set; }

        private readonly ShopDbContext _context;

        public UnitOfWork(ShopDbContext context)
        {
            _context = context;

            Products = new ProductRepository(_context);
            Purchases = new PurchaseRepository(_context);
            Sales = new SaleRepository(_context);
            Users = new UserRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
