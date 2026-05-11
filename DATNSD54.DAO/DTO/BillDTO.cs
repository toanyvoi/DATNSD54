using DATNSD54.DAO.Models;
using System.ComponentModel.DataAnnotations;

namespace DATNSD54.DAO.DTO
{
    public class BillDTO
    {
        [Key]
        public int ID { get; set; }
        public int Customer_ID { get; set; }
        public string Customer_Name { get; set; }
        public int? User_ID { get; set; }

        // Khóa ngoại nối tới Voucher (để null nếu đơn hàng không dùng voucher)
        public string? Voucher_ID { get; set; }
        public Voucher? voucher { get; set; }
        public decimal? Shipcost { get; set; } = 0; // Chi phí vận chuyển, mặc định là 0 nếu không có chi phí vận chuyển
        public string? VoucherShip_ID { get; set; }
        public VoucherShip? voucherShip { get; set; }
        public DateTime Ngay_Tao { get; set; }
        public decimal Gia_Goc { get; set; } = 0;
        public int Phuong_Thuc_Thanh_Toan { get; set; } = 0; // 0: Tiền mặt, 1: Chuyển khoản
        public decimal Thanh_Tien { get; set; } = 0;
        public int Dia_Chi_Id { get; set; }
        public string SDT_Nguoi_Nhan { get; set; }
        public string Ten_Nguoi_Nhan { get; set; }
        public string Dia_Chi_Chi_tiet { get; set; }
        [Range(0, 2, ErrorMessage = "Trạng thái không hợp lệ")]
        public int Trang_Thai { get; set; } = 0;// 0:Chưa xác thực 0: Đang xử lý, 1: Đang giao, 2: Đã giao,4:Thanh Công, 3: Đã hủy

        public List<BillItemDTO>? BillItems { get; set; } = new List<BillItemDTO>();

        public List<CartItemDTO>? CartItems { get; set; } = new List<CartItemDTO>();

        public List<Address> listAddress { get; set; } = new List<Address>();
    }
}
