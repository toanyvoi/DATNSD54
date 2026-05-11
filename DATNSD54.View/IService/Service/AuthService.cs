using DATNSD54.DAO.DTO;
using DATNSD54.DAO.DTO.Request;
using DATNSD54.DAO.Models;

namespace DATNSD54.View.IService.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        // Không cần _httpContextAccessor ở đây nữa vì máy lọc ở Program.cs đã giữ nó rồi!

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            // Triệu hồi con client đã được gắn máy lọc tự động
            _httpClient = httpClientFactory.CreateClient("MyAPI");
        }


        public async Task<List<Customer>> Show()
        {

            var response = await _httpClient.GetAsync("api/Customers");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Customer>>();
            }
            return new List<Customer>();
        }

        // Đăng nhập Customer
        public async Task<LoginResponseDTO> LoginCustomer(string username, string password)
        {
            var loginData = new { Username = username, Password = password };

            var response = await _httpClient.PostAsJsonAsync("api/Auth/login-customer", loginData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                return result;
            }

            // Xử lý lỗi
            var errorContent = await response.Content.ReadAsStringAsync();
            return new LoginResponseDTO
            {
                Success = false,
                Message = "Đăng nhập thất bại. Vui lòng thử lại!"
            };
        }

        // Đăng nhập User
        public async Task<LoginResponseDTO> LoginUser(string username, string password)
        {
            var loginData = new { Username = username, Password = password };

            var response = await _httpClient.PostAsJsonAsync("api/Auth/login-user", loginData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
                return result;
            }

            // Xử lý lỗi
            var errorContent = await response.Content.ReadAsStringAsync();
            return new LoginResponseDTO
            {
                Success = false,
                Message = "Đăng nhập thất bại. Vui lòng thử lại!"
            };
        }

        public async Task<RegisterResponseDTO> CustomerRegister(string Name, string SDT, string? Email, string MatKhau, string ConfirmPassword)
        {
            CustomerRegisterDTO customerRegisterDTO = new CustomerRegisterDTO()
            {
                Name = Name,
                SDT = SDT,
                Email = Email,
                Password = MatKhau,
                ConfirmPassword = ConfirmPassword
            };

            var response = await _httpClient.PostAsJsonAsync("api/Auth/register-customer", customerRegisterDTO);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<RegisterResponseDTO>();
                return result;
            }

            // Xử lý lỗi
            var errorContent = await response.Content.ReadAsStringAsync();
            return new RegisterResponseDTO
            {
                Success = false,
                Message = errorContent
            };
        }

        public async Task<AccManagerDTO> GetAccManager()
        {
            var response = await _httpClient.GetAsync("api/Auth/customer-Acc-View");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AccManagerDTO>();
                return result;
            }
            return null;
        }

        public async Task<bool> UpdateProfile(string? name, string? email, string? sdt, string? avatar)
        {
            var data = new
            {
                Ten = name,
                Email = email,
                SDT = sdt,
                AnhDaiDien = avatar
            };

            var response = await _httpClient.PutAsJsonAsync("api/Auth/update-profile", data);

            return response.IsSuccessStatusCode;
        }
    }
}
