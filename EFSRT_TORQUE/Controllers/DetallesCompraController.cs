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
        //para listar los Detalles de compra
        IEnumerable<DetallesCompra> detallescompra()
        {
            List<DetallesCompra> dcompTemporal = new List<DetallesCompra>();
            SqlConnection conn = null;
            conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString
                );
            conn.Open();

            //definimos un comando: SELECT * FROM Detalles de Compra , este listara todos los elementos de la bd
            string query = "SELECT * FROM DetallesCompra";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // DetallesCompra() => sale de el nombre definido en el enumerable IEnumerable<DetallesCompra>
                dcompTemporal.Add(new DetallesCompra()
                {
                    DetalleID = rdr.GetInt32(0),
                    CompraID = rdr.GetInt32(1),
                    ProductoID = rdr.GetInt32(2),
                    Cantidad = rdr.GetInt32(3),
                    PrecioCompra = rdr.GetDecimal(4),
                });
            }
            rdr.Close();
            conn.Close();
            return dcompTemporal;
        }
        // GET: Detalles de Compra
        //txt saludos
        public ActionResult Index()
        {
            return View(detallescompra());
        }
    }
}

