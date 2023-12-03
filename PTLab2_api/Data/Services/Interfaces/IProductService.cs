using PTLab2_api.Data.Models;

namespace PTLab2_api.Data.Services.Interfaces
{
    public interface IProductService
    {
        public ServiceResponse<List<Product>> GetAll();
        public ServiceResponse<Product> GetById(int id);
    }
}
