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
    public class CommonController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IWebHostEnvironment webHostEnvironment;

        public CommonController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        // GET: Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        protected string UploadedFile(PlantViewModel model)
        {
            string uniqueFileName = null;

            if (model.PlantImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PlantImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.PlantImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        protected bool PlantExists(int id)
        {
            return _context.Flower.Any(e => e.Id == id);
        }
    }
}
