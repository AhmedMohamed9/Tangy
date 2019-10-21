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

        //public int categoryid { get; set; }

        public List<category> categorieslist { get; set; }

        public List<string> subcategorylist { get; set; }

        [Display(Name ="New Sub Category")]
        public bool isnew { get; set; }

        public string StatusMessage { get; set; }
    }
}
