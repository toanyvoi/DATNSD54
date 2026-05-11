using DATNSD54.DAO.DTO;
using DATNSD54.DAO.Models;

namespace DATNSD54.View.IService
{
    public interface IPayService
    {
        // Lấy giỏ hàng của khách
        Task<CartDTO> GetCart(int? id);

        Task<BillDTO> GetBillUnVerified(int? BillId);

        Task<BillDTO> CreateUnverifiedBillAsync();
        Task<bool> ConfirmBillAsync(int id,int pttt);
    }
}