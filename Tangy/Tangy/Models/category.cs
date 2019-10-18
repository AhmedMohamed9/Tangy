using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models
{
    public class category
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="Name")]

        public string name { get; set; }
        [Required]
        [Display(Name = "Display Name")]
        public int DisplayName { get; set; }
    }
}
