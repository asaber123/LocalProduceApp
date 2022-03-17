#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocalProduceApp.Data;
using LocalProduceApp.Models;

namespace LocalProduceApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly LocalProduceAppDbContext _context;

        public CustomerController(LocalProduceAppDbContext context)
        {
            _context = context;
        }

        // GET: Customer
        public async Task<IActionResult> Index(string searchString)
        {
            // Storing the users email to filter the view, so that the only tables that are displayed are from the user that is logged in. 
            var user = User.Identity?.Name;
            var customer = from Customer in _context.Customer.Include(c => c.Produce)
                           select Customer;
            customer = customer.Where(s => s.Produce.ProducerEmail!.Contains(user));
            // Filtering result off array from the search string content, filtering on produce name 
            if (!String.IsNullOrEmpty(searchString))
            {
                customer = customer.Where(s => s.Produce.ProduceName!.ToLower().Contains(searchString.ToLower()));
            }

            return View(await customer.ToListAsync());


            // var localProduceAppDbContext = _context.Customer.Include(c => c.Produce);
            // return View(await localProduceAppDbContext.ToListAsync());
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.Produce)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            ViewData["ProduceId"] = new SelectList(_context.Produce, "ProduceId", "ProduceName");
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,PhoneNumber,Date,ProduceId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Changed so that the user can select producer from producer name instead of producer id.

            ViewData["ProduceId"] = new SelectList(_context.Produce, "ProduceId", "ProduceName", customer.ProduceId);
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["ProduceId"] = new SelectList(_context.Produce, "ProduceId", "ProduceName", customer.ProduceId);
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("CustomerId,CustomerName,PhoneNumber,Date,ProduceId")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProduceId"] = new SelectList(_context.Produce, "ProduceId", "ProduceName", customer.ProduceId);
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .Include(c => c.Produce)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int? id)
        {
            return _context.Customer.Any(e => e.CustomerId == id);
        }
    }
}
