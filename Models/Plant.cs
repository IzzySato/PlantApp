using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantApp.Models
{
    public class Plant
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        [Display(Name = "Name")]
        [StringLength(100)]
        public String Name { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        [Display(Name = "Description")]
        [StringLength(500)]
        public String Description { get; set; }


        [Required(ErrorMessage = "Please choose image")]
        public string Image { get; set; }

        public Plant()
        {

        }
    }
}
