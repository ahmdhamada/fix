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
using Microsoft.AspNetCore.Authorization;

namespace FixFactorSystem.Controllers
{
  
    public class NewPhonesController : Controller
    {
        private readonly PhoneShopContext _context;
        private readonly IWebHostEnvironment hosting;
        public NewPhonesController(PhoneShopContext context, IWebHostEnvironment hosting)
        {
            _context = context;
            this.hosting = hosting;
        }

        // GET: NewPhones
        //[Authorize(Roles = "Admin")]
       
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewPhone.ToListAsync());
        }

        // GET: NewPhones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newPhone = await _context.NewPhone
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newPhone == null)
            {
                return NotFound();
            }

            return View(newPhone);
        }

        // GET: NewPhones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewPhones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
      //  [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create([Bind("Id,MobName,Ram,Storage,Price,File")] NewPhone newPhone)
        {
            if (ModelState.IsValid)
            {
              

              //  string FileName = string.Empty;
                if (newPhone.File != null)
                {


                     String rotpath = hosting.WebRootPath;
                     string fileName = Path.GetFileNameWithoutExtension(newPhone.File.FileName);
                     string extention = Path.GetExtension(newPhone.File.FileName);
                     newPhone.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                     string path = Path.Combine(rotpath + "/Mobimg", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await newPhone.File.CopyToAsync(fileStream);
                       
                    }

                    





                    //  string NewPhones = Path.Combine(hosting.WebRootPath, "Mobimg");
                    //  FileName = newPhone.File.FileName;

                    //   string fullpath = Path.Combine(NewPhones, FileName);
                    //  newPhone.File.CopyTo(new FileStream(fullpath, FileMode.Create));


                    //   newPhone.Image = FileName;

                    _context.Add(newPhone);
                    await _context.SaveChangesAsync();
                   // TempData["mydata"] = _context;
              
                //  return RedirectToAction("Index");
                 return   RedirectToAction("Create", "NewPhoneDetails", "newPhone");
                }
            }
            return View(newPhone);
        }

        // GET: NewPhones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newPhone = await _context.NewPhone.FindAsync(id);
            if (newPhone == null)
            {
                return NotFound();
            }
            return View(newPhone);
        }

        // POST: NewPhones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MobName,Ram,Storage,Price,File")] NewPhone newPhone)
        {
            if (id != newPhone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (newPhone.File != null)
                    {


                        String rotpath = hosting.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(newPhone.File.FileName);
                        string extention = Path.GetExtension(newPhone.File.FileName);
                        newPhone.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                        string path = Path.Combine(rotpath + "/Mobimg", fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await newPhone.File.CopyToAsync(fileStream);

                        }



                        _context.Update(newPhone);
                        await _context.SaveChangesAsync();
                    }
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!NewPhoneExists(newPhone.Id))
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
            return View(newPhone);
        }

        // GET: NewPhones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newPhone = await _context.NewPhone
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newPhone == null)
            {
                return NotFound();
            }

            return View(newPhone);
        }


      
           
        

        // POST: NewPhones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newPhone = await _context.NewPhone.FindAsync(id);
            _context.NewPhone.Remove(newPhone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewPhoneExists(int id)
        {
            return _context.NewPhone.Any(e => e.Id == id);
        }
    }
}
