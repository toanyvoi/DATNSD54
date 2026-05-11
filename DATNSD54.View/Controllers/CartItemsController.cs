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
    public class CartItemsController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public CartItemsController(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }

        // Hàm hỗ trợ load dữ liệu cho các SelectList từ API
        private async Task LoadDropdownData(CartItem cartItem = null)
        {
            var carts = await _httpClient.GetFromJsonAsync<List<Cart>>("api/Carts") ?? new List<Cart>();
            var productDetails = await _httpClient.GetFromJsonAsync<List<ProductDetail>>("api/ProductDetails") ?? new List<ProductDetail>();

            ViewData["Cart_ID"] = new SelectList(carts, "ID", "ID", cartItem?.Cart_ID);
            ViewData["Product_Detail_ID"] = new SelectList(productDetails, "Id", "Id", cartItem?.Product_Detail_ID);
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            List<CartItem> list = new List<CartItem>();
            var response = await _httpClient.GetAsync("api/CartItems");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<CartItem>>();
            }
            else
            {
                TempData["Error"] = "Không thể lấy danh sách giỏ hàng từ API";
            }
            return View(list);
        }

        // GET: CartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/CartItems/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var cartItem = await response.Content.ReadFromJsonAsync<CartItem>();
            return View(cartItem);
        }

        // GET: CartItems/Create
        public async Task<IActionResult> Create()
        {
            await LoadDropdownData();
            return View();
        }

        // POST: CartItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Cart_ID,Product_Detail_ID,So_Luong,Ngay_Tao")] CartItem cartItem)
        {
            var response = await _httpClient.PostAsJsonAsync("api/CartItems", cartItem);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Không thể thêm sản phẩm vào giỏ hàng. Vui lòng thử lại.");
            await LoadDropdownData(cartItem);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/CartItems/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var cartItem = await response.Content.ReadFromJsonAsync<CartItem>();
            await LoadDropdownData(cartItem);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Cart_ID,Product_Detail_ID,So_Luong,Ngay_Tao")] CartItem cartItem)
        {
            if (id != cartItem.ID) return NotFound();

            var response = await _httpClient.PutAsJsonAsync($"api/CartItems/{id}", cartItem);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Cập nhật số lượng thất bại.");
            await LoadDropdownData(cartItem);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/CartItems/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var cartItem = await response.Content.ReadFromJsonAsync<CartItem>();
            return View(cartItem);
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/CartItems/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa sản phẩm khỏi giỏ hàng thất bại.";
            return RedirectToAction(nameof(Index));
        }
    }
}