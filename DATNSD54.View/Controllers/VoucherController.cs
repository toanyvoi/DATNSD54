using DATNSD54.DAO.Models;
using Microsoft.AspNetCore.Mvc;

namespace DATNSD54.View.Controllers
{
    public class VoucherController : Controller
    {
        protected readonly HttpClient _httpClient;

        public VoucherController(IHttpClientFactory httpClientFactory)
        {
            // Đảm bảo tên client "MyAPI" đã được cấu hình trong Program.cs của dự án View
            _httpClient = httpClientFactory.CreateClient("MyAPI");
        }

        // 1. GET: Danh sách Voucher
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Vouchers"); // Lưu ý: API thường dùng số nhiều "Vouchers"
                if (response.IsSuccessStatusCode)
                {
                    var list = await response.Content.ReadFromJsonAsync<List<Voucher>>();
                    return View(list ?? new List<Voucher>());
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Không thể kết nối đến máy chủ API: " + ex.Message;
            }
            return View(new List<Voucher>());
        }

        // 2. GET: Chi tiết Voucher
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var response = await _httpClient.GetAsync($"api/Vouchers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var voucher = await response.Content.ReadFromJsonAsync<Voucher>();
                return View(voucher);
            }
            return NotFound();
        }

        // 3. GET: Trang tạo mới
        public IActionResult Create() => View();

        // POST: Lưu Voucher mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Gửi yêu cầu POST sang API
                    var response = await _httpClient.PostAsJsonAsync("api/Vouchers", voucher);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Success"] = $"Voucher {voucher.ID} đã được tạo thành công!";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {

                        // 1. Đọc nội dung lỗi dạng chuỗi trơn (Plain Text) từ API nhả về
                        string errorRaw = await response.Content.ReadAsStringAsync();

                        // 2. In thẳng cụ cục lỗi này lên màn hình giao diện để mình nhìn bằng mắt luôn ní ơi!
                        ModelState.AddModelError(string.Empty, "Chi tiết lỗi từ API: " + errorRaw);
                        
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Lỗi hệ thống: " + ex.Message);
                }
            }
            return View(voucher);
        }

        // 4. GET: Trang chỉnh sửa
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var response = await _httpClient.GetAsync($"api/Vouchers/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var voucher = await response.Content.ReadFromJsonAsync<Voucher>();
            return View(voucher);
        }

        // POST: Cập nhật Voucher
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Voucher voucher)
        {
            if (id != voucher.ID) return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Vouchers/{id}", voucher);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Cập nhật Voucher thành công!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Không thể cập nhật Voucher.");
            }
            return View(voucher);
        }

        // 5. GET: Trang xác nhận xóa
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var response = await _httpClient.GetAsync($"api/Vouchers/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var voucher = await response.Content.ReadFromJsonAsync<Voucher>();
            return View(voucher);
        }

        // POST: Thực hiện xóa
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/Vouchers/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Đã xóa Voucher thành công!";
            }
            else
            {
                TempData["Error"] = "Xóa thất bại. Vui lòng kiểm tra lại.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
