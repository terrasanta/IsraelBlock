using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IsraelBlock.Contexts;
using IsraelBlock.Models;

namespace IsraelBlock.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly EFContextDbContext _context = new EFContextDbContext();

        /*private static IList<Category> categoryList = new List<Category>()
        {
            new Category(){ CategoryId = 1, Name = "Keyboard"},
            new Category(){ CategoryId = 2, Name = "Monitor"},
            new Category(){ CategoryId = 3, Name = "Notebook"},
            new Category(){ CategoryId = 4, Name = "Mouse"},
            new Category(){ CategoryId = 5, Name = "Printer"}
        };*/

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
        #endregion

        #region Details
        public ActionResult Details(long? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var category = _context.Categories.Find(id.Value);

            if (category == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(category);
        }

        #endregion

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

        #endregion

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


        #endregion

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