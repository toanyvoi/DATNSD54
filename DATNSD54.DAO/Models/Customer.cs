using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = " họ và tên không được để trống")]
        [StringLength(50, ErrorMessage = "họ và tên không được vượt quá 50 ký tự")]
        public string Ten { get; set; }
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Định dạng số điện thoại không hợp lệ")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng số 0 và có 10 chữ số")]
        public string SDT { get; set; }

        [EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email phải đúng định dạng (ví dụ: abc@gmail.com)")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string? Email { get; set; }
        public string? AnhDaiDien { get; set; }
        public bool? Gioi_Tinh { get; set; }// true Nam, False nữ
        public DateTime? Ngay_Sinh { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)\S+$",
ErrorMessage = "Mật khẩu phải bao gồm chữ hoa, chữ thường, số và không được chứa khoảng trắng")]
        public string Mat_Khau { get; set; }
        public DateTime Ngay_Tao { get; set; } = DateTime.Now;
        public bool Trang_Thai { get; set; } = true;

        public string? ResetToken { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Bill>? Bills { get; set; }
        public ICollection<Address>? Address { get; set; }
    }
}
