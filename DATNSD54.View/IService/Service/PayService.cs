using DATNSD54.DAO.DTO;
using DATNSD54.DAO.Models;
using DATNSD54.View.IService.Service;
using DATNSD54.View.IService;
using System.Net.Http;

public class PayService : BaseService, IPayService
{
    public PayService(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
    : base(httpClient, accessor) { }

    public async Task<CartDTO> GetCart(int? id)
    {
        // Gọi đến API lấy giỏ hàng, ní check lại route api/Carts cho đúng nhé
        var response = await _httpClient.GetAsync($"api/Carts/{id}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<CartDTO>();
        }

        return new CartDTO(); // Trả về object rỗng nếu lỗi
    }
    public async Task<BillDTO> GetBillUnVerified(int? BillId)
    {
        // Link API: GET api/Buy/{id}
        var response = await _httpClient.GetAsync($"api/Buy/{BillId}");
        return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<BillDTO>() : new BillDTO();
    }

    public async Task<BillDTO> CreateUnverifiedBillAsync()
    {
        // Link API: POST api/Buy/Create?Customerid=...
        var response = await _httpClient.PostAsync($"api/Buy/PostBillUnVerified",null);

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<BillDTO>();
        var errorContent = await response.Content.ReadAsStringAsync();
        var statusCode = response.StatusCode; 
        throw new Exception(await response.Content.ReadAsStringAsync());
    }

    public async Task<bool> ConfirmBillAsync(int id, int pttt)
    {
        // Link API: POST api/Buy/Confirm
        var response = await _httpClient.PostAsJsonAsync($"api/Buy/ConfirmUnVerified?ID={id}&pttt={pttt}", new {});
        return response.IsSuccessStatusCode;
    }
}