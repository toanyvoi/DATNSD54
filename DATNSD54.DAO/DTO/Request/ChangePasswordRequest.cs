using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.DTO.Request
{
    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
    ErrorMessage = "Mật khẩu phải bao gồm chữ hoa, chữ thường và số")]
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
