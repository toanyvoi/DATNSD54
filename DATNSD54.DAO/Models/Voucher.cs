using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.Models
{
    public class Voucher
    {
        [Key]
        [RegularExpression(@"^[^\s]+$", ErrorMessage = "Không được nhập dấu cách!")]
        [StringLength(50, ErrorMessage = "Mã voucher tối đa 25 ký tự")]
        public string ID { get; set; }

        [Required(ErrorMessage = "Tên voucher không được để trống")]
        [StringLength(100, ErrorMessage = "Tên tối đa 100 ký tự")]
        public string Ten { get; set; } // Tên chương trình giảm giá

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        public DateTime Ngay_Bat_Dau { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        public DateTime Ngay_Ket_Thuc { get; set; }

        [Range(0, 100, ErrorMessage = "Tỷ lệ giảm phải từ 0 - 100%")]
        public decimal Ty_Le_Giam { get; set; } // % vd 20, 10, 30

        [Range(0, double.MaxValue, ErrorMessage = "Đơn hàng tối thiểu phải >= 0")]
        public decimal Don_Hang_Toi_Thieu { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giảm tối đa phải >= 0")]
        public decimal Giam_Toi_Da { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải >= 0")]
        public int So_Luong { get; set; }

        public DateTime Ngay_Tao { get; set; } = DateTime.Now;

        /// <summary>
        /// 0: Ngừng áp dụng
        /// 1: Đang áp dụng
        /// 2: Hết hạn
        /// </summary>
        [Range(0, 1, ErrorMessage = "Trạng thái không hợp lệ")]
        public int Trang_Thai { get; set; } = 1;//1 : Còn, ): hết

        // Navigation
        public virtual ICollection<Bill>? Bills { get; set; }
    }
}
