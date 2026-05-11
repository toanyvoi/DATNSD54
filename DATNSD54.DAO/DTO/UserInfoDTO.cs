namespace DATNSD54.DAO.DTO
{
    public class UserInfoDTO
    {
        public int ID { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        public string Email { get; set; }
        public string SDT { get; set; }
        public bool Role { get; set; } // true: Admin, false: Nhân viên
        public string RoleName { get; set; }
    }
}
