namespace DATNSD54.DAO.DTO
{
    public class CartItemDTO
    {
        public int id { get; set; }
        public int cart_id { get; set; }
        public int product_id { get; set; }
        public string nameProduct { get; set; }
        public string UrlIMG { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal price { get; set; }
        public int sale { get; set; }
        public int product_detail_id { get; set; }
        public int so_luong { get; set; }
        public bool trangthai { get; set; }
    }
}
