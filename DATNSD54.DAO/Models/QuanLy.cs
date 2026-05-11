using System.ComponentModel.DataAnnotations.Schema;

namespace DATNSD54.DAO.Models
{
    public class QuanLy
    {
        public int ID { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal phiShip { get; set; }
    }
}
