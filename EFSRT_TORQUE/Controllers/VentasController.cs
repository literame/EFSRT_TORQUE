using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EFSRT_TORQUE.Models;
using System.Drawing;

namespace EFSRT_TORQUE.Controllers
{
    public class VentasController : Controller
    {
        //para listar los Ventas
        IEnumerable<Ventas> ventas()
        {
            List<Ventas> ventasTemporal = new List<Ventas>();
            SqlConnection conn = null;
            conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cadena"].ConnectionString
                );
            conn.Open();

            //definimos un comando: SELECT * FROM Ventas , este listara todos los elementos de la bd
            string query = "SELECT * FROM Ventas";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // Ventas() => sale de el nombre definido en el enumerable IEnumerable<Ventas>
                ventasTemporal.Add(new Ventas()
                {
                    VentaID = rdr.GetInt32(0),
                    Fecha = rdr.GetDateTime(0),
                    ClienteID = rdr.GetInt32(0),
                    Total = rdr.GetDecimal(1),
                });
            }

            rdr.Close();
            conn.Close();
            return ventasTemporal;
        }

        // GET: Ventas
        //txt saludos
        public ActionResult Index()
        {
            return View(ventas());
        }

    }
}