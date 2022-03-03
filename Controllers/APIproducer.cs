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
    public class APIproducer : ControllerBase
    {
        private readonly LocalProduceAppDbContext _context;

        public APIproducer(LocalProduceAppDbContext context)
        {
            _context = context;
        }

        // GET: api/APIproducer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producer>>> GetProducer()
        {
            return await _context.Producer.ToListAsync();
        }

        // GET: api/APIproducer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producer>> GetProducer(int? id)
        {
            var producer = await _context.Producer.FindAsync(id);

            if (producer == null)
            {
                return NotFound();
            }

            return producer;
        }

        // PUT: api/APIproducer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducer(int? id, Producer producer)
        {
            if (id != producer.ProducerId)
            {
                return BadRequest();
            }

            _context.Entry(producer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProducerExists(id))
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

        // POST: api/APIproducer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Producer>> PostProducer(Producer producer)
        {
            _context.Producer.Add(producer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducer", new { id = producer.ProducerId }, producer);
        }

        // DELETE: api/APIproducer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducer(int? id)
        {
            var producer = await _context.Producer.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }

            _context.Producer.Remove(producer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProducerExists(int? id)
        {
            return _context.Producer.Any(e => e.ProducerId == id);
        }
    }
}
