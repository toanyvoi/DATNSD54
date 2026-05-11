namespace DATNSD54.DAO.DTO
{
    public class CartDTO
    {
        public int Id { get; set; }//IdCustomer

        public List<CartItemDTO> cartItems { get; set; }=new List<CartItemDTO>();
    }
}
