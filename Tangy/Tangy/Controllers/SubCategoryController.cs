using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tangy.Data;
namespace Tangy.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext db;

        public SubCategoryController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index()
        {
            var subcategory = db.subCategories.Include(x => x.category);
            return View(await subcategory.ToListAsync());
        }
    }
}