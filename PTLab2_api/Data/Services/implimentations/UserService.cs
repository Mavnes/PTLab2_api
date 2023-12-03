using PTLab2_api.Data.Database;
using PTLab2_api.Data.DTO;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Services.Interfaces;

namespace PTLab2_api.Data.Services.implimentations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public ServiceResponse<UserDto> GetById(int id)
        {
            ServiceResponse<UserDto> _response = new();

            try
            {
                var user = _unitOfWork.Users.Get(id);

                if (user is null)
                {
                    _response.Message = "User was not received.";
                    _response.ErrorMessages = new List<string> { "Wrong user id." };
                    _response.Success = false;
                    _response.Data = null;

                    return _response;
                }

                var sale = _unitOfWork.Sales.Get((int)user.SaleId);

                if (sale is null)
                {
                    _response.Message = "User was not received.";
                    _response.ErrorMessages = new List<string> { "Wrong sale id." };
                    _response.Success = false;
                    _response.Data = null;

                    return _response;
                }

                UserDto userDto = new UserDto()
                {
                    Id = id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    TotalExpenses = user.TotalExpenses,
                    Sale = sale.Value
                };

                _response.Data = userDto;
                _response.Message = "User was successfully received.";
                _response.Success = true;

                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = "User was not received.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Success = false;
                _response.Data = null;

                return _response;
            }
        }

        public ServiceResponse<UserDto> UpdateSale(int id)
        {
            ServiceResponse<UserDto> _response = new();

            var user = _unitOfWork.Users.Get(id);

            if (user is null)
            {
                _response.Message = "Sale was not updated.";
                _response.ErrorMessages = new List<string> { "User is null." };
                _response.Success = false;
                _response.Data = null;
                return _response;
            }

            try
            {
                int userPurchasesCount = _unitOfWork.Purchases.GetAll().Where(p => p.UserId == user.Id).Count();

                var sale = _unitOfWork.Sales.GetAll()
                                            .OrderBy(s => s.MinTotalExpenses)
                                            .First();

                float totalSum = 0;

                if (userPurchasesCount > 0)
                {
                    totalSum = _unitOfWork.Purchases.GetAll()
                                                    .Where(p => p.UserId == user.Id)
                                                    .Sum(p => p.UsedPrice);

                    sale = _unitOfWork.Sales.GetAll()
                                            .OrderByDescending(s => s.MinTotalExpenses)
                                            .First(s => s.MinTotalExpenses < totalSum);
                }
                    

                user.TotalExpenses = totalSum;
                user.SaleId = sale.Id;

                _unitOfWork.Users.Update(user);
                _unitOfWork.Complete();

                UserDto userDto = new UserDto()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    TotalExpenses = user.TotalExpenses,
                    Sale = sale.Value
                };

                _response.Data = userDto;
                _response.Message = "Sale was successfully updated.";
                _response.Success = true;

                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = "Sale was not updated.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Success = false;
                _response.Data = null;

                return _response;
            }
        }
    }
}
