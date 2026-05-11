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
    public class CartsController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public CartsController(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }

        // Hàm hỗ trợ load dữ liệu cho SelectList từ API
        private async Task LoadCustomerDropdown(int? selectedId = null)
        {
            var customers = await _httpClient.GetFromJsonAsync<List<Customer>>("api/Customers") ?? new List<Customer>();
            // Ở đây tôi đổi từ "Mat_Khau" sang "Ten" để hiển thị hợp lý hơn trên giao diện
            ViewData["Customer_ID"] = new SelectList(customers, "Id", "Ten", selectedId);
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            List<Cart> list = new List<Cart>();
            var response = await _httpClient.GetAsync("api/Carts");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<Cart>>();
            }
            else
            {
                TempData["Error"] = "Không thể lấy danh sách giỏ hàng từ API";
            }
            return View(list);
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Carts/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var cart = await response.Content.ReadFromJsonAsync<Cart>();
            return View(cart);
        }

        // GET: Carts/Create
        public async Task<IActionResult> Create()
        {
            await LoadCustomerDropdown();
            return View();
        }

        // POST: Carts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Customer_ID,Ngay_Tao,Trang_Thai")] Cart cart)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Carts", cart);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Không thể tạo giỏ hàng. Vui lòng kiểm tra lại.");
            await LoadCustomerDropdown(cart.Customer_ID);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Carts/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var cart = await response.Content.ReadFromJsonAsync<Cart>();
            await LoadCustomerDropdown(cart.Customer_ID);
            return View(cart);
        }

        // POST: Carts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Customer_ID,Ngay_Tao,Trang_Thai")] Cart cart)
        {
            if (id != cart.ID) return NotFound();

            var response = await _httpClient.PutAsJsonAsync($"api/Carts/{id}", cart);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Cập nhật giỏ hàng thất bại.");
            await LoadCustomerDropdown(cart.Customer_ID);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Carts/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var cart = await response.Content.ReadFromJsonAsync<Cart>();
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Carts/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa giỏ hàng thất bại.";
            return RedirectToAction(nameof(Index));
        }
    }
}