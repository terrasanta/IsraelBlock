﻿using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IsraelBlock.Contexts;
using IsraelBlock.Models;

namespace IsraelBlock.Controllers
{
    public class SuppliersController : Controller
    {
        private readonly EFContextDbContext _context = new EFContextDbContext();

        // GET: Fabricantes
        public ActionResult Index()
        {
            return View(_context.Suppliers.OrderBy(s => s.Name));
        }

        #region Create

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        #endregion Create

        #region Edit

        public ActionResult Edit(long? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var supplier = _context.Suppliers.Find(id.Value);

            if (supplier == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(supplier).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        #endregion Edit

        #region Delete

        public ActionResult Delete(long? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var supplier = _context.Suppliers.Find(id.Value);

            if (supplier == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(supplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                var s = _context.Suppliers.Find(supplier.SupplierId);

                if (supplier == null)
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);

                _context.Suppliers.Remove(s);

                _context.SaveChanges();

                TempData["Message"] = "Fornecedor " + supplier.Name.ToUpper() + " foi removido";

                return RedirectToAction("index");
            }
            return View();
        }

        /**
         public ActionResult Delete(long id)
        {
            var supplier =
                _context
                .Suppliers
                .Find(id);

            _context
                .Suppliers
                .Remove(supplier);

            _context
                .SaveChanges();

            TempData["Message"] = "Fornecedor " + supplier.Name.ToUpper() + " foi removido";

            return RedirectToAction("index");
        }
             */

        #endregion Delete

        #region Details

        public ActionResult Details(long? id)
        {
            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var supplier = _context.Suppliers.Find(id.Value);

            if (supplier == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(supplier);
        }

        #endregion Details

        [HttpPost]
        public JsonResult GetDados()
        {
            var resultado = new
            {
                Sup = _context.Suppliers.OrderBy(s => s.Name)
            };
            return Json(resultado);
        }
    }
}