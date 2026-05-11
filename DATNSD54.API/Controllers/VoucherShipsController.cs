using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DATNSD54.DAO.Data;
using DATNSD54.DAO.Models;

namespace DATNSD54.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherShipsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public VoucherShipsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/VoucherShips
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoucherShip>>> GetVoucherShip()
        {
            return await _context.VoucherShip.ToListAsync();
        }

        // GET: api/VoucherShips/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoucherShip>> GetVoucherShip(string id)
        {
            var voucherShip = await _context.VoucherShip.FindAsync(id);

            if (voucherShip == null)
            {
                return NotFound();
            }

            return voucherShip;
        }

        // PUT: api/VoucherShips/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoucherShip(string id, VoucherShip voucherShip)
        {
            if (id != voucherShip.ID)
            {
                return BadRequest();
            }

            _context.Entry(voucherShip).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoucherShipExists(id))
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

        // POST: api/VoucherShips
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VoucherShip>> PostVoucherShip(VoucherShip voucherShip)
        {
           

            _context.VoucherShip.Add(voucherShip);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VoucherShipExists(voucherShip.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVoucherShip", new { id = voucherShip.ID }, voucherShip);
        }

        // DELETE: api/VoucherShips/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoucherShip(string id)
        {
            var voucherShip = await _context.VoucherShip.FindAsync(id);
            if (voucherShip == null)
            {
                return NotFound();
            }
            try
            {
                _context.VoucherShip.Remove(voucherShip);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                // Lỗi Foreign Key thường gây ra DbUpdateException
                return BadRequest("Dữ liệu này đang được sử dụng ở bảng khác, không thể xóa!");
            }


        }

        private bool VoucherShipExists(string id)
        {
            return _context.VoucherShip.Any(e => e.ID == id);
        }
    }
}
