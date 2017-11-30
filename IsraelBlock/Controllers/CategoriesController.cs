using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IsraelBlock.Contexts;
using IsraelBlock.Models;

namespace IsraelBlock.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly EFContextDbContext _context = new EFContextDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            return View(_context.Categories.OrderBy(c => c.Name));
        }

        #region Create

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion Create

        #region Details

        public ActionResult Details(long? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var category = _context
                .Categories
                .Where(c => c.CategoryId == id)
                .Include("Products.Supplier")
                .First();

            if (category == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(category);
        }

        #endregion Details

        #region Edit

        public ActionResult Edit(long? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var category = _context.Categories.Find(id.Value);

            if (category == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category modified)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(modified).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(modified);
        }

        #endregion Edit

        #region Delete

        public ActionResult Delete(long? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var category = _context.Categories.Find(id.Value);

            if (category == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            var category = _context.Categories.Find(id);
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        #endregion Delete

        [HttpPost]
        public JsonResult GetDados()
        {
            var resultado = new
            {
                Sup = _context.Categories.OrderBy(c => c.Name)
            };
            return Json(resultado);
        }
    }
}