﻿using System.Web.Mvc;

namespace IsraelBlock.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }

        // GET: Base/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Base/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Base/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Base/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Base/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Base/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Base/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}