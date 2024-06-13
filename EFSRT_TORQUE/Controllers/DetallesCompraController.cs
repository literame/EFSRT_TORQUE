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
            List<DetallesCompra> prodTemporal = new List<DetallesCompra>();
            SqlConnection conn = null;
            conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cadena"].ConnectionString
                );
            conn.Open();

            //definimos un comando: SELECT * FROM Detalles de Compra , este listara todos los elementos de la bd
            string query = "SELECT * FROM Detalles de Compra";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // DetallesCompra() => sale de el nombre definido en el enumerable IEnumerable<DetallesCompra>
                prodTemporal.Add(new DetallesCompra()
                {
                    DetalleID = rdr.GetInt32(0),
                    CompraID = rdr.GetInt32(0),
                    ProductoID = rdr.GetInt32(0),
                    Cantidad = rdr.GetInt32(0),
                    PrecioCompra = rdr.GetDecimal(1),
                });
            }
            rdr.Close();
            conn.Close();
            return prodTemporal;
        }
        // GET: Detalles de Compra
        //txt saludos
        public ActionResult Index()
        {
            return View();
        }
    }
}

