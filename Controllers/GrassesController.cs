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
using Microsoft.AspNetCore.Authorization;
using PlantApp.Models;
using PlantApp.ViewModels;

namespace PlantApp.Controllers
{
    public class GrassesController : CommonController
    {

        public GrassesController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment) : base(context, hostEnvironment)
        {
        }

        // GET: Grasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Grass.ToListAsync());
        }

        // GET: Grasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grass = await _context.Grass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grass == null)
            {
                return NotFound();
            }

            return View(grass);
        }

        // POST: Grasses/Create
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

                Grass grass = new Grass
                {
                    Name = model.Name,
                    Description = model.Description,
                    Image = uniqueFileName,
                };
                _context.Add(grass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Grasses/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grass = await _context.Grass.FindAsync(id);
            if (grass == null)
            {
                return NotFound();
            }
            return View(grass);
        }

        // POST: Grasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Image")] Grass grass)
        {
            if (id != grass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantExists(grass.Id))
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
            return View(grass);
        }

        // GET: Grasses/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grass = await _context.Grass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grass == null)
            {
                return NotFound();
            }

            return View(grass);
        }

        // POST: Grasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grass = await _context.Grass.FindAsync(id);
            _context.Grass.Remove(grass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ShowSearchResult(string SearchPhrase)
        {
            return View("Index", await _context.Grass.Where(g =>
                g.Name.Contains(SearchPhrase)).ToListAsync());
        }
    }
}
