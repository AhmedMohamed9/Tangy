using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models.ViewModels
{
    public class SubcategoryViewModel
    {
        public SubCategory subCategory { get; set; }

        public IEnumerable<category> categorieslist { get; set; }

        public List<string> subcategory { get; set; }

        [Display(Name ="New Sub Category")]
        public bool isnew { get; set; }

        public string StatusMessage { get; set; }
    }
}
