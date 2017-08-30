using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsraelBlock.Contexts;
using System.Data.Entity;
using IsraelBlock.Models;
using System.Net;

namespace IsraelBlock.Controllers
{
    public class ProductsController : Controller
    {
        private readonly EFContextDbContext _context = new EFContextDbContext();
        // GET: Products
        public ActionResult Index()
        {
            var products = _context
                .Products
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .OrderBy(n => n.Name);
            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(long? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = _context
                .Products
                .Where(p => p.ProductId == id)
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .First();

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = 
                new SelectList(
                    _context.
                    Categories.
                    OrderBy(c => c.Name), 
                        "CategoryId", "Name"
                    );
            ViewBag.SupplierId = 
                new SelectList(
                    _context
                    .Suppliers
                    .OrderBy(s => s.Name), 
                        "SupplierId", "Name"
                    );
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product Product)
        {
            try
            {
                _context.Products.Add(Product);
                _context.SaveChanges();
                TempData["Message"] = "Novo produto registrado com sucesso!";
                return RedirectToAction("Index");
            }
            catch
            {
                return View(Product);
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(long? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = _context.Products.Find(id.Value);

            if (product == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            ViewBag.CategoryId = new SelectList(_context.Categories.OrderBy(c => c.Name), "CategoryId", "Name", product.CategoryId);
            ViewBag.SupplierId = new SelectList(_context.Suppliers.OrderBy(s => s.Name), "SupplierId", "Name", product.SupplierId);

            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Entry(product).State = EntityState.Modified;
                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(long? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var product = _context
                .Products
                .Where(p => p.ProductId == id)
                .Include(c => c.Category)
                .Include(s => s.Supplier)
                .First();

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                var product = _context.Products.Find(id);
                _context.Products.Remove(product);
                _context.SaveChanges();

                TempData["Message"] = "Produto " + product.Name.ToUpper() + " removido com sucesso";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult GetDados()
        {
            var resultado = new
            {
                Sup = _context.Products.OrderBy(p => p.Name)
            };
            return Json(resultado);
        }
    }
}
