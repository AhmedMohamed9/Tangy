using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tangy.Models.ViewModels
{
    public class Menuitem_ViewModel
    {
        public Menuitem menuitem { get; set; }

        public List<category> categorylist { get; set; }
        public List<SubCategory>  Subcategorylist { get; set; }

    }
}
