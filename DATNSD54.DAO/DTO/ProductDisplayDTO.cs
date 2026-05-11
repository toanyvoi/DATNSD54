namespace DATNSD54.DAO.DTO
{
    public class ProductDisplayDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }//tên sản phẩm 
        public string URLImage { get; set; }//ảnh của sản phẩm có giá thấp nhất
        public decimal MinPrice { get; set; }//giá của sản phẩm thấp nhất
        public int Sale { get; set; }//sale %

        public string ProductTypeName { get; set; }
        public string SupplierName { get; set; }
        public string brandName { get; set; } 
        public string trangThai { get; set; }

    }
}
