using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsraelBlock.Contexts;

namespace IsraelBlock.Controllers
{
    public class MakersController : Controller
    {
        private EFContextDbContext context = new EFContextDbContext();
        // GET: Fabricantes
        public ActionResult Index()
        {
            return View(context.Makers.OrderBy(
            c => c.Name));
        }
    }
}