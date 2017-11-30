using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IsraelBlock.Contexts;
using IsraelBlock.Models;

namespace WebTeste.Controllers
{
    public class SalesController : Controller
    {
        private readonly EFContextDbContext _context = new EFContextDbContext();

        public ActionResult Index()
        {
            return View(_context.Sales.OrderBy(c => c.NameClient));
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sale = _context.Sales.Find(id.Value);
            if (sale == null)
            {
                return HttpNotFound();
            }
            
            return View(sale);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleId,NrNota,DateSale,NameClient,CpfClient,Phone,TotalSale,Closed")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                sale.DateSale = DateTime.Now;
                sale.Closed = "N";
                _context.Sales.Add(sale);
                _context.SaveChanges();
                return RedirectToAction("Edit", new { id = sale.SaleId });
            }

            return View(sale);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = _context.Sales.Include(s => s.Items).FirstOrDefault(s => s.SaleId == id.Value);

            if (sale == null)
            {
                return HttpNotFound();
            }

            if ("S".Equals(sale.Closed))
            {
                TempData["Message"] = "Sale " + sale.SaleId + " já foi encerrada";

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ProductId = new SelectList(_context.Products.Where(p => p.Active == "S"), "ProductId", "Name");
                ViewBag.SaleId = new SelectList(_context.Sales, "SaleId", "NrNota");
                return View(sale);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleId,NrNota,DateSale,NameClient,CpfClient,Phone,TotalSale,Closed")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(sale).State = EntityState.Modified;
                _context.SaveChanges();
                if ("S".Equals(sale.Closed))
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Edit", new { id = sale.SaleId });
            }
            ViewBag.ProductId = new SelectList(_context.Products.Where(p => p.Active == "S"), "ProductId", "Name");
            ViewBag.SaleId = new SelectList(_context.Sales, "SaleId", "NrNota");
            return View(sale);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = _context.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }

            if (("S").Equals(sale.Closed))
            {
                TempData["Message"] = "Sale " + sale.SaleId + " já foi encerrada";

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ProductId = new SelectList(_context.Products.Where(p => p.Active == "S"), "ProductId", "Name");
                ViewBag.SaleId = new SelectList(_context.Sales, "SaleId", "NrNota");
                return View(sale);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Sale sale = _context.Sales.Find(id);
            _context.Items
                .Where(i => i.SaleId == id)
                .ToList()
                .ForEach(i => _context.Items.Remove(i));
            _context.Sales.Remove(sale);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}