using DATNSD54.DAO.DTO;
using DATNSD54.DAO.DTO.Request;
using DATNSD54.DAO.Models;

namespace DATNSD54.View.IService
{
    public interface IAuthService
    {
        Task<List<Customer>> Show();
        Task<RegisterResponseDTO> CustomerRegister(string Name, string SDT, string? Email, string MatKhau,string ConfirmPassword);
        Task<LoginResponseDTO> LoginCustomer(string username, string password);
        Task<LoginResponseDTO> LoginUser(string username, string password);

        Task<AccManagerDTO> GetAccManager();
        Task<bool> UpdateProfile(string? name, string? email, string? sdt, string? avatar);

        Task<bool> CancelBillAsync(int billId);
        Task<bool> ForgotPassword(string Email);
        Task<bool> ResetPassword(string token, string newPass);
    }
}
