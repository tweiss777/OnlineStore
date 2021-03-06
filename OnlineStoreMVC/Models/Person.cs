﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreMVC.Models
{
    public class Person
    {
        public int UserID { get; set; }

        [Required] 
        public string Password { get; set; }

        [Required,Display(Name ="First Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$"), StringLength(50)]
        public string Firstname { get; set; }

        [Required, Display(Name ="Last Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]*$"), StringLength(50)]
        public string Lastname { get; set; }

        [Required]
        [Display(Name="Address 1")]
        public string Addr1 { get; set; }

        [Display(Name="Address 2")]
        public string Addr2 { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
