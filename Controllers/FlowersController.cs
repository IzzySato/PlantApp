#nullable disable
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlantApp.Data;
using PlantApp.Models;
using Microsoft.AspNetCore.Authorization;
using PlantApp.ViewModels;

namespace PlantApp.Controllers
{
    public class FlowersController : CommonController
    {

        public FlowersController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment) : base (context, hostEnvironment)
        {
        }

        // GET: Flowers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Flower.ToListAsync());
        }

        // GET: Flowers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flower = await _context.Flower
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flower == null)
            {
                return NotFound();
            }

            return View(flower);
        }

        // POST: Flowers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(PlantViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);

                Flower flower = new Flower
                {
                    Name = model.Name,
                    Description = model.Description,
                    IsAnnual = model.IsAnnual,
                    Image = uniqueFileName,
                };
                _context.Add(flower);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Flowers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flower = await _context.Flower.FindAsync(id);
            if (flower == null)
            {
                return NotFound();
            }
            return View(flower);
        }

        // POST: Flowers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Edit(int? id, Flower flower, IFormFile file)
        {
            if (id == null)
            {
                return NotFound();
            }

            Flower f = await _context.Flower.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (f == null)
            {
                return NotFound();
            }

            string uniqueFileName = null;

            if (file != null && file.Length != 0)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                f.Image = uniqueFileName;
            }

            f.Name = flower.Name;
            f.Description = flower.Description;
            f.IsAnnual = flower.IsAnnual;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        // GET: Flowers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flower = await _context.Flower
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flower == null)
            {
                return NotFound();
            }

            return View(flower);
        }

        // POST: Flowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flower = await _context.Flower.FindAsync(id);
            _context.Flower.Remove(flower);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShowSearchResult(string SearchPhrase)
        {
            return View("Index", await _context.Flower.Where(f =>
                f.Name.Contains(SearchPhrase)).ToListAsync());
        }

    }
}
