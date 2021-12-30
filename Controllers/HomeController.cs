using Microsoft.AspNetCore.Mvc;
using PlantApp.Models;
using PlantApp.Data;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PlantApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private MergeModel model = new MergeModel();

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            model.Flowers = await _context.Flower.ToListAsync();
            model.Grasses = await _context.Grass.ToListAsync();
            model.Trees = await _context.Tree.ToListAsync();

            return View(model);
        }


        public async Task<IActionResult> ShowSearchResult(string SearchPhrase)
        {
            MergeModel filteredModel = new MergeModel();
            filteredModel.Flowers = await _context.Flower.Where(f =>
                f.Name.Contains(SearchPhrase)).ToListAsync();
            filteredModel.Grasses = await _context.Grass.Where(g =>
                g.Name.Contains(SearchPhrase)).ToListAsync();
            filteredModel.Trees = await _context.Tree.Where(t =>
                t.Name.Contains(SearchPhrase)).ToListAsync();

            return View("Index", filteredModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}