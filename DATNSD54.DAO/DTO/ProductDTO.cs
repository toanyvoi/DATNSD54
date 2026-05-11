using DATNSD54.DAO.Models;
using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.DTO
{
    public class ProductDTO
    {

        public int Id { get; set; }

        public string? Ma { get; set; }

        public string Ten { get; set; }
        public int? Nam_SX { get; set; }
        public string? Mo_Ta { get; set; } 
        public decimal? GiaMin { get; set; } = 0;
        public int sale { get; set; } = 0;
        public decimal? salePriceMin { get; set; } = 0;
        public string type { get; set; }
        public string brand { get; set; }
        

        public List<string> ListIMG { get; set; }

        public List<ProductDetailDTO> ListProductDetail { get; set; }
    }
}
