using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.Models
{
    public class Color
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "ma không được để trống")]
        public string Ma { get; set; }
        [Required(ErrorMessage = "tên không được để trống")]
        [StringLength(100, ErrorMessage = "tên không được vượt quá 100 ký tự")]
        public string Ten { get; set; }
        public DateTime Ngay_Tao { get; set; }= DateTime.Now;
        public ICollection<ProductDetail>? ProductDetails { get; set; }
    }

}
