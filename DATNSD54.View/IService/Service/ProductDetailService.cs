using DATNSD54.DAO.DTO;
using DATNSD54.DAO.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DATNSD54.View.IService.Service
{
    public class ProductDetailService : BaseService,IProductDetailService
    {
        public ProductDetailService(IHttpClientFactory httpClient, IHttpContextAccessor accessor) : base(httpClient, accessor) { }



        public async Task<(bool IsSuccess, string Message)> AddToCart(int IdPd, int Sl)
        {
            var response = await _httpClient.PostAsync($"api/CartItems/Customer/Add?IdPd={IdPd}&sl={Sl}", null);

            if (response.IsSuccessStatusCode)
            {
                return (true, "Thành công");
            }

            // Đọc nội dung lỗi từ BadRequest("...") của API
            var errorMessage = await response.Content.ReadAsStringAsync();
            return (false, errorMessage);
        }

        public async Task<ProductDTO> Show(int? Id)
        {
            var response = await _httpClient.GetAsync($"api/Products/{Id}");

            if (response.IsSuccessStatusCode) { 
                return await response.Content.ReadFromJsonAsync<ProductDTO>();
            }
            return null;
        }

    }
}
