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
    public class UsedPhoneDetailsController : Controller
    {
        private readonly PhoneShopContext _context;
        private readonly IWebHostEnvironment hosting;
        public UsedPhoneDetailsController(PhoneShopContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            this.hosting = hosting;
        }

        // GET: UsedPhoneDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.UsedPhoneDetails.ToListAsync());
        }

        // GET: UsedPhoneDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usedPhoneDetails = await _context.UsedPhoneDetails
                .FirstOrDefaultAsync(m => m.Idu == id);
            if (usedPhoneDetails == null)
            {
                return NotFound();
            }

            return View(usedPhoneDetails);
        }

        // GET: UsedPhoneDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsedPhoneDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idu,MobNameu,Ramu,Storageu,Camerau,File,Color,Display,Battary,AddDetails,Price")] UsedPhoneDetails usedPhoneDetails)
        {
            if (ModelState.IsValid)
            {
                if (usedPhoneDetails.File != null)
                {


                    String rotpath = hosting.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(usedPhoneDetails.File.FileName);
                    string extention = Path.GetExtension(usedPhoneDetails.File.FileName);
                    usedPhoneDetails.Imgu = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    string path = Path.Combine(rotpath + "/Mobimg", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await usedPhoneDetails.File.CopyToAsync(fileStream);

                    }


                    _context.Add(usedPhoneDetails);
                    await _context.SaveChangesAsync();
                    // NewPhone newPhone = TempData["mydata"] as NewPhone;
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(usedPhoneDetails);
        }

        // GET: UsedPhoneDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usedPhoneDetails = await _context.UsedPhoneDetails.FindAsync(id);
            if (usedPhoneDetails == null)
            {
                return NotFound();
            }
            return View(usedPhoneDetails);
        }

        // POST: UsedPhoneDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idu,MobNameu,Ramu,Storageu,Camerau,File,Color,Display,Battary,AddDetails,Price")] UsedPhoneDetails usedPhoneDetails)
        {
            if (id != usedPhoneDetails.Idu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (usedPhoneDetails.File != null)
                    {


                        String rotpath = hosting.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(usedPhoneDetails.File.FileName);
                        string extention = Path.GetExtension(usedPhoneDetails.File.FileName);
                        usedPhoneDetails.Imgu = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                        string path = Path.Combine(rotpath + "/Mobimg", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await usedPhoneDetails.File.CopyToAsync(fileStream);

                        }
                        _context.Update(usedPhoneDetails);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsedPhoneDetailsExists(usedPhoneDetails.Idu))
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
            return View(usedPhoneDetails);
        }

        // GET: UsedPhoneDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usedPhoneDetails = await _context.UsedPhoneDetails
                .FirstOrDefaultAsync(m => m.Idu == id);
            if (usedPhoneDetails == null)
            {
                return NotFound();
            }

            return View(usedPhoneDetails);
        }

        // POST: UsedPhoneDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usedPhoneDetails = await _context.UsedPhoneDetails.FindAsync(id);
            _context.UsedPhoneDetails.Remove(usedPhoneDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsedPhoneDetailsExists(int id)
        {
            return _context.UsedPhoneDetails.Any(e => e.Idu == id);
        }
    }
}
