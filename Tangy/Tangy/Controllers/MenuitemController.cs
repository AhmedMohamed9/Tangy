using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Tangy.Data;
namespace Tangy.Controllers
{
    public class MenuitemController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment hostingEnvironment;

        public MenuitemController(ApplicationDbContext db,IHostingEnvironment hostingEnvironment)
        {
            this.db = db;
            this.hostingEnvironment = hostingEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }
    
    
    }
}