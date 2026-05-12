using DATNSD54.DAO.DTO;
using DATNSD54.DAO.Models;

namespace DATNSD54.View.IService.Service
{
    public class HomeSevice : BaseService, IHomeService
    {
        public HomeSevice(IHttpClientFactory httpClient, IHttpContextAccessor accessor): base(httpClient, accessor)
        {

        }

        public Task<List<ProductDisplayDTO>> GetAllProducts()
        {
            var response = _httpClient.GetAsync("api/Products").Result;
            if(response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<ProductDisplayDTO>>();
            }
            return new Task<List<ProductDisplayDTO>>(() => new List<ProductDisplayDTO>());
        }

        public async Task<SearchProductDTO> SearchProducts(string? textSearch)
        {
            string encodedText = Uri.EscapeDataString(textSearch ?? "");
            // Dùng await thay vì .Result
            var response = await _httpClient.GetAsync($"api/Products/Search/{encodedText}");

            if (response.IsSuccessStatusCode)
            {
                // Phải await khi đọc Json
                return await response.Content.ReadFromJsonAsync<SearchProductDTO>() ?? new SearchProductDTO();
            }

            return new SearchProductDTO(); // Trả về object rỗng nếu lỗi
        }
    }
}
