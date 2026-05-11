using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã size không được để trống")]
        [StringLength(20, ErrorMessage = "Mã size tối đa 20 ký tự")]
        public int Ma { get; set; }

        [Required(ErrorMessage = "Tên size không được để trống")]
        [StringLength(50, ErrorMessage = "Tên size tối đa 50 ký tự")]
        public string Ten { get; set; }

        public DateTime Ngay_Tao { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<ProductDetail>? ProductDetails { get; set; }
    }
}
