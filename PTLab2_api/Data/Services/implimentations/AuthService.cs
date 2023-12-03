using PTLab2_api.Data.Database;
using PTLab2_api.Data.DTO;
using PTLab2_api.Data.Models;
using PTLab2_api.Data.Repositories.Interfaces;
using PTLab2_api.Data.Services.Interfaces;

namespace PTLab2_api.Data.Services.implimentations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public ServiceResponse<UserDto> Register(string name, string email, string password)
        {
            ServiceResponse<UserDto> _response = new();

            if (_unitOfWork.Users.GetUserByEmail(email) is not null) 
            {
                _response.Message = "Registration was not successful.";
                _response.ErrorMessages = new List<string> { "User with this email already exists." };
                _response.Success = false;
                _response.Data = null;
                return _response;
            }

            try
            {
                var sale = _unitOfWork.Sales.GetAll()
                                            .OrderBy(s => s.MinTotalExpenses)
                                            .First();

                User user = new User(0, name, email, password, 0, sale.Id);

                if (sale is null)
                {
                    _response.Message = "User was not received.";
                    _response.ErrorMessages = new List<string> { "Wrong sale id." };
                    _response.Success = false;
                    _response.Data = null;

                    return _response;
                }

                _unitOfWork.Users.Add(user);
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
                _response.Message = "New user created.";
                _response.Success = true;
                return _response;
            }
            catch (Exception ex) 
            {
                _response.Message = "Registration was not successful.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Success = false;
                _response.Data = null;
                return _response;
            }
        }

        public ServiceResponse<UserDto> Login(string email, string password)
        {
            ServiceResponse<UserDto> _response = new();

            try
            {
                var user = _unitOfWork.Users.GetUserByEmail(email);

                if (user is null)
                {
                    _response.Message = "Login was not successful.";
                    _response.ErrorMessages = new List<string> { "User with this email does not exist." };
                    _response.Success = false;
                    _response.Data = null;
                    return _response;
                }

                if (user.Password != password)
                {
                    _response.Message = "Login was not successful.";
                    _response.ErrorMessages = new List<string> { "Passwords does not match." };
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
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    TotalExpenses = user.TotalExpenses,
                    Sale = sale.Value
                };

                _response.Message = "Successful login.";
                _response.Success = true;
                _response.Data = userDto;
                return _response;
            }
            catch (Exception ex)
            {
                _response.Message = "Login was not successful.";
                _response.ErrorMessages = new List<string> { Convert.ToString(ex.Message) };
                _response.Success = false;
                _response.Data = null;
                return _response;
            }
        }
    }
}
