using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATNSD54.DAO.Models
{
    public class VoucherHistory
    {
        [Key]
        public int ID { get; set; }
        public int Bill_ID { get; set; }
        public string? Voucher_ID { get; set; }
        public string? VoucherShip_ID { get; set; }
        public DateTime? Bill_Date { get; set; }

        public int Customer_ID { get; set; }
        public decimal? Discount_Amount { get; set; }
        public decimal? Shipping_Discount { get; set; }
        [ForeignKey("Bill_ID")]
        public virtual Bill? Bill { get; set; }
        [ForeignKey("Voucher_ID")]
        public virtual Voucher? Voucher { get; set; }
        [ForeignKey("VoucherShip_ID")]
        public virtual VoucherShip? VoucherShip { get; set; }
        [ForeignKey("Customer_ID")]
        public virtual Customer? Customer { get; set; }

        // Navigation properties

    }
}
