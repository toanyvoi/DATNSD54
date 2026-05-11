using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATNSD54.DAO.Models
{
    public class ProductDetail
    {
        [Key]
        public int Id { get; set; }

        // FK
        [Required(ErrorMessage = "Sản phẩm không được để trống")]
        public int Product_ID { get; set; }

        [Required(ErrorMessage = "Size không được để trống")]
        public int Size { get; set; }

        [Required(ErrorMessage = "Màu sắc không được để trống")]
        public int Color { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "{0} phải lớn hơn hoặc bằng 0")]
        [Column(TypeName = "decimal(18, 2)")] // Đảm bảo độ chính xác trong DB
        public decimal Don_Gia { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải >= 0")]
        public int SL { get; set; }

        [Range(0, 100, ErrorMessage = "Sale từ 0 - 100%")]
        public int Sale { get; set; }= 0;

        public bool Trang_Thai { get; set; } = true;

        public DateTime Ngay_Tao { get; set; } = DateTime.Now;

        // Navigation
        [ForeignKey("Product_ID")]
        public Product? Product { get; set; }

        [ForeignKey("Size")]
        public Size? SizeNavigation { get; set; }

        [ForeignKey("Color")]
        public Color? ColorNavigation { get; set; }

        public ICollection<CartItem>? CartItems { get; set; }
        public ICollection<BillItem>? BillItems { get; set; }
    }
}
