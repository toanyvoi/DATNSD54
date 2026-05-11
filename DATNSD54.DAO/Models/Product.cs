using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATNSD54.DAO.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string? Ma { get; set; }
        [Required(ErrorMessage = "tên không được để trống")]
        [StringLength(100, ErrorMessage = "tên không được vượt quá 100 ký tự")]
        public string Ten { get; set; }
        public int? Nam_SX { get; set; }
        public string? Mo_Ta { get; set; }
       
        // FK
        public int Product_Type_ID { get; set; }
        public int Supplier_ID { get; set; }
        public int Brand_ID { get; set; }

        // Navigation
        [ForeignKey("Product_Type_ID")]
        public ProductType? ProductType { get; set; }
        [ForeignKey("Supplier_ID")]
        public Supplier? Supplier { get; set; }
        [ForeignKey("Brand_ID")]
        public Brand? Brand { get; set; }

        public ICollection<ProductDetail>? ProductDetails { get; set; }
        public ICollection<Image>? images { get; set; }
    }

}
