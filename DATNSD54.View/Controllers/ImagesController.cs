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
        
        public async Task<IActionResult> Create(
            int productId,
            IFormFile fileImage)
        {
            if (fileImage == null)
            {
                TempData["Error"] =
                    "Vui lòng chọn ảnh";

                return View();
            }

            // tạo tên file
            string fileName = Guid.NewGuid().ToString()
                + Path.GetExtension(fileImage.FileName);

            // đường dẫn folder
            string folder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/img/product"
            );

            // nếu chưa có thư mục thì tạo
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            // đường dẫn file
            string filePath = Path.Combine(
                folder,
                fileName
            );

            // lưu file
            using (var stream = new FileStream(
                filePath,
                FileMode.Create))
            {
                await fileImage.CopyToAsync(stream);
            }

            // tạo object image
            Image image = new Image()
            {
                Product_ID = productId,
                IMG = "/img/product/" + fileName,
                Ngay_Tao = DateTime.Now,
                Trang_Thai = true
            };

            // gọi api
            var response = await _httpClient
                .PostAsJsonAsync("api/Images", image);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(
    "Details",           // 1. Tên Action muốn quay lại (Details)
    "Products",          // 2. Tên Controller (Products) - Có thể bỏ qua nếu đang ở cùng Controller Products
    new { id = productId } // 3. Tên tham số phải khớp với cấu trúc route (thường là id chứ không phải productId)
);
            }

            TempData["Error"] = response.StatusCode == System.Net.HttpStatusCode.BadRequest
                ? "Mỗi sản phẩm chỉ được có tối đa 6 ảnh"
                : "Thêm ảnh thất bại";


            return RedirectToAction(
    "Details",           // 1. Tên Action muốn quay lại (Details)
    "Products",          // 2. Tên Controller (Products) - Có thể bỏ qua nếu đang ở cùng Controller Products
    new { id = productId } // 3. Tên tham số phải khớp với cấu trúc route (thường là id chứ không phải productId)
);
        }

        

        

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
       
        public async Task<IActionResult> DeleteConfirmed(String Url, int productId)
        {
            // Gửi yêu cầu DELETE sang API
            var response = await _httpClient.DeleteAsync($"api/Images?imgUrl={Uri.EscapeDataString(Url)}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", "Products", new { id = productId });
            }

            TempData["Error"] = "Xóa không thành công.";
            return RedirectToAction("Details","Products",new { id = productId });
        }
    }
}