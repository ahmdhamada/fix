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
    public class UsedPhonesController : Controller
    {
        private readonly PhoneShopContext _context;
        private readonly IWebHostEnvironment hosting;
        public UsedPhonesController(PhoneShopContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            this.hosting = hosting;
        }

        // GET: UsedPhones
        public async Task<IActionResult> Index()
        {
            return View(await _context.UsedPhone.ToListAsync());
        }

        // GET: UsedPhones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usedPhone = await _context.UsedPhone
                .FirstOrDefaultAsync(m => m.Idu == id);
            if (usedPhone == null)
            {
                return NotFound();
            }

            return View(usedPhone);
        }

        // GET: UsedPhones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsedPhones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idu,MobNameu,Ramu,Storageu,Priceu,File")] UsedPhone usedPhone)
        {
            if (ModelState.IsValid)
            {
                if (usedPhone.File != null)
                {


                    String rotpath = hosting.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(usedPhone.File.FileName);
                    string extention = Path.GetExtension(usedPhone.File.FileName);
                    usedPhone.Imgu = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    string path = Path.Combine(rotpath + "/Mobimg", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await usedPhone.File.CopyToAsync(fileStream);

                    }


                    _context.Add(usedPhone);
                    await _context.SaveChangesAsync();
                    // NewPhone newPhone = TempData["mydata"] as NewPhone;
                    return RedirectToAction("Create", "UsedPhoneDetails");
                   // return RedirectToAction(nameof(Index));
                }
            }
            return View(usedPhone);
        }


        // GET: UsedPhones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usedPhone = await _context.UsedPhone.FindAsync(id);
            if (usedPhone == null)
            {
                return NotFound();
            }
            return View(usedPhone);
        }

        // POST: UsedPhones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idu,MobNameu,Ramu,Storageu,Priceu,File")] UsedPhone usedPhone)
            {
                if (id != usedPhone.Idu)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                try
                {
                    if (usedPhone.File != null)
                    {


                        String rotpath = hosting.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(usedPhone.File.FileName);
                        string extention = Path.GetExtension(usedPhone.File.FileName);
                        usedPhone.Imgu = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                        string path = Path.Combine(rotpath + "/Mobimg", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await usedPhone.File.CopyToAsync(fileStream);

                        }
                        _context.Update(usedPhone);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsedPhoneExists(usedPhone.Idu))
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
            
            return View(usedPhone);
        }

        // GET: UsedPhones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usedPhone = await _context.UsedPhone
                .FirstOrDefaultAsync(m => m.Idu == id);
            if (usedPhone == null)
            {
                return NotFound();
            }

            return View(usedPhone);
        }

        // POST: UsedPhones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usedPhone = await _context.UsedPhone.FindAsync(id);
            _context.UsedPhone.Remove(usedPhone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsedPhoneExists(int id)
        {
            return _context.UsedPhone.Any(e => e.Idu == id);
        }
    }
}
