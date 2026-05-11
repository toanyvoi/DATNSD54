using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.Models
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên loại sản phẩm không được để trống")]
        [StringLength(100, ErrorMessage = "Tên tối đa 100 ký tự")]
        public string Ten { get; set; }

        [Required(ErrorMessage = "Mã loại sản phẩm không được để trống")]
        [StringLength(20, ErrorMessage = "Mã tối đa 20 ký tự")]
        public string Ma { get; set; }

        public DateTime Ngay_Tao { get; set; } = DateTime.Now;

        public bool Trang_Thai { get; set; } = true;

        // Navigation
        public ICollection<Product>? Products { get; set; }
    }
}
