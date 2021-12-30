using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PlantApp.ViewModels
{
    public class PlantViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please description")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please height in inch")]
        [Display(Name = "Height (feet)")]
        public int HeightFeet { get; set; }

        [Required(ErrorMessage = "Please annual or perennial")]
        [Display(Name = "Annual or Perennial")]
        public bool IsAnnual { get; set; }

        [Required(ErrorMessage = "Please choose image")]
        [Display(Name = "plant Picture")]
        public IFormFile PlantImage { get; set; }
    }
}
