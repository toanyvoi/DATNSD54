using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATNSD54.DAO.Models
{
    public class CartItem
    {
        [Key]
        public int ID { get; set; }
        public int Cart_ID { get; set; }
        public int Product_Detail_ID { get; set; }
        public int So_Luong { get; set; }
        public DateTime Ngay_Tao { get; set; }= DateTime.Now;

        public bool trangthai { get; set; } = true;

        [ForeignKey("Cart_ID")]
        public virtual Cart? Cart { get; set; }
        [ForeignKey("Product_Detail_ID")]
        public virtual ProductDetail? ProductDetail { get; set; }
    }
}
