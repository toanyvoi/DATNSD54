using DATNSD54.DAO.Models;
using DATNSD54.View.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DATNSD54.View.Controllers
{
    
    public class PayController : Controller
    {
        private readonly ILogger<PayController> _logger;
        private readonly IPayService _payService;
        public PayController(ILogger<PayController> logger, IPayService payService)
        {
            _logger = logger;
            _payService = payService;
        }
        public async Task<IActionResult> Cart()
        {
            var role = HttpContext.Session.GetString("UserRole");
            if(role == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            if (role == "Customer")
            {
                var IdCart = HttpContext.Session.GetInt32("userId");
                var cart = await _payService.GetCart(IdCart);
                if (cart == null) { return NotFound(); }
                return View(cart);

            }
            else // Là Admin hoặc Staff (User)
            {

                return NotFound();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateBill()
        {
            var userId = HttpContext.Session.GetInt32("userId");
            if (userId == null) return RedirectToAction("Login", "Account");

            try
            {
                // Tạo hóa đơn tạm
                var bill = await _payService.CreateUnverifiedBillAsync();

                // Đẩy thẳng sang Action ThanhToan trong cùng Controller Pay, kèm theo tham số id
                return RedirectToAction("ThanhToan", new { id = bill.ID });
            }
            catch (Exception ex)
            {
                // Nếu lỗi thì quay về giỏ hàng kèm thông báo lỗi
                TempData["Error"] = ex.Message;
                return RedirectToAction("Cart");
            }
        }

        // 3. TRANG VIEW: Hiển thị trang thanhtoan.cshtml
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> ThanhToan(int id) // id này là BillId
        {
            var billDto = await _payService.GetBillUnVerified(id);
            var sessionUserId = HttpContext.Session.GetInt32("userId");
            if (sessionUserId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (billDto == null || billDto.Customer_ID != sessionUserId || billDto.Trang_Thai != 0)
            {
                
                return RedirectToAction("Cart");
            }
            if (billDto == null || billDto.ID == 0)
            {
                return RedirectToAction("Cart");
            }

            // Trả về trang thanhtoan.cshtml cùng với dữ liệu BillDTO
            return View(billDto);
        }

        // 4. CHỨC NĂNG: Xác nhận mua hàng cuối cùng (Gọi khi nhấn "Xác nhận đơn" ở trang thanhtoan)
        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(int id,int pttt)
        {
            

            var result = await _payService.ConfirmBillAsync(id,pttt);
            if (result)
            {
                return RedirectToAction("ThanhCong");
            }

            return Json(new { success = false, message = "Có lỗi xảy ra khi xác nhận đơn hàng." });
        }

        [HttpGet]
        public IActionResult ThanhCong()
        {
            // Cứ gọi là hiện, không hỏi han gì thêm
            return View();
        }
    }
}
