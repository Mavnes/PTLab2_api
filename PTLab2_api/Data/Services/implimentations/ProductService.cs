using PTLab2_api.Data.Database;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Services.Interfaces;

namespace PTLab2_api.Data.Services.implimentations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public ServiceResponse<Product> GetById(int id)
        {
            ServiceResponse<Product> _response = new();

            try
            {
                _response.Data = _unitOfWork.Products.Get(id);
                _response.Message = "Product was successfully received.";
                _response.Success = true;

                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = "Products was not received.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Success = false;
                _response.Data = null;

                return _response;
            }
        }

        public ServiceResponse<List<Product>> GetAll()
        {
            ServiceResponse<List<Product>> _response = new();

            try
            {
                _response.Data = _unitOfWork.Products.GetAll().ToList();
                _response.Message = "The list of products was successfully received.";
                _response.Success = true;

                return _response;
            }
            catch (Exception ex) 
            {
                _response.Message = "The list of products was not received.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Success = false;
                _response.Data = null;

                return _response;
            }
        }
    }
}
