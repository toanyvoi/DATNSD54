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
    public class UsersController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public UsersController(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            List<User> list = new List<User>();
            var response = await _httpClient.GetAsync("api/Users");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<User>>();
            }
            else
            {
                TempData["Error"] = "Không thể lấy danh sách người dùng từ API";
            }
            return View(list);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = new User();
            var response = await _httpClient.GetAsync($"api/Users/{id}");

            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadFromJsonAsync<User>();
            }
            else
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Role,Ma,Ten,Anhdaidien,Gioi_Tinh,Ngay_Sinh,Dia_Chi,Email,SDT,CCCD,Mat_Khau,Ngay_Tao,Trang_Thai")] User user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Users", user);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Không thể tạo người dùng mới qua API.");
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Users/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var user = await response.Content.ReadFromJsonAsync<User>();
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Role,Ma,Ten,Anhdaidien,Gioi_Tinh,Ngay_Sinh,Dia_Chi,Email,SDT,CCCD,Mat_Khau,Ngay_Tao,Trang_Thai")] User user)
        {
            if (id != user.ID) return NotFound();

            var response = await _httpClient.PutAsJsonAsync($"api/Users/{id}", user);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Cập nhật thông tin thất bại.");
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Users/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var user = await response.Content.ReadFromJsonAsync<User>();
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Users/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa người dùng thất bại.";
            return RedirectToAction(nameof(Index));
        }
    }
}