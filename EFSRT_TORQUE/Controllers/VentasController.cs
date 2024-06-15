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
                ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString
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
                    Fecha = rdr.GetDateTime(1),
                    ClienteID = rdr.GetString(2),
                    Total = rdr.GetDecimal(3),
                });
            }

            rdr.Close();
            conn.Close();
            return ventasTemporal;
        }

        // GET: Ventas
        //txt saludos
        public ActionResult ListarVentas()
        {
            return View(ventas());
        }



        string AgregarProveedores(Ventas reg)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_agregarVentas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ventaId", reg.VentaID);
                    cmd.Parameters.AddWithValue("@fecha", reg.Fecha);
                    cmd.Parameters.AddWithValue("@clienteId", reg.ClienteID);
                    cmd.Parameters.AddWithValue("@total", reg.Total);

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

        public ActionResult CreateVentas()
        {
            return View(new Ventas());
        }

        [HttpPost]
        public ActionResult CreateVentas(Ventas reg)
        {
            // recibe los datos en reg, ejecuto y almaceno en un ViewBag
            ViewBag.mensaje = AgregarProveedores(reg);
            //refrescar la vista
            return View(reg);
        }



    }
}