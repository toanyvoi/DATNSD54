using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DATNSD54.DAO.Data;
using DATNSD54.DAO.Models;
using Azure;
using DATNSD54.DAO.DTO;

namespace DATNSD54.View.Controllers
{
    public class ProductTypesController : Controller
    {
        //gọi httpclient để call api
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public ProductTypesController(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }
        //
        

        // GET: ProductTypes
        public async Task<IActionResult> Index()
        {
            List<ProductType> list = new List<ProductType>();
            var response = await _httpClient.GetAsync($"api/ProductTypes");
            
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<ProductType>>();
            }
            else
            {
                // Ghi log lỗi hoặc thông báo nếu cần
                TempData["Error"] = "Không thể lấy dữ liệu từ API";
            }
            return View(list);
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = new ProductType  ();
            var response = await _httpClient.GetAsync($"api/ProductTypes/{id}");
            if (response.IsSuccessStatusCode)
            {
                productType = await response.Content.ReadFromJsonAsync<ProductType>();
            }
            else
            {
                // Ghi log lỗi hoặc thông báo nếu cần
                TempData["Error"] = "Không thể lấy dữ liệu từ API";
            }
            
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ten,Ma,Ngay_Tao,Trang_Thai")] ProductType productType)
        {
            // 1. Gửi yêu cầu POST kèm dữ liệu dạng JSON sang API
            var response = await _httpClient.PostAsJsonAsync("api/ProductTypes", productType);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            // Nếu lỗi (ví dụ trùng mã), hiện thông báo
            ModelState.AddModelError("", "Không thể thêm mới. Vui lòng kiểm tra lại dữ liệu.");
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/ProductTypes/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var productType = await response.Content.ReadFromJsonAsync<ProductType>();
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,Ma,Ngay_Tao,Trang_Thai")] ProductType productType)
        {
            if (id != productType.Id) return NotFound();

            // Gửi yêu cầu PUT sang API
            var response = await _httpClient.PutAsJsonAsync($"api/ProductTypes/{id}", productType);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Cập nhật thất bại.");
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/ProductTypes/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var productType = await response.Content.ReadFromJsonAsync<ProductType>();
            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Gửi yêu cầu DELETE sang API
            var response = await _httpClient.DeleteAsync($"api/ProductTypes/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa không thành công.";
            return RedirectToAction(nameof(Index));
        }

        
    }
}
