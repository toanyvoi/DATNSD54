using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATNSD54.DAO.Data;
using DATNSD54.DAO.Models;
using DATNSD54.DAO.DTO;

namespace DATNSD54.View.Controllers
{
    public class SuppliersController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public SuppliersController(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index()
        {
            List<Supplier> list = new List<Supplier>();
            var response = await _httpClient.GetAsync("api/Suppliers");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<Supplier>>();
            }
            else
            {
                TempData["Error"] = "Không thể lấy dữ liệu từ API";
            }
            return View(list);
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var supplier = new Supplier();
            var response = await _httpClient.GetAsync($"api/Suppliers/{id}");

            if (response.IsSuccessStatusCode)
            {
                supplier = await response.Content.ReadFromJsonAsync<Supplier>();
            }
            else
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,SDT,Email,Dia_Chi,Ngay_Tao,Trang_Thai")] Supplier supplier)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Suppliers", supplier);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Không thể thêm mới qua API.");
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Suppliers/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var supplier = await response.Content.ReadFromJsonAsync<Supplier>();
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,SDT,Email,Dia_Chi,Ngay_Tao,Trang_Thai")] Supplier supplier)
        {
            if (id != supplier.Id) return NotFound();

            var response = await _httpClient.PutAsJsonAsync($"api/Suppliers/{id}", supplier);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Cập nhật thất bại.");
            return View(supplier);
        }

        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Suppliers/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var supplier = await response.Content.ReadFromJsonAsync<Supplier>();
            return View(supplier);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Suppliers/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa không thành công.";
            return RedirectToAction(nameof(Index));
        }
    }
}