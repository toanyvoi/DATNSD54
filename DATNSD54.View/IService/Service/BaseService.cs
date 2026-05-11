using System.Net.Http;

namespace DATNSD54.View.IService.Service
{
    public class BaseService
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public BaseService(IHttpClientFactory httpClientFactory, IHttpContextAccessor accessor)
        {
            // PHẢI GỌI ĐÚNG TÊN "MyAPI" ĐÃ CẤU HÌNH TRONG PROGRAM.CS
            _httpClient = httpClientFactory.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }
    }
}
