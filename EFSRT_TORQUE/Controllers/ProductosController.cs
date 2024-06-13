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
    public class ProductosController : Controller
    {
        // GET: Productos
        //txt saludos
        public ActionResult Index()
        {
            return View();
        }
    }
}