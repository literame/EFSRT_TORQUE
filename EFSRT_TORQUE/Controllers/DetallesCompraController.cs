using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EFSRT_TORQUE.Models;

namespace EFSRT_TORQUE.Controllers
{
    public class DetallesCompraController : Controller
    {
        // GET: DetallesCompra
        public ActionResult Index()
        {
            return View();
        }

        // GET: DetallesCompra/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DetallesCompra/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DetallesCompra/Create
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

        // GET: DetallesCompra/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DetallesCompra/Edit/5
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

        // GET: DetallesCompra/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DetallesCompra/Delete/5
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
