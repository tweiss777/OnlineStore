using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreMVC.Models
{
    public class Car
    {


        //required fields
        public int Vin { get; set; }
        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }

        //non required fields
        public string Trimtype { get; set; }
        public string Color { get; set; }

        [Required]
        public double Msrp { get; set; }

        [Required,Display(Name="Image")]
        public string imgURL {get; set; } //holds the image url
    }
}
