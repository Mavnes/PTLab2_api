using PTLab2_api.Data.DTO;

namespace PTLab2_api.Data.Services.Interfaces
{
    public interface IUserService
    {
        public ServiceResponse<UserDto> UpdateSale(int id);
        public ServiceResponse<UserDto> GetById(int id);
    }
}
