using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATNSD54.DAO.Models
{
    public class Image
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Product")]
        public int Product_ID { get; set; }
        [Required(ErrorMessage = "Ảnh không được để trống")]
        public string IMG { get; set; }
        public DateTime Ngay_Tao { get; set; } = DateTime.Now;
        public bool Trang_Thai { get; set; }
        public virtual Product? Product { get; set; }
    }
}
