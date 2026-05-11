using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATNSD54.DAO.Models;
using System.Net.Http.Json;

namespace DATNSD54.View.Controllers
{
    public class ColorsController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public ColorsController(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }

        // GET: Colors
        public async Task<IActionResult> Index()
        {
            List<Color> list = new List<Color>();
            var response = await _httpClient.GetAsync("api/Colors");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<Color>>();
            }
            else
            {
                TempData["Error"] = "Không thể lấy danh sách màu sắc từ API";
            }
            return View(list);
        }

        // GET: Colors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Colors/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var color = await response.Content.ReadFromJsonAsync<Color>();
            return View(color);
        }

        // GET: Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Colors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ma,Ten,Ngay_Tao")] Color color)
        {
            // 1. Gửi yêu cầu POST kèm dữ liệu dạng JSON sang API
            var response = await _httpClient.PostAsJsonAsync("api/Colors", color);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            // Nếu lỗi (ví dụ trùng mã màu), hiện thông báo
            ModelState.AddModelError("", "Không thể thêm mới. Vui lòng kiểm tra lại dữ liệu.");
            return View(color);
        }

        // GET: Colors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Colors/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var color = await response.Content.ReadFromJsonAsync<Color>();
            return View(color);
        }

        // POST: Colors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ma,Ten,Ngay_Tao")] Color color)
        {
            if (id != color.Id) return NotFound();

            // Gửi yêu cầu PUT sang API
            var response = await _httpClient.PutAsJsonAsync($"api/Colors/{id}", color);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Cập nhật thất bại. Vui lòng kiểm tra lại.");
            return View(color);
        }

        // GET: Colors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Colors/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var color = await response.Content.ReadFromJsonAsync<Color>();
            return View(color);
        }

        // POST: Colors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Gửi yêu cầu DELETE sang API
            var response = await _httpClient.DeleteAsync($"api/Colors/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa không thành công.";
            return RedirectToAction(nameof(Index));
        }
    }
}