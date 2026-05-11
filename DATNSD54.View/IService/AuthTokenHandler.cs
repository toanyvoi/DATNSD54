using System.Net.Http.Headers;

namespace DATNSD54.View.IService
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;

        public AuthTokenHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // 1. Lấy Token từ Session (Key phải khớp với lúc ní Login là "JwtToken")
            var token = _accessor.HttpContext?.Session.GetString("JwtToken");

            // 2. Nếu có token thì "dán" vào Header ngay
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
