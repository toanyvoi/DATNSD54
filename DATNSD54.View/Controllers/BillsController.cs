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
    public class BillsController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public BillsController(IHttpClientFactory httpClient, IHttpContextAccessor accessor)
        {
            _httpClient = httpClient.CreateClient("MyAPI");
            _httpContextAccessor = accessor;
        }

        // Hàm hỗ trợ load tất cả dữ liệu ngoại vi cho Dropdown từ API
        private async Task LoadDropdownData(Bill bill = null)
        {
            var addresses = await _httpClient.GetFromJsonAsync<List<Address>>("api/Addresses") ?? new List<Address>();
            var customers = await _httpClient.GetFromJsonAsync<List<Customer>>("api/Customers") ?? new List<Customer>();
            var users = await _httpClient.GetFromJsonAsync<List<User>>("api/Users") ?? new List<User>();
            var vouchers = await _httpClient.GetFromJsonAsync<List<Voucher>>("api/Vouchers") ?? new List<Voucher>();
            var voucherShips = await _httpClient.GetFromJsonAsync<List<VoucherShip>>("api/VoucherShips") ?? new List<VoucherShip>();

            ViewData["Address_Id"] = new SelectList(addresses, "IdCustomer", "DiaChiChitiet", bill?.Address_Id);
            ViewData["Customer_ID"] = new SelectList(customers, "Id", "Ten", bill?.Customer_ID);
            ViewData["User_ID"] = new SelectList(users, "ID", "CCCD", bill?.User_ID);
            ViewData["Voucher_ID"] = new SelectList(vouchers, "ID", "Ten", bill?.Voucher_ID);
            ViewData["VoucherShip_ID"] = new SelectList(voucherShips, "ID", "Ten", bill?.VoucherShip_ID);
        }

        // GET: Bills
        public async Task<IActionResult> Index()
        {
            List<Bill> list = new List<Bill>();
            var response = await _httpClient.GetAsync("api/Bills");

            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadFromJsonAsync<List<Bill>>();
            }
            else
            {
                TempData["Error"] = "Không thể lấy danh sách hóa đơn từ API";
            }
            return View(list);
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Bills/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var bill = await response.Content.ReadFromJsonAsync<Bill>();
            return View(bill);
        }

        // GET: Bills/Create
        public async Task<IActionResult> Create()
        {
            await LoadDropdownData();
            return View();
        }

        // POST: Bills/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Customer_ID,User_ID,Voucher_ID,VoucherShip_ID,Ngay_Tao,Gia_Goc,Phuong_Thuc_Thanh_Toan,Thanh_Tien,Address_Id,Trang_Thai")] Bill bill)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Bills", bill);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Lỗi khi tạo hóa đơn mới.");
            await LoadDropdownData(bill);
            return View(bill);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Bills/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var bill = await response.Content.ReadFromJsonAsync<Bill>();
            await LoadDropdownData(bill);
            return View(bill);
        }

        // POST: Bills/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Customer_ID,User_ID,Voucher_ID,VoucherShip_ID,Ngay_Tao,Gia_Goc,Phuong_Thuc_Thanh_Toan,Thanh_Tien,Address_Id,Trang_Thai")] Bill bill)
        {
            if (id != bill.ID) return NotFound();

            var response = await _httpClient.PutAsJsonAsync($"api/Bills/{id}", bill);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Cập nhật hóa đơn thất bại.");
            await LoadDropdownData(bill);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var response = await _httpClient.GetAsync($"api/Bills/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var bill = await response.Content.ReadFromJsonAsync<Bill>();
            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Bills/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            TempData["Error"] = "Xóa hóa đơn thất bại.";
            return RedirectToAction(nameof(Index));
        }
    }
}