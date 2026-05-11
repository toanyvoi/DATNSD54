using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATNSD54.DAO.Models
{
    public class BillItem
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Bill")]
        public int Bill_ID { get; set; }
        [ForeignKey("ProductDetail")]
        public int Product_Detail_ID { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "{0} phải lớn hơn hoặc bằng 0")]
        public decimal Don_Gia { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "{0} phải lớn hơn hoặc bằng 0")]
        [Column(TypeName = "decimal(18, 2)")] // Đảm bảo độ chính xác trong DB
        public decimal Gia_Ban { get; set; } // = don_gia sau sale
        [Range(1, int.MaxValue, ErrorMessage = "{0} phải lớn hơn hoặc bằng 1")]

        public int So_Luong { get; set; }

        public virtual Bill? Bill { get; set; }
        public virtual ProductDetail? ProductDetail { get; set; }
    }
}
