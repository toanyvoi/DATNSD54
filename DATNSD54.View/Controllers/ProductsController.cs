using DATNSD54.DAO.Models;
using DATNSD54.DAO.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;

namespace DATNSD54.View.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductsController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyAPI");
        }

        // Hàm hỗ trợ tải dữ liệu cho Dropdown List (Brand, Supplier, ProductType)
        private async Task LoadDropdowns()
        {
            try
            {
                // Giả định bạn có các API GET danh sách này. Nếu đường dẫn API khác, bạn hãy đổi lại nhé.
                var brands = await _httpClient.GetFromJsonAsync<List<Brand>>("api/Brands") ?? new List<Brand>();
                var suppliers = await _httpClient.GetFromJsonAsync<List<Supplier>>("api/Suppliers") ?? new List<Supplier>();
                var productTypes = await _httpClient.GetFromJsonAsync<List<ProductType>>("api/ProductTypes") ?? new List<ProductType>();

                ViewBag.Brands = new SelectList(brands, "Id", "Ten");
                ViewBag.Suppliers = new SelectList(suppliers, "Id", "Ten");
                ViewBag.ProductTypes = new SelectList(productTypes, "Id", "Ten");
            }
            catch
            {
                // Xử lý lỗi nếu API chưa có sẵn
                ViewBag.Brands = new SelectList(new List<Brand>(), "Id", "Ten");
                ViewBag.Suppliers = new SelectList(new List<Supplier>(), "Id", "Ten");
                ViewBag.ProductTypes = new SelectList(new List<ProductType>(), "Id", "Ten");
            }
        }

        // 1. GET: Index
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("api/Products");
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadFromJsonAsync<List<ProductDisplayDTO>>();
                return View(list ?? new List<ProductDisplayDTO>());
            }
            TempData["Error"] = "Lỗi kết nối API lấy danh sách.";
            return View(new List<ProductDisplayDTO>());
        }

        // 2. GET: Details
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"api/Products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<ProductDTO>();
                if (product != null) return View(product);
            }
            return NotFound();
        }

        // 3. GET: Create
        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("api/Products", product);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Thêm sản phẩm thành công!";
                    return RedirectToAction(nameof(Index));
                }
            }
            await LoadDropdowns();
            TempData["Error"] = "Vui lòng kiểm tra lại thông tin.";
            return View(product);
        }

        // 4. GET: Edit
        // 4. GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            // Chỉ gọi duy nhất API trả về Raw Product
            var response = await _httpClient.GetAsync($"api/Products/Raw/{id}");

            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<Product>();
                await LoadDropdowns();
                return View(product);
            }

            TempData["Error"] = "Lỗi: Không lấy được dữ liệu gốc của sản phẩm.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Products/{id}", product);
                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "Cập nhật thành công!";
                    return RedirectToAction(nameof(Index));
                }
            }
            await LoadDropdowns();
            return View(product);
        }

        // 5. GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"api/Products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var product = await response.Content.ReadFromJsonAsync<ProductDTO>();
                return View(product);
            }
            return NotFound();
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Products/{id}");
            if (response.IsSuccessStatusCode)
                TempData["Success"] = "Xóa thành công!";
            else
                TempData["Error"] = "Xóa thất bại!";
            return RedirectToAction(nameof(Index));
        }
    }
}