using System;
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
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
