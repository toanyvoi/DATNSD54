using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATNSD54.DAO.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }//khóa chính

        public int IdCustomer { get; set; }//khóa ngoại
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Địa chỉ chi tiết không được để trống")]
        [MaxLength(500, ErrorMessage = "Địa chỉ quá dài")]
        public string DiaChiChitiet { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Định dạng số điện thoại không hợp lệ")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng số 0 và có 10 chữ số")]
        public string SDT { get; set; }

        // Navigation property: Trỏ về bảng Customer
        [ForeignKey("IdCustomer")]
        public virtual Customer? Customer { get; set; }

        public ICollection<Bill>? Bills { get; set; }
    }
}
