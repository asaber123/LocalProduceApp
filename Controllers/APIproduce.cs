#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocalProduceApp.Data;
using LocalProduceApp.Models;

namespace LocalProduceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIproduce : ControllerBase
    {
        private readonly LocalProduceAppDbContext _context;

        public APIproduce(LocalProduceAppDbContext context)
        {
            _context = context;
        }

        // GET: api/APIproduce
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produce>>> GetProduce()
        {
            return await _context.Produce.ToListAsync();
        }

        // GET: api/APIproduce/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produce>> GetProduce(int? id)
        {
            var produce = await _context.Produce.FindAsync(id);

            if (produce == null)
            {
                return NotFound();
            }

            return produce;
        }

        // PUT: api/APIproduce/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduce(int? id, Produce produce)
        {
            if (id != produce.ProduceId)
            {
                return BadRequest();
            }

            _context.Entry(produce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduceExists(id))
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

        // POST: api/APIproduce
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produce>> PostProduce(Produce produce)
        {
            _context.Produce.Add(produce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduce", new { id = produce.ProduceId }, produce);
        }

        // DELETE: api/APIproduce/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduce(int? id)
        {
            var produce = await _context.Produce.FindAsync(id);
            if (produce == null)
            {
                return NotFound();
            }

            _context.Produce.Remove(produce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProduceExists(int? id)
        {
            return _context.Produce.Any(e => e.ProduceId == id);
        }
    }
}
