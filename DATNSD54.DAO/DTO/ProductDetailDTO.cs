using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.DTO
{
    public class ProductDetailDTO
    {
        public int Id { get; set; }

        
        public int Product_ID { get; set; }

      
        public int Size { get; set; }
        
        public string? Image { get; set; }

        public string Color { get; set; }

      
        public decimal Don_Gia { get; set; }

        public decimal salePrice { get; set; }
        public int SL { get; set; }

        public int Sale { get; set; }
        //public List<int> Sizes { get; set; }
        //public List<string> Colors { get; set; }

        public bool Trang_Thai { get; set; } = true;

        public DateTime Ngay_Tao { get; set; } = DateTime.Now;
    }
}
