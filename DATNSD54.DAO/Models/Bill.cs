using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.Models
{
    public class Bill
    {
        [Key]
        public int ID { get; set; }
        public int Customer_ID { get; set; }
        public int? User_ID { get; set; }

        // Khóa ngoại nối tới Voucher (để null nếu đơn hàng không dùng voucher)
        public string? Voucher_ID { get; set; }
        public decimal? Discount_Amount { get; set; } = 0; // Số tiền giảm giá từ voucher, mặc định là 0 nếu không dùng voucher
        public decimal? ShipCost { get; set; } = 0; // Chi phí vận chuyển, mặc định là 0 nếu không có chi phí vận chuyển
        public string? VoucherShip_ID { get; set; }

        public DateTime Ngay_Tao { get; set; }
        public decimal Gia_Goc { get; set; } = 0;
        public int Phuong_Thuc_Thanh_Toan { get; set; } = 0; // 0: Tiền mặt, 1: Chuyển khoản
        public decimal Thanh_Tien { get; set; } = 0;
        public int Address_Id { get; set; }
        
        [Range(0,5 , ErrorMessage = "Trạng thái không hợp lệ")]
        public int Trang_Thai { get; set; } = 0;// 0:Chưa xác nhận 1: Đang xử lý, 2: Đang giao, 3: Đã giao,4:Thanh Công, 5: Đã hủy

        [ForeignKey("Address_Id")]
        public virtual Address? Address { get; set; }

        // Navigation Properties
        [ForeignKey("Voucher_ID")]
        public virtual Voucher? Voucher { get; set; }
        [ForeignKey("VoucherShip_ID")]
        public virtual VoucherShip? VoucherShip { get; set; }

        [ForeignKey("Customer_ID")]
        public virtual Customer? Customer { get; set; }

        [ForeignKey("User_ID")]
        public virtual User? User { get; set; }

        public virtual ICollection<BillItem>? BillItems { get; set; }
    }
}
