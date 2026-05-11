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
    public class ImagesController : Controller
    {
        // Gọi HttpClient để call api
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public ImagesController(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }

        // Hàm hỗ trợ load dữ liệu cho SelectList (Dropdown Sản phẩm) từ API
        private async Task LoadProductDropdown(int? selectedId = null)
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>("api/Products") ?? new List<Product>();
            ViewData["Product_ID"] = new SelectList(products, "Id", "Ten", selectedId);
        }

        // GET: Images
        public async Task<IActionResult> Index()
        {
            List<Image> list = new List<Image>();
            var response = await _httpClient.GetAsync("api/Images");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<Image>>();
            }
            else
            {
                TempData["Error"] = "Không thể lấy danh sách hình ảnh từ API";
            }
            return View(list);
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Images/{id}");
            if (response.IsSuccessStatusCode)
            {
                var image = await response.Content.ReadFromJsonAsync<Image>();
                return View(image);
            }

            return NotFound();
        }

        // GET: Images/Create
        public async Task<IActionResult> Create()
        {
            await LoadProductDropdown();
            return View();
        }

        // POST: Images/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Product_ID,IMG,Ngay_Tao,Trang_Thai")] Image image)
        {
            // 1. Gửi yêu cầu POST kèm dữ liệu dạng JSON sang API
            var response = await _httpClient.PostAsJsonAsync("api/Images", image);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            // Nếu lỗi, hiện thông báo và load lại dropdown
            ModelState.AddModelError("", "Không thể thêm mới. Vui lòng kiểm tra lại dữ liệu.");
            await LoadProductDropdown(image.Product_ID);
            return View(image);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Images/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var image = await response.Content.ReadFromJsonAsync<Image>();
            await LoadProductDropdown(image.Product_ID);
            return View(image);
        }

        // POST: Images/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Product_ID,IMG,Ngay_Tao,Trang_Thai")] Image image)
        {
            if (id != image.ID) return NotFound();

            // Gửi yêu cầu PUT sang API
            var response = await _httpClient.PutAsJsonAsync($"api/Images/{id}", image);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Cập nhật thất bại.");
            await LoadProductDropdown(image.Product_ID);
            return View(image);
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Images/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var image = await response.Content.ReadFromJsonAsync<Image>();
            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Gửi yêu cầu DELETE sang API
            var response = await _httpClient.DeleteAsync($"api/Images/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa không thành công.";
            return RedirectToAction(nameof(Index));
        }
    }
}