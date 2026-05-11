using DATNSD54.DAO.Models;
using Microsoft.AspNetCore.Mvc;

namespace DATNSD54.View.Controllers
{
    public class AddressesController : Controller
    {
        protected readonly HttpClient _httpClient;

        public AddressesController(IHttpClientFactory httpClientFactory)
        {
            // Giả sử ní đã cấu hình "MyAPI" trong Program.cs
            _httpClient = httpClientFactory.CreateClient("MyAPI");
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            List<Address> list = new List<Address>();
            // API Route: api/Addresses
            var response = await _httpClient.GetAsync("api/Addresses");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<Address>>();
            }
            else
            {
                TempData["Error"] = "Không thể lấy danh sách địa chỉ.";
            }
            return View(list);
        }
        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Gọi API để lấy chi tiết 1 địa chỉ theo ID
            var response = await _httpClient.GetAsync($"api/Addresses/{id}");

            if (response.IsSuccessStatusCode)
            {
                var address = await response.Content.ReadFromJsonAsync<Address>();
                return View(address);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
                     response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                // Trường hợp API trả về lỗi "Không có quyền" (do ní check userId ở API)
                TempData["Error"] = "Bạn không có quyền xem thông tin địa chỉ này.";
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
        // GET: Addresses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Addresses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address address)
        {
            // API của ní tự lấy userId từ Claim nên không cần gán IdCustomer ở đây
            var response = await _httpClient.PostAsJsonAsync("api/Addresses", address);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Thêm địa chỉ thành công!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Không thể thêm mới địa chỉ. Vui lòng thử lại.");
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Addresses/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var address = await response.Content.ReadFromJsonAsync<Address>();
            return View(address);
        }

        // POST: Addresses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Address address)
        {
            if (id != address.Id) return NotFound();

            var response = await _httpClient.PutAsJsonAsync($"api/Addresses/{id}", address);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "Cập nhật thành công!";
                return RedirectToAction(nameof(Index));
            }

            var errorMsg = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError("", errorMsg ?? "Cập nhật thất bại.");
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Addresses/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var address = await response.Content.ReadFromJsonAsync<Address>();
            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Addresses/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa không thành công.";
            return RedirectToAction(nameof(Index));
        }
    }
}
