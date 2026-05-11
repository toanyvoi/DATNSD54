using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// true: Admin
        /// false: Nhân viên
        /// </summary>
        public bool Role { get; set; } = false;

        [Required(ErrorMessage = "Mã nhân viên không được để trống")]
        [StringLength(20, ErrorMessage = "Mã tối đa 20 ký tự")]
        public string Ma { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(100, ErrorMessage = "Tên tối đa 100 ký tự")]
        public string Ten { get; set; }

        public string? Anhdaidien { get; set; }

        public bool? Gioi_Tinh { get; set; } //true Nam false nữ

        [DataType(DataType.Date)]
        public DateTime Ngay_Sinh { get; set; }

        [StringLength(255, ErrorMessage = "Địa chỉ tối đa 255 ký tự")]
        public string? Dia_Chi { get; set; }

        [EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email phải đúng định dạng (ví dụ: abc@gmail.com)")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Định dạng số điện thoại không hợp lệ")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng số 0 và có 10 chữ số")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "CCCD không được để trống")]
        [RegularExpression(@"^[0-9]{12}$", ErrorMessage = "CCCD phải đủ 12 số")]
        public string CCCD { get; set; }


        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)\S+$",
ErrorMessage = "Mật khẩu phải bao gồm chữ hoa, chữ thường, số và không được chứa khoảng trắng")]
        public string Mat_Khau { get; set; }

        public DateTime Ngay_Tao { get; set; } = DateTime.Now;

        public bool Trang_Thai { get; set; } = true;

        // Navigation
        public ICollection<Bill>? Bills { get; set; }
    }
}
