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
    public class MaintenencesController : Controller
    {
        private readonly PhoneShopContext _context;
        private readonly IWebHostEnvironment hosting;

        public MaintenencesController(PhoneShopContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            this.hosting = hosting;
        }

        // GET: Maintenences
        public async Task<IActionResult> Index()
        {
            return View(await _context.Maintenence.ToListAsync());

        }

        // GET: Maintenences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenence = await _context.Maintenence
                .FirstOrDefaultAsync(m => m.DamageId == id);
            if (maintenence == null)
            {
                return NotFound();
            }

            return View(maintenence);
        }

        // GET: Maintenences/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Maintenences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DamageId,DamMobName,DamageDescription,File,PhoneOwner,PhoneNumber")] Maintenence maintenence)
        {
            if (ModelState.IsValid)
            {

                if (maintenence.File != null)
                {


                    String rotpath = hosting.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(maintenence.File.FileName);
                    string extention = Path.GetExtension(maintenence.File.FileName);
                    maintenence.DamImg = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                    string path = Path.Combine(rotpath + "/Mobimg", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await maintenence.File.CopyToAsync(fileStream);

                    }



                    _context.Add(maintenence);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(maintenence);
        }

        // GET: Maintenences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenence = await _context.Maintenence.FindAsync(id);
            if (maintenence == null)
            {
                return NotFound();
            }
            return View(maintenence);
        }

        // POST: Maintenences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DamageId,DamMobName,DamageDescription,File,PhoneOwner,PhoneNumber")] Maintenence maintenence)
        {
            if (id != maintenence.DamageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (maintenence.File != null)
                    {


                        String rotpath = hosting.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(maintenence.File.FileName);
                        string extention = Path.GetExtension(maintenence.File.FileName);
                        maintenence.DamImg = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                        string path = Path.Combine(rotpath + "/Mobimg", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await maintenence.File.CopyToAsync(fileStream);

                        }
                        _context.Update(maintenence);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenenceExists(maintenence.DamageId))
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
            return View(maintenence);
        }

        // GET: Maintenences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenence = await _context.Maintenence
                .FirstOrDefaultAsync(m => m.DamageId == id);
            if (maintenence == null)
            {
                return NotFound();
            }

            return View(maintenence);
        }

        // POST: Maintenences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenence = await _context.Maintenence.FindAsync(id);
            _context.Maintenence.Remove(maintenence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenenceExists(int id)
        {
            return _context.Maintenence.Any(e => e.DamageId == id);
        }
    }
}
