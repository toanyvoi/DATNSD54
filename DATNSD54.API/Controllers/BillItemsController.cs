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
    public class BillItemsController : ControllerBase
    {
        private readonly DbContextApp _context;

        public BillItemsController(DbContextApp context)
        {
            _context = context;
        }

        // GET: api/BillItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillItem>>> GetBillItem()
        {
            return await _context.BillItem.ToListAsync();
        }

        // GET: api/BillItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillItem>> GetBillItem(int id)
        {
            var billItem = await _context.BillItem.FindAsync(id);

            if (billItem == null)
            {
                return NotFound();
            }

            return billItem;
        }

        // PUT: api/BillItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillItem(int id, BillItem billItem)
        {
            if (id != billItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(billItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillItemExists(id))
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

        // POST: api/BillItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BillItem>> PostBillItem(BillItem billItem)
        {
            _context.BillItem.Add(billItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillItem", new { id = billItem.ID }, billItem);
        }

        // DELETE: api/BillItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBillItem(int id)
        {
            var billItem = await _context.BillItem.FindAsync(id);
            if (billItem == null)
            {
                return NotFound();
            }

            _context.BillItem.Remove(billItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillItemExists(int id)
        {
            return _context.BillItem.Any(e => e.ID == id);
        }
    }
}
