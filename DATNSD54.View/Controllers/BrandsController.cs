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
    public class BrandsController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public BrandsController(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
            List<Brand> list = new List<Brand>();
            var response = await _httpClient.GetAsync("api/Brands");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<Brand>>();
            }
            else
            {
                TempData["Error"] = "Không thể lấy danh sách thương hiệu từ API";
            }
            return View(list);
        }

        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Brands/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var brand = await response.Content.ReadFromJsonAsync<Brand>();
            return View(brand);
        }

        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,LogoUrl,NgayTao,TrangThai")] Brand brand)
        {
            // Gửi dữ liệu thương hiệu mới sang API
            var response = await _httpClient.PostAsJsonAsync("api/Brands", brand);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Không thể thêm mới thương hiệu. Vui lòng kiểm tra lại.");
            return View(brand);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Brands/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var brand = await response.Content.ReadFromJsonAsync<Brand>();
            return View(brand);
        }

        // POST: Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,LogoUrl,NgayTao,TrangThai")] Brand brand)
        {
            if (id != brand.Id) return NotFound();

            // Gửi yêu cầu cập nhật (PUT) sang API
            var response = await _httpClient.PutAsJsonAsync($"api/Brands/{id}", brand);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Cập nhật thương hiệu thất bại.");
            return View(brand);
        }

        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Brands/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var brand = await response.Content.ReadFromJsonAsync<Brand>();
            return View(brand);
        }

        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Gửi yêu cầu xóa sang API
            var response = await _httpClient.DeleteAsync($"api/Brands/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa thương hiệu không thành công.";
            return RedirectToAction(nameof(Index));
        }
    }
}