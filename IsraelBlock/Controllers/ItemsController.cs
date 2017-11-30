using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using IsraelBlock.Contexts;
using IsraelBlock.Models;


namespace WebTeste.Controllers
{
    public class ItemsController : Controller
    {
        private readonly EFContextDbContext _context = new EFContextDbContext();

        public ActionResult Index()
        {
            var Items = _context.Items.Include(s => s.Product).Include(s => s.Sale);
            return View(Items.ToList());
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _context.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            ViewBag.ProductId = new SelectList(_context.Products.Where(p => p.Active == "S"), "ProductId", "Nome", item.ProductId);
            ViewBag.SaleId = new SelectList(_context.Sales, "SaleId", "SaleId", item.SaleId);
            return View(item);
        }

        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_context.Products.Where(p => p.Active == "S"), "ProductId", "Name");
            ViewBag.SaleId = new SelectList(_context.Sales, "SaleId", "SaleId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,Amount,UnitaryValue,ProductId,SaleId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(item);
                var Sale = _context.Sales.Find(item.SaleId);
                Sale.TotalSale += item.Amount * item.UnitaryValue;
                _context.Entry(Sale).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Edit", "Sales", new { id = item.SaleId });
            }

            return RedirectToAction("Edit", "Sales", new { id = item.SaleId });
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _context.Items.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            Sale Sale = _context.Sales.Find(item.SaleId);
            if (Sale == null)
            {
                return HttpNotFound();
            }

            if (("S").Equals(Sale.Closed))
            {
                TempData["Message"] = "Sale " + item.Sale.SaleId + " já foi encerrada";

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ProductId = new SelectList(_context.Products.Where(p => p.Active == "S"), "ProductId", "Name", item.ProductId);
                ViewBag.SaleId = new SelectList(_context.Sales, "SaleId", "SaleId", item.SaleId);
                return View(item);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,Amount,UnitaryValue,ProductId,SaleId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();

                Sale Sale = _context.Sales.Include(s => s.Items).FirstOrDefault(s => s.SaleId == item.SaleId);
                Sale.TotalSale = Sale.Items.Sum(s => s.Amount * s.UnitaryValue);

                _context.Entry(Sale).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(_context.Products.Where(p => p.Active == "S"), "ProductId", "Name", item.ProductId);
            ViewBag.SaleId = new SelectList(_context.Sales, "SaleId", "SaleId", item.SaleId);
            return View(item);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _context.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            Sale Sale = _context.Sales.Find(item.SaleId);
            if (Sale == null)
            {
                return HttpNotFound();
            }

            if (("S").Equals(Sale.Closed))
            {
                TempData["Message"] = "Sale " + item.Sale.SaleId + " já foi encerrada";

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ProductId = new SelectList(_context.Products.Where(p => p.Active == "S"), "ProductId", "Name", item.ProductId);
                ViewBag.SaleId = new SelectList(_context.Sales, "SaleId", "SaleId", item.SaleId);
                return View(item);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "SaleId,ItemId,ProductId,Amount,UnitaryValue,")] Item item)
        {
            if (ModelState.IsValid)
            {
                long? idSale = item.SaleId;

                _context.Entry(item).State = EntityState.Deleted;
                _context.SaveChanges();

                Sale Sale = _context.Sales.Include(s => s.Items).FirstOrDefault(s => s.SaleId == idSale);
                Sale.TotalSale = Sale.Items.Sum(s => s.Amount * s.UnitaryValue);

                _context.Entry(Sale).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(_context.Products.Where(p => p.Active == "S"), "ProductId", "Name", item.ProductId);
            ViewBag.SaleId = new SelectList(_context.Sales, "SaleId", "SaleId", item.SaleId);
            return View(item);
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