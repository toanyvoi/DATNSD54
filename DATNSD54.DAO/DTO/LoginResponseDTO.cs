namespace DATNSD54.DAO.DTO
{
    public class LoginResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string UserType { get; set; } // "Customer" hoặc "User"
        public CustomerInfoDTO? CustomerInfo { get; set; }
        // Thay vì dùng object, hãy dùng đúng Class này
        public UserInfoDTO? UserInfo { get; set; }
    }
}
