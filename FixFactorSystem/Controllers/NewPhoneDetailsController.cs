using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FixFactorSystem.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace FixFactorSystem.Controllers
{
    public class NewPhoneDetailsController : Controller
    {
        private readonly PhoneShopContext _context;
        private readonly IWebHostEnvironment hosting;
        public NewPhoneDetailsController(PhoneShopContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            this.hosting = hosting;
        }

        // GET: NewPhoneDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewPhoneDetails.ToListAsync());
        }

        // GET: NewPhoneDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newPhoneDetails = await _context.NewPhoneDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newPhoneDetails == null)
            {
                return NotFound();
            }

            return View(newPhoneDetails);
        }

        // GET: NewPhoneDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewPhoneDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MobName,Ram,Storage,Camera,Image,Color,Display,Battary,AddDetails,Price,File")] NewPhoneDetails newPhoneDetails)
        {
            if (ModelState.IsValid)
            {

                if (newPhoneDetails.File != null)
                {


                    String rotpath = hosting.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(newPhoneDetails.File.FileName);
                    string extention = Path.GetExtension(newPhoneDetails.File.FileName);
                    newPhoneDetails.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    string path = Path.Combine(rotpath + "/Mobimg", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await newPhoneDetails.File.CopyToAsync(fileStream);

                    }

                   
                    _context.Add(newPhoneDetails);
                    await _context.SaveChangesAsync();
                   // NewPhone newPhone = TempData["mydata"] as NewPhone;
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(newPhoneDetails);
        }

        // GET: NewPhoneDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newPhoneDetails = await _context.NewPhoneDetails.FindAsync(id);
            if (newPhoneDetails == null)
            {
                return NotFound();
            }
            return View(newPhoneDetails);
        }

        // POST: NewPhoneDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MobName,Ram,Storage,Camera,File,Color,Display,Battary,AddDetails,Price")] NewPhoneDetails newPhoneDetails)
        {
            if (id != newPhoneDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (newPhoneDetails.File != null)
                    {


                        String rotpath = hosting.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(newPhoneDetails.File.FileName);
                        string extention = Path.GetExtension(newPhoneDetails.File.FileName);
                        newPhoneDetails.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                        string path = Path.Combine(rotpath + "/Mobimg", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await newPhoneDetails.File.CopyToAsync(fileStream);

                        }
                        _context.Update(newPhoneDetails);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewPhoneDetailsExists(newPhoneDetails.Id))
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
            return View(newPhoneDetails);
        }

        // GET: NewPhoneDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newPhoneDetails = await _context.NewPhoneDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newPhoneDetails == null)
            {
                return NotFound();
            }

            return View(newPhoneDetails);
        }

        // POST: NewPhoneDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newPhoneDetails = await _context.NewPhoneDetails.FindAsync(id);
            _context.NewPhoneDetails.Remove(newPhoneDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewPhoneDetailsExists(int id)
        {
            return _context.NewPhoneDetails.Any(e => e.Id == id);
        }
    }
}
