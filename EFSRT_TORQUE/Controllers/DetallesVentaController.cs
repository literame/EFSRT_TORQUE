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
    public class DetallesVentaController : Controller
    {
        //para listar los Detalles de Venta
        IEnumerable<DetallesVenta> detallesventa()
        {
            List<DetallesVenta> dventaTemporal = new List<DetallesVenta>();
            SqlConnection conn = null;
            conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString
                );
            conn.Open();

            //definimos un comando: SELECT * FROM Detalles de Venta , este listara todos los elementos de la bd
            string query = "SELECT * FROM DetallesVenta";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // DetallesVenta() => sale de el nombre definido en el enumerable IEnumerable<DetallesVenta>
                dventaTemporal.Add(new DetallesVenta()
                {
                    DetalleID = rdr.GetInt32(0),
                    VentaID = rdr.GetInt32(1),
                    ProductoID = rdr.GetInt32(2),
                    Cantidad = rdr.GetInt32(3),
                    PrecioVenta = rdr.GetDecimal(4),
                });
            }
            rdr.Close();
            conn.Close();
            return dventaTemporal;
        }
        // GET: Detalles de Venta
        //txt saludos
        public ActionResult Index()
        {
            return View(detallesventa());
        }
    }
}
