using DATNSD54.DAO.Models;

namespace DATNSD54.DAO.DTO
{
    public class AccManagerDTO
    {
        public Customer AccCustomer { get; set; }
        public List<BillDTO> Listbill { get; set; }


    }
}
