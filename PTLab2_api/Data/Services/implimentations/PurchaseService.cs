using PTLab2_api.Data.Database;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using PTLab2_api.Data.DTO;

namespace PTLab2_api.Data.Services.implimentations
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public PurchaseService(IUnitOfWork unitOfWork, IUserService userService, IProductService productService) 
        { 
            _unitOfWork = unitOfWork; 
            _userService = userService;
            _productService = productService;
        }

        public ServiceResponse<List<PurchaseDto>> GetByUser(int userId)
        {
            ServiceResponse<List<PurchaseDto>> _response = new();

            var _userResponse = _userService.GetById(userId);

            if (!_userResponse.Success)
            {
                _response.Success = _userResponse.Success;
                _response.ErrorMessages = _userResponse.ErrorMessages;
                _response.Error = _userResponse.Error;
                _response.Message = "Purchases of user were not received.";
                _response.Data = null;
                return _response;
            }

            try
            {
                _response.Data = _unitOfWork.Purchases.GetAll()
                                                      .Where(p => p.UserId == userId)
                                                      .Join(_unitOfWork.Products.GetAll(),
                                                            purchase => purchase.ProductId,
                                                            product => product.Id,
                                                            (purchase, product) => new PurchaseDto(
                                                                purchase.Id, purchase.Date, purchase.Address,
                                                                product.Name, purchase.UsedPrice) )
                                                      .ToList();
                _response.Message = "Purchases of user were successfully received.";
                _response.Success = true;

                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = "Purchases of user were not received.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Success = false;
                _response.Data = null;

                return _response;
            }
        }

        public ServiceResponse<PurchaseDto> MakePurchase(int userId, int productId, string address)
        {
            ServiceResponse<PurchaseDto> _response = new();

            var _userResponse = _userService.UpdateSale(userId);

            if (!_userResponse.Success)
            {
                _response.Success = _userResponse.Success;
                _response.ErrorMessages = _userResponse.ErrorMessages;
                _response.Error = _userResponse.Error;
                _response.Message = "Purchase was not made.";
                _response.Data = null;
                return _response;
            }

            var user = _userResponse.Data;

            var _productResponse = _productService.GetById(productId);

            if (!_productResponse.Success)
            {
                _response.Success = _productResponse.Success;
                _response.ErrorMessages = _productResponse.ErrorMessages;
                _response.Error = _productResponse.Error;
                _response.Message = "Purchase was not made.";
                _response.Data = null;
                return _response;
            }

            var product = _productResponse.Data;

            try
            {
                var saleValue = user.Sale;

                float usedPrice = (product.Price / 100f) * (100f - (float)saleValue);

                Purchase purchase = new Purchase(0, DateTime.Now, address, product.Id, user.Id, usedPrice);

                _unitOfWork.Purchases.Add(purchase);
                _unitOfWork.Complete();

                _userResponse = _userService.UpdateSale(userId);

                if (!_userResponse.Success)
                {
                    _response.Success = _userResponse.Success;
                    _response.ErrorMessages = _userResponse.ErrorMessages;
                    _response.Error = _userResponse.Error;
                    _response.Message = "Purchase was not made.";
                    _response.Data = null;
                    return _response;
                }

                PurchaseDto purchaseDto = new PurchaseDto(purchase.Id, purchase.Date, purchase.Address, product.Name, purchase.UsedPrice);

                _response.Data = purchaseDto;
                _response.Message = "Purchase was successfully made.";
                _response.Success = true;

                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = "Purchase was not made.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Success = false;
                _response.Data = null;

                return _response;
            }
        }
    }
}
