using DATNSD54.DAO.DTO;
using DATNSD54.DAO.Models;

namespace DATNSD54.View.IService
{
    public interface IHomeService
    {
        Task<List<ProductDisplayDTO>> GetAllProducts();
        Task<SearchProductDTO> SearchProducts(string? textSearch);
    }
}
