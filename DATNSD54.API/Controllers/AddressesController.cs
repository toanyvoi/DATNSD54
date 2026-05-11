using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DATNSD54.DAO.Data;
using DATNSD54.DAO.Models;
using System.Security.Claims;

namespace DATNSD54.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly DbContextApp _context;

        public AddressesController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            var idCustomer = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;//lấy idCustomer dg đăng nhập 

            var listAddress = await _context.Address.Where(a => a.IdCustomer == int.Parse(idCustomer)).ToListAsync();
            return Ok(listAddress);
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var idCustomer = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var address = await _context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }
            if(address.IdCustomer != int.Parse(idCustomer))
                {
                return BadRequest("Bạn không có quyền truy cập địa chỉ này");
            }

            return address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address address)
        {
            var idCustomer = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (id != address.Id)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                if(address.IdCustomer != int.Parse(idCustomer)){
                    return BadRequest("Bạn không có quyền chỉnh sửa địa chỉ này");
                }

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            var idCustomer = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var newAddress = new Address
            {
                IdCustomer = int.Parse(idCustomer),
                HoTen = address.HoTen,
                DiaChiChitiet = address.DiaChiChitiet,
                SDT = address.SDT
            };
            _context.Address.Add(newAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = newAddress.Id }, newAddress);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var idCustomer = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            if(address.IdCustomer != int.Parse(idCustomer))
            {
                return BadRequest("Bạn không có quyền xóa địa chỉ này");
            }
            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.Id == id);
        }
    }
}
