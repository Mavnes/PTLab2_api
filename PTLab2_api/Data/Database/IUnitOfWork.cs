using PTLab2_api.Data.Repositories.Interfaces;

namespace PTLab2_api.Data.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IPurchaseRepository Purchases { get; }
        ISaleRepository Sales { get; }
        IUserRepository Users { get; }

        int Complete();
    }
}
