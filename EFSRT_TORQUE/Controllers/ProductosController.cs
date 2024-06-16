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
        //para listar los productos
        private IEnumerable<Productos> productos()
        {
            List<Productos> prodTemporal = new List<Productos>();
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString
            );
            conn.Open();

            string query = "SELECT * FROM Productos";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                prodTemporal.Add(new Productos
                {
                    ProductoID = rdr.GetInt32(0),
                    Descripcion = rdr.GetString(1),
                    Precio = rdr.GetDecimal(2),
                    Stock = rdr.GetInt32(3),
                    ProveedorID = rdr.GetInt32(4)
                });
            }

            rdr.Close();
            conn.Close();
            return prodTemporal;
        }

        // GET: Productos
        public ActionResult ListarProductos()
        {
            return View(productos());
        }

}
