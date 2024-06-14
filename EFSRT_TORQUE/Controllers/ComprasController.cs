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
    public class ComprasController : Controller
    {
        //para listar los compras
        IEnumerable<Compras> compras()
        {
            List<Compras> compTemporal = new List<Compras>();
            SqlConnection conn = null;
            conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cadena"].ConnectionString
                );
            conn.Open();

            //definimos un comando: SELECT * FROM Compras , este listara todos los elementos de la bd
            string query = "SELECT * FROM Compras";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // Compras() => sale de el nombre definido en el enumerable IEnumerable<Compras>
                compTemporal.Add(new Compras()
                {
                    CompraID = rdr.GetInt32(0),
                    Fecha = rdr.GetDateTime(1),
                    ProveedorID = rdr.GetInt32(2),
                    Total = rdr.GetDecimal(3),
                });
            }
            rdr.Close();
            conn.Close();
            return compTemporal;
        }
        // GET: Compras
        //txt saludos
        public ActionResult ListarCompras()
        {
            return View(compras());
        }
    }
}

