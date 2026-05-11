using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using DATNSD54.DAO.Models;

namespace DATNSD54.View.Controllers
{
    public class VoucherShipsController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public VoucherShipsController(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            // Tên "MyAPI" phải khớp với cấu hình trong Program.cs dự án View của mày
            _httpClient = httpClient.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }

        // GET: VoucherShips (Trang danh sách)
        public async Task<IActionResult> Index()
        {
            List<VoucherShip> list = new List<VoucherShip>();
            var response = await _httpClient.GetAsync("api/VoucherShips");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<VoucherShip>>();
            }
            else
            {
                TempData["Error"] = "Không thể lấy dữ liệu từ API";
            }
            return View(list);
        }

        // GET: VoucherShips/Details/ID_CUA_VOUCHER
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var response = await _httpClient.GetAsync($"api/VoucherShips/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var voucher = await response.Content.ReadFromJsonAsync<VoucherShip>();
            return View(voucher);
        }

        // GET: VoucherShips/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VoucherShips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoucherShip voucher)
        {
            // Gửi yêu cầu POST sang API
            var response = await _httpClient.PostAsJsonAsync("api/VoucherShips", voucher);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            // Nếu lỗi (ví dụ trùng mã ID), hiện thông báo
            ModelState.AddModelError("", "Không thể thêm mới. Mã Voucher có thể đã tồn tại.");
            return View(voucher);
        }

        // GET: VoucherShips/Edit/ID_CUA_VOUCHER
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var response = await _httpClient.GetAsync($"api/VoucherShips/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var voucher = await response.Content.ReadFromJsonAsync<VoucherShip>();
            return View(voucher);
        }

        // POST: VoucherShips/Edit/ID_CUA_VOUCHER
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, VoucherShip voucher)
        {
            if (id != voucher.ID) return NotFound();

            var response = await _httpClient.PutAsJsonAsync($"api/VoucherShips/{id}", voucher);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Cập nhật thất bại. Vui lòng kiểm tra lại.");
            return View(voucher);
        }

        // GET: VoucherShips/Delete/ID_CUA_VOUCHER
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var response = await _httpClient.GetAsync($"api/VoucherShips/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var voucher = await response.Content.ReadFromJsonAsync<VoucherShip>();
            return View(voucher);
        }

        // POST: VoucherShips/Delete/ID_CUA_VOUCHER
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/VoucherShips/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa không thành công.";
            return RedirectToAction(nameof(Index));
        }
    }
}