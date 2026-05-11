using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATNSD54.DAO.Data;
using DATNSD54.DAO.Models;

namespace DATNSD54.View.Controllers
{
    public class SizesController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public SizesController(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }

        // GET: Sizes
        public async Task<IActionResult> Index()
        {
            List<Size> list = new List<Size>();
            var response = await _httpClient.GetAsync("api/Sizes");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<Size>>();
            }
            else
            {
                TempData["Error"] = "Không thể lấy dữ liệu từ API";
            }
            return View(list);
        }

        // GET: Sizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Sizes/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var size = await response.Content.ReadFromJsonAsync<Size>();
            return View(size);
        }

        // GET: Sizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sizes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ma,Ten,Ngay_Tao")] Size size)
        {
            // 1. Gửi yêu cầu POST kèm dữ liệu dạng JSON sang API
            var response = await _httpClient.PostAsJsonAsync("api/Sizes", size);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            // Nếu lỗi (ví dụ trùng mã), hiện thông báo
            ModelState.AddModelError("", "Không thể thêm mới. Vui lòng kiểm tra lại dữ liệu.");
            return View(size);
        }

        // GET: Sizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Sizes/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var size = await response.Content.ReadFromJsonAsync<Size>();
            return View(size);
        }

        // POST: Sizes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ma,Ten,Ngay_Tao")] Size size)
        {
            if (id != size.Id) return NotFound();

            // Gửi yêu cầu PUT sang API
            var response = await _httpClient.PutAsJsonAsync($"api/Sizes/{id}", size);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Cập nhật thất bại. Vui lòng kiểm tra lại.");
            return View(size);
        }

        // GET: Sizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Sizes/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var size = await response.Content.ReadFromJsonAsync<Size>();
            return View(size);
        }

        // POST: Sizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Gửi yêu cầu DELETE sang API
            var response = await _httpClient.DeleteAsync($"api/Sizes/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa không thành công.";
            return RedirectToAction(nameof(Index));
        }
    }
}