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
    public class CustomersController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public CustomersController(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            List<Customer> list = new List<Customer>();
            var response = await _httpClient.GetAsync("api/Customers");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<Customer>>();
            }
            else
            {
                TempData["Error"] = "Không thể lấy danh sách khách hàng từ API";
            }
            return View(list);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Customers/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var customer = await response.Content.ReadFromJsonAsync<Customer>();
            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,SDT,Email,AnhDaiDien,Gioi_Tinh,Ngay_Sinh,Mat_Khau,Ngay_Tao,Trang_Thai")] Customer customer)
        {
            // 1. Gửi yêu cầu POST kèm dữ liệu dạng JSON sang API
            var response = await _httpClient.PostAsJsonAsync("api/Customers", customer);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            // Nếu lỗi (ví dụ trùng Email/SDT), hiện thông báo
            ModelState.AddModelError("", "Không thể thêm mới. Vui lòng kiểm tra lại dữ liệu.");
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Customers/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var customer = await response.Content.ReadFromJsonAsync<Customer>();
            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,SDT,Email,AnhDaiDien,Gioi_Tinh,Ngay_Sinh,Mat_Khau,Ngay_Tao,Trang_Thai")] Customer customer)
        {
            if (id != customer.Id) return NotFound();

            // Gửi yêu cầu PUT sang API
            var response = await _httpClient.PutAsJsonAsync($"api/Customers/{id}", customer);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Cập nhật thất bại. Vui lòng kiểm tra lại.");
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Customers/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var customer = await response.Content.ReadFromJsonAsync<Customer>();
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Gửi yêu cầu DELETE sang API
            var response = await _httpClient.DeleteAsync($"api/Customers/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa không thành công.";
            return RedirectToAction(nameof(Index));
        }
    }
}