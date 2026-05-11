using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "tên không được để trống")]
        [StringLength(100, ErrorMessage = "tên không được vượt quá 100 ký tự")]
        public string Ten { get; set; }
        public string? LogoUrl { get; set; }
        public DateTime? NgayTao { get; set; }
        public bool TrangThai { get; set; }=true;
        public ICollection<Product>? Products { get; set; }
    }
}
