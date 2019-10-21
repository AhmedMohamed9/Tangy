using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tangy.Data;
using Tangy.Models.ViewModels;
namespace Tangy.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext db;
        [TempData]
        public string statusMessage { get; set; }

        public SubCategoryController(ApplicationDbContext db)
        {
            this.db = db;
        }



        public async Task<IActionResult> Index()
        {
            var subcategory = db.subCategories.Include(x => x.category);
            return View(await subcategory.ToListAsync());
        }




        //Get Action for Create
        public IActionResult create()
        {
            SubcategoryViewModel model = new SubcategoryViewModel()
            {
                subCategory = new Models.SubCategory(),
                categorieslist  = db.categories.ToList(),
                subcategorylist = db.subCategories.OrderBy(p=>p.Name).Select(x => x.Name).Distinct().ToList()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create(SubcategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doessubcatisexist = db.subCategories.Where(x => x.Name == model.subCategory.Name).Count();
                var doessubcatandcatexist = db.subCategories.Where(x => x.Name == model.subCategory.Name && x.Categoryid == model.subCategory.Categoryid).Count();
                
                if(doessubcatisexist >0 && model.isnew)
                {
                    //error
                    statusMessage = "error : Subcategory Already exist";
                }
                else
                {
                    if (doessubcatisexist == 0 && !model.isnew)
                    {
                        //error
                        statusMessage = "error : Subcategory doesn't exist";

                    }
                    else
                    {
                        if(doessubcatandcatexist > 0)
                        {
                            //error
                            statusMessage = "error : Subcategory and category Already exist";

                        }
                        else
                        {
                            db.Add(model.subCategory);
                            await db.SaveChangesAsync();
                            return RedirectToAction("index");
                        }
                    }
                }
            }
            SubcategoryViewModel modelvm = new SubcategoryViewModel()
            {
                subCategory = new Models.SubCategory(),
                categorieslist = db.categories.ToList(),
                subcategorylist = db.subCategories.OrderBy(p => p.Name).Select(x => x.Name).ToList(),
                StatusMessage = statusMessage
        };
            return View(modelvm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var osubcategory =await db.subCategories.SingleOrDefaultAsync(x => x.id == id);
            if (osubcategory == null)
            {
                return NotFound();
            }
            SubcategoryViewModel model = new SubcategoryViewModel()
            {
                categorieslist = db.categories.ToList(),
                subCategory=osubcategory,
                subcategorylist=db.subCategories.Select(x=>x.Name).Distinct().ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubcategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doessubcatisexist = db.subCategories.Where(x => x.Name == model.subCategory.Name).Count();
                var doessubcatandcatexist = db.subCategories.Where(x => x.Name == model.subCategory.Name && x.Categoryid == model.subCategory.Categoryid).Count();

                if (doessubcatisexist == 0)
                {
                    statusMessage = "Error : sub Category Doesn't exist.you cann't add a new subcategory here";
                }
                else
                {
                    if (doessubcatandcatexist > 0)
                    {
                        statusMessage = "Error : sub Category and category already here";
                    }
                    else
                    {
                        var subcat = db.subCategories.Find(id);
                        subcat.Categoryid = model.subCategory.Categoryid;
                        subcat.Name = model.subCategory.Name;
                        await db.SaveChangesAsync();
                        return RedirectToAction("index");

                    }
                }
            }
                SubcategoryViewModel modelvm = new SubcategoryViewModel()
                {
                    subCategory = new Models.SubCategory(),
                    categorieslist = db.categories.ToList(),
                    subcategorylist = db.subCategories.OrderBy(p => p.Name).Select(x => x.Name).ToList(),
                    StatusMessage = statusMessage
                };
                return View(modelvm);
            
        }


        public async Task<IActionResult> details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subcat = await db.subCategories.Include(x => x.category).SingleOrDefaultAsync(x => x.id == id);
            if (subcat ==null)
            {
                return NotFound();
            }
            return View(subcat);
        }
        public async Task<IActionResult> delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subcat = await db.subCategories.Include(x => x.category).SingleOrDefaultAsync(x => x.id == id);
            if (subcat == null)
            {
                return NotFound();
            }
            return View(subcat);
        }
        [HttpPost,ActionName("delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> confirmdelte(int id)
        {
            var subcat =await db.subCategories.SingleOrDefaultAsync(m => m.id == id);
            db.Remove(subcat);
            await db.SaveChangesAsync();
            return RedirectToAction("index");
        }

    }
}