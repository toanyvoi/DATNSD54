using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATNSD54.DAO.Models
{
    public class Cart
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("Customer")]
        public int Customer_ID { get; set; }
        public DateTime Ngay_Tao { get; set; }
        public int Trang_Thai { get; set; } = 0;
        public virtual ICollection<CartItem>? CartItems { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
