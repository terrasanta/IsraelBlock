using System.Linq;
using System.Web.Mvc;
using IsraelBlock.Contexts;

namespace IsraelBlock.Controllers
{
    public class HomeController : Controller
    {
        private readonly EFContextDbContext _context = new EFContextDbContext();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Categorias = _context.Categories.OrderBy(c => c.Name).ToArray();
            ViewBag.Fornecedores = _context.Suppliers.OrderBy(c => c.Name).ToArray();
            ViewBag.Produtos = _context.Products.OrderBy(n => n.Name).ToArray();

            return View();

            //return View();
        }

        public JsonResult Data()
        {
            var ret = new
            {
                Categorias = _context.Categories.OrderBy(c => c.Name).ToArray(),
                Fornecedores = _context.Suppliers.OrderBy(c => c.Name).ToArray(),
                Produtos = _context.Products.OrderBy(n => n.Name).ToArray()
            };
            return Json(ret);
        }
    }
}