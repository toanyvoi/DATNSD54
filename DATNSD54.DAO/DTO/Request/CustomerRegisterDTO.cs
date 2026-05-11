using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.DTO.Request
{
    public class CustomerRegisterDTO
    {
        [Required(ErrorMessage = "họ và Tên không được để trống")]
        [StringLength(50, ErrorMessage = "họ và tên không được vượt quá 50 ký tự")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Định dạng số điện thoại không hợp lệ")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải bắt đầu bằng số 0 và có 10 chữ số")]
        public string SDT { get; set; }
        [EmailAddress(ErrorMessage = "Định dạng Email không hợp lệ")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email phải đúng định dạng (ví dụ: abc@gmail.com)")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự")]
        public string? Email { get; set; }= null;
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
    ErrorMessage = "Mật khẩu phải bao gồm chữ hoa, chữ thường và số")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; }
    }
}
