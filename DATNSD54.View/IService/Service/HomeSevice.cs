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

        public Task<List<ProductDTO>> SearchProducts( string? textSearch)
        {


            var response = _httpClient.GetAsync($"api/Products/Search?text={textSearch}").Result;
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<List<ProductDTO>>();
            }
            return new Task<List<ProductDTO>>(() => new List<ProductDTO>());
        }
    }
}
