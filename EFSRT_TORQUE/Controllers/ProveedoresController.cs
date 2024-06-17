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
    public class ProveedoresController : Controller
    {
        //para listar los Proveedores
        IEnumerable<Proveedores> proveedor()
        {
            List<Proveedores> provTemporal = new List<Proveedores>();
            SqlConnection conn = null;
            conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString
                );
            conn.Open();

            //definimos un comando: SELECT * FROM Proveedores , este listara todos los elementos de la bd
            string query = "SELECT * FROM Proveedores";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // Proveedores() => sale de el nombre definido en el enumerable IEnumerable<Proveedores>
                provTemporal.Add(new Proveedores()
                {
                    ProveedorID = rdr.GetInt32(0),
                    Nombre = rdr.GetString(1),
                    Email = rdr.GetString(2),
                    Telefono = rdr.GetString(3),
                    Direccion = rdr.GetString(4),
                });
            }

            rdr.Close();
            conn.Close();
            return provTemporal;
        }

        // GET: Proveedores
        //txt saludos
        public ActionResult ListarProveedores()
        {
            return View(proveedor());
        }

        string AgregarProveedores(Proveedores reg)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_agregarCompra", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@proveedorId", reg.ProveedorID);
                    cmd.Parameters.AddWithValue("@nombre", reg.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", reg.Telefono);
                    cmd.Parameters.AddWithValue("@email", reg.Email);
                    cmd.Parameters.AddWithValue("@direccion", reg.Direccion);


                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha insertado {i} socios";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message; //si hay error muestra mensaje del error 
                }
                finally
                {
                    conn.Close();
                }
                return mensaje;
            }
        }

        public ActionResult CreateProveedores()
        {
            return View(new Proveedores());
        }

        [HttpPost]
        public ActionResult CreateProveedores(Proveedores reg)
        {
            // recibe los datos en reg, ejecuto y almaceno en un ViewBag
            ViewBag.mensaje = AgregarProveedores(reg);
            //refrescar la vista
            return View(reg);
        }











    }
}
