using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models
{
    public class SubCategory
    {
        [Required]
        public int id { get; set; }
        [Required]
        [Display(Name ="Sub Category")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int Categoryid { get; set; }

        [ForeignKey("Categoryid")]
        public virtual category category { get; set; }
    }
}
