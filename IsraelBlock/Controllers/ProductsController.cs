using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IsraelBlock.Contexts;
using IsraelBlock.Models;

namespace WebTeste.Controllers
{
    public class ProductsController : Controller
    {
        private readonly EFContextDbContext _context = new EFContextDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var Products = _context.Products.Include(c => c.Category).Include(f => f.Supplier).OrderBy(n => n.Name);
            return View(Products.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories.OrderBy(b => b.Name), "CategoryId", "Name");
            ViewBag.SupplierId = new SelectList(_context.Suppliers.OrderBy(b => b.Name), "SupplierId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Active,CategoryId,SupplierId")] Product produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    produto.Active = "S";
                    _context.Products.Add(produto);
                    _context.SaveChanges();

                    TempData["Message"] = "Product " + produto.Name.ToUpper() + " foi adicionado";

                    return RedirectToAction("Index");
                }
                _context.Products.Add(produto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(produto);
            }
        }

        public ActionResult Details(long? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var produto = _context.Products.Find(id.Value);

            if (produto == null)
                return new HttpStatusCodeResult(
                    HttpStatusCode.NotFound);

            ViewBag.CategoryId = new SelectList(_context.Categories.OrderBy(b => b.Name), "CategoryId", "Name", produto.CategoryId);
            ViewBag.SupplierId = new SelectList(_context.Suppliers.OrderBy(b => b.Name), "SupplierId", "Name", produto.SupplierId);
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Details(Product produto)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product produto = _context.Products.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryId = new SelectList(_context.Categories.OrderBy(b => b.Name), "CategoryId", "Name", produto.CategoryId);
            ViewBag.SupplierId = new SelectList(_context.Suppliers.OrderBy(b => b.Name), "SupplierId", "Name", produto.SupplierId);
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Active,CategoryId,SupplierId")] Product produto)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();

                TempData["Message"] = "Product " + produto.Name.ToUpper() + " foi alterado";
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "Name", produto.CategoryId);
            ViewBag.SupplierId = new SelectList(_context.Suppliers, "SupplierId", "Name", produto.SupplierId);
            return View(produto);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.
                BadRequest);
            }
            Product produto = _context.Products.Where(p => p.ProductId ==
            id).Include(c => c.Category).Include(f => f.Supplier).
            First();
            if (produto == null)
            {
                return HttpNotFound();
            }

            if (produto.Active.Equals("S"))
            {
                return View(produto);
            }
            else
            {
                TempData["Message"] = "Product " + produto.Name.ToUpper() + " já está desativado";
                return RedirectToAction("Index");
            }
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(long id)
        {
            try
            {
                Product produto = _context.Products.Find(id);
                produto.Active = "N";

                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();

                TempData["Message"] = "Product " + produto.Name.ToUpper() + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}