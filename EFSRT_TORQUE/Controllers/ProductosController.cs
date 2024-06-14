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
    public class ProductosController : Controller
    {
        //para listar los productos
        IEnumerable<Productos> productos()
        {
            List<Productos> prodTemporal = new List<Productos>();
            SqlConnection conn = null;
            conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cadena"].ConnectionString
                );
            conn.Open();

            //definimos un comando: SELECT * FROM Productos , este listara todos los elementos de la bd
            string query = "SELECT * FROM Productos";
            SqlCommand cmd = new SqlCommand(query, conn);
            
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // Productos() => sale de el nombre definido en el enumerable IEnumerable<Productos>
                prodTemporal.Add(new Productos()
                {
                    ProductoID = rdr.GetInt32(0),
                    Nombre = rdr.GetString(1),
                    Descripcion = rdr.GetString(2),
                    Precio = rdr.GetDecimal(3),
                    Stock = rdr.GetInt32(4),
                    ProveedorID = rdr.GetInt32(5),
                });

            }



            rdr.Close();
            conn.Close();
            return prodTemporal;
        }





        // GET: Productos
        //txt saludos
        public ActionResult ListarProductos()
        {
            return View(productos());
        }




    }
}