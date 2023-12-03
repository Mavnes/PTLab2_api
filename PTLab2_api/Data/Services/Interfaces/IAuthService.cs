using PTLab2_api.Data.DTO;

namespace PTLab2_api.Data.Services.Interfaces
{
    public interface IAuthService
    {
        public ServiceResponse<UserDto> Register(string name, string email, string password);
        public ServiceResponse<UserDto> Login(string email, string password);
    }
}
