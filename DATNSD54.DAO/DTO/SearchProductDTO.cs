using DATNSD54.DAO.Models;

namespace DATNSD54.DAO.DTO
{
    public class SearchProductDTO
    {
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        public List<Brand> brands { get; set; } = new List<Brand>();
        public List<Size> sizes { get; set; }
        public List<ProductType> productTypes { get; set; }
    }
}
