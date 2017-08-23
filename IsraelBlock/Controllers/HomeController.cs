using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsraelBlock.Contexts;
using IsraelBlock.Models;

namespace IsraelBlock.Controllers
{
    public class HomeController : Controller
    {
        private readonly EFContextDbContext _context = new EFContextDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}