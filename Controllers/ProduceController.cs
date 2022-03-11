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
//using System.Drawing;
//using LazZiya.ImageResize;

namespace LocalProduceApp.Controllers
{
    public class ProduceController : Controller
    {
        private readonly LocalProduceAppDbContext _context;
        //Adds Iwebhostenvironment to be able to read path to img file folder
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProduceController(LocalProduceAppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        // GET: Produce
        public async Task<IActionResult> Index(string searchString, string user)
        {
            var produce = from Produce in _context.Produce.Where(s => s.ProducerEmail!.Contains(user))
                          select Produce;

            if (!String.IsNullOrEmpty(searchString))
            {
                produce = produce.Where(s => s.ProduceName!.ToLower().Contains(searchString.ToLower()));
            }

            return View(await produce.ToListAsync());
        }

        // GET: Produce/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produce = await _context.Produce
                .Include(p => p.Producer)
                .FirstOrDefaultAsync(m => m.ProduceId == id);
            if (produce == null)
            {
                return NotFound();
            }

            return View(produce);
        }

        // GET: Produce/Create
        public IActionResult Create (string user)
        {
            //Does so that the admin that is logged in can only see and edit their own produce. 
            var producer = from Producer in _context.Producer
                           select Producer;

            producer = producer.Where(s => s.ProducerEmail!.Contains(user));
            //Changed so that the admin can se the different producer company name instead of email. 
            ViewData["ProducerId"] = new SelectList(producer, "ProducerId", "ProducerName");
            return View();
        }

        // POST: Produce/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProduceId,ProduceName,Price,PickupPlace,ProducerEmail,Theme,Description,ImgFile,ProducerId")] Produce produce)
        {
            if (ModelState.IsValid)
            {
                //check if image is uploaded
                if(produce.ImgFile != null){
                    //upload image and get path to img folder 
                    string wwwRootPath = _hostEnvironment.WebRootPath;//path to wwroot folder
                    string filename=Path.GetFileNameWithoutExtension(produce.ImgFile.FileName); //get filename for image that has been sent through the form
                    string extention = Path.GetExtension(produce.ImgFile.FileName); //Extention in filename to get an unique name
                    filename = filename + DateTime.Now.ToString("yyyMMddssff") + extention; //creating a "unique file name
                    //adding filename to model
                    produce.ImgName = filename;

                    //path to folder
                    string path = Path.Combine(wwwRootPath + "/img/" + filename);
                    
                    //move img to img folder
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await produce.ImgFile.CopyToAsync(fileStream);
                    }
                   // CreateImageFiles(filename);

                }
                _context.Add(produce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProducerId"] = new SelectList(_context.Producer, "ProducerId", "ProducerName", produce.ProducerId); 

            return View(produce);
        }
        // Create thumbnail by cropping


        //Resize images
        // private void CreateImageFiles(string filename){
        //     string wwwRootPath = _hostEnvironment.WebRootPath;
        //     //create thumbnail
        //     using(var img = Image.FromFile(Path.Combine(wwwRootPath + "/img/", filename)))
        //     {
        //         img.Scale(200, 200).SaveAs(Path.Combine(wwwRootPath + "/img/thumb_", filename));
        //     }
        // }
        // GET: Produce/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produce = await _context.Produce.FindAsync(id);
            if (produce == null)
            {
                return NotFound();
            }
            ViewData["ProducerId"] = new SelectList(_context.Producer, "ProducerId", "ProducerName", produce.ProducerId);
            return View(produce);
        }

        // POST: Produce/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("ProduceId,ProduceName,Price,PickupPlace,ProducerEmail,Theme,Description,ImgName,ProducerId")] Produce produce)
        {
            if (id != produce.ProduceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produce);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduceExists(produce.ProduceId))
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
            ViewData["ProducerId"] = new SelectList(_context.Producer, "ProducerId", "ProducerName", produce.ProducerId);
            return View(produce);
        }


        // GET: Produce/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produce = await _context.Produce
                .Include(p => p.Producer)
                .FirstOrDefaultAsync(m => m.ProduceId == id);
            if (produce == null)
            {
                return NotFound();
            }

            return View(produce);
        }

        // POST: Produce/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var produce = await _context.Produce.FindAsync(id);
            _context.Produce.Remove(produce);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduceExists(int? id)
        {
            return _context.Produce.Any(e => e.ProduceId == id);
        }
    }
}
