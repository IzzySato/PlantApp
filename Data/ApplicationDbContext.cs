using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlantApp.Models;

namespace PlantApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Flower> Flower { get; set; }
        public DbSet<Tree> Tree { get; set; }
        public DbSet<Grass> Grass { get; set; }
    }
}