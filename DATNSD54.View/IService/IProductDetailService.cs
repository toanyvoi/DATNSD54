using DATNSD54.DAO.DTO;
using DATNSD54.DAO.Models;

namespace DATNSD54.View.IService
{
    public interface IProductDetailService
    {
        Task<ProductDTO> Show(int? Id);
        Task<(bool IsSuccess, string Message)> AddToCart(int IdPd ,int Sl);

    }
}
