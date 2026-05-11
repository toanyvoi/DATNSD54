using DATNSD54.DAO.Models;
using DATNSD54.View.IService;
using Microsoft.AspNetCore.Mvc;

namespace DATNSD54.View.Controllers
{
    public class ProductDetailController : Controller
    {
        private readonly ILogger<ProductDetailController> _logger;
        private readonly IProductDetailService _productDetailService;
        public ProductDetailController(ILogger<ProductDetailController> logger, IProductDetailService productDetailService)
        {
            _logger = logger;
            _productDetailService = productDetailService;
        }

        public async Task< IActionResult> Index(int id)
        {
            if (id == null) {return NotFound(); }
            
            var procuduct = await _productDetailService.Show(id);

            return View(procuduct);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int? idPd, int SL, int Id)
        {
            // Kiểm tra đầu vào
            if (idPd == null || SL <= 0)
            {
                TempData["ErrorMessage"] = "Số lượng hoặc sản phẩm không hợp lệ.";
                return RedirectToAction("Index", "ProductDetail", new { id = Id });
            }

            // Kiểm tra Session Login
            var idCart = HttpContext.Session.GetInt32("userId");
            if (idCart == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            // Gọi Service và hứng kết quả (Tuple)
            var (isOk, msg) = await _productDetailService.AddToCart(idPd.Value, SL);

            if (!isOk)
            {
                // Dùng TempData để "vác" cái lỗi qua trang Redirect
                TempData["ErrorMessage"] = msg;
                return RedirectToAction("Index", "ProductDetail", new { id = Id });
            }

            // Nếu ok thì qua giỏ hàng
            return RedirectToAction("Cart", "Pay", new { id = idCart });
        }
    }
}
