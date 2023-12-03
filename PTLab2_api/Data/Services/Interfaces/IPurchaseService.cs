using PTLab2_api.Data.DTO;

namespace PTLab2_api.Data.Services.Interfaces
{
    public interface IPurchaseService
    {
        public ServiceResponse<PurchaseDto> MakePurchase(int userId, int productId, string address);
        public ServiceResponse<List<PurchaseDto>> GetByUser(int userId);
    }
}
