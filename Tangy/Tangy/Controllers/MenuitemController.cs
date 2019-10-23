using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Tangy.Data;
using Tangy.Models.ViewModels;
using Tangy.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Tangy.Controllers
{
    public class MenuitemController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment hostingEnvironment;

        [BindProperty]
        public Menuitem_ViewModel MenuItemVM { get; set; }
        

        public MenuitemController(ApplicationDbContext db,IHostingEnvironment hostingEnvironment)
        {
            this.db = db;
            this.hostingEnvironment = hostingEnvironment;
            this.MenuItemVM = new Menuitem_ViewModel()
            {
                menuitem=new Menuitem(),
                categorylist=db.categories.ToList(),

            };
        }


        public async Task<IActionResult> Index()
        {
            var menuitems =await db.menuitems.Include(m => m.Category).Include(x => x.subcategory).ToListAsync();
            return View(menuitems);
        }


        public IActionResult create()
        {
            return View(MenuItemVM);

        }

        [HttpPost,ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Createpost()
        {
            MenuItemVM.menuitem.subcategoryid = Convert.ToInt32(Request.Form["subcategoryid"].ToString());
            MenuItemVM.menuitem.categoryid = Convert.ToInt32(Request.Form["categoryid"].ToString());

            if (!ModelState.IsValid)
            {
                return View(MenuItemVM);
            }
            MenuItemVM.menuitem.image = UploadFile(Request.Form.Files[0]);
            db.menuitems.Add(MenuItemVM.menuitem);
            await db.SaveChangesAsync();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuItemVM.menuitem =await db.menuitems.Include(m => m.Category).Include(x => x.subcategory).SingleOrDefaultAsync(s => s.id == id);
            if (MenuItemVM.menuitem==null)
            {
                return NotFound();
            }    
            MenuItemVM.Subcategorylist = db.subCategories.Where(x => x.Categoryid == MenuItemVM.menuitem.categoryid).ToList();

            return View(MenuItemVM);
        }

        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editpost()
        {
            if (ModelState.IsValid)
            {
                MenuItemVM.menuitem.subcategoryid = Convert.ToInt32(Request.Form["subcategoryid"].ToString());
                MenuItemVM.menuitem.categoryid = Convert.ToInt32(Request.Form["categoryid"].ToString());

                MenuItemVM.menuitem.image = UploadFile(MenuItemVM.File, MenuItemVM.menuitem.image);
                db.Update(MenuItemVM.menuitem);
                await db.SaveChangesAsync();
                return RedirectToAction("index");
            }
            return View(MenuItemVM);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuItemVM.menuitem = await db.menuitems.Include(m => m.Category).Include(x => x.subcategory).SingleOrDefaultAsync(s => s.id == id);
           
            if (MenuItemVM.menuitem == null)
            {
                return NotFound();
            }

            return View(MenuItemVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MenuItemVM.menuitem = await db.menuitems.Include(m => m.Category).Include(x => x.subcategory).SingleOrDefaultAsync(s => s.id == id);
            if (MenuItemVM.menuitem == null)
            {
                return NotFound();
            }
            MenuItemVM.Subcategorylist = db.subCategories.Where(x => x.Categoryid == MenuItemVM.menuitem.categoryid).ToList();

            return View(MenuItemVM);
        }
        public async Task<IActionResult> deletepost(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            var men =await db.menuitems.SingleOrDefaultAsync(x => x.id == id);

            db.menuitems.Remove(men);
            await db.SaveChangesAsync();
            return RedirectToAction("index");
        }
        public JsonResult getsubcategory(int categoryid)
        {
            List<SubCategory> subcat = new List<SubCategory>();
            subcat = (from subCategories in db.subCategories
                      where subCategories.Categoryid == categoryid
                      select subCategories).ToList();
            return Json(new SelectList(subcat,"id", "Name"));
        }

        string UploadFile(IFormFile file)
        {
            //at Create
            if (file != null)
            {
                string uploads = Path.Combine(hostingEnvironment.WebRootPath, "images");

                string newPath = Path.Combine(uploads, file.FileName);
                 
             
                file.CopyTo(new FileStream(newPath, FileMode.Create));
                

                return file.FileName;
            }

            return null;
        }


        string UploadFile(IFormFile file, string imageUrl)
        {
            //at Edit
            if (file != null)
            {
                string uploads = Path.Combine(hostingEnvironment.WebRootPath, "images");

                string newPath = Path.Combine(uploads, file.FileName);
                string oldPath = Path.Combine(uploads, imageUrl);

                if (oldPath != newPath)
                {
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }

                return file.FileName;
            }

            return imageUrl;
        }


    }
}