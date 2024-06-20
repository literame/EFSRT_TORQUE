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
        IEnumerable<Ventas> Venta()
        {
            List<Ventas> ventasTemporal = new List<Ventas>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                string notificacion = "";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_listarVentas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

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
                }
                catch (SqlException ex)
                {
                    notificacion = ex.Message;
                }
                finally
                {

                    conn.Close();
                }
                return ventasTemporal;
            }
        }

        // GET: Ventas
        //txt saludos
        public ActionResult ListarVentas()
        {
            return View(Venta());
        }



        // Método para agregar una venta
        string AgregarVenta(Ventas reg)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_agregarVenta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ventaId", reg.VentaID);
                    cmd.Parameters.AddWithValue("@fecha", reg.Fecha);
                    cmd.Parameters.AddWithValue("@clienteId", reg.ClienteID);
                    cmd.Parameters.AddWithValue("@total", reg.Total);

                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha agregado {i} venta(s)";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
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

        // Acción para crear un nueva venta (post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ventas reg)
        {
            if (ModelState.IsValid)
            {
                ViewBag.mensaje = AgregarVenta(reg);
                return RedirectToAction("ListarVentas");
            }

            return View("CreateVentas", reg);
        }


        // Método para eliminar una venta
        string EliminarVenta(int ventaId)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_eliminarVenta", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ventaId", ventaId);

                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} venta(s)";
                }
                catch (SqlException ex)
                {
                    mensaje = $"Error: {ex.Message}";
                    // Puedes manejar el error aquí o registrar en un sistema de logging
                }
                finally
                {
                    conn.Close();
                }
            }
            return mensaje;
        }

        // Acción para eliminar una venta

        public ActionResult DeleteVenta(int id)
        {
            Ventas venta = Venta().FirstOrDefault(c => c.VentaID == id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // Acción para eliminar una venta (POST)
        [HttpPost, ActionName("DeleteVenta")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            string mensaje = EliminarVenta(id);
            ViewBag.mensaje = mensaje;
            return RedirectToAction("ListarVentas");
        }

        string ActualizarVentas(Ventas reg)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_actualizarVentas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ventaId", reg.VentaID);
                    cmd.Parameters.AddWithValue("@fecha", reg.Fecha);
                    cmd.Parameters.AddWithValue("@clienteId", reg.ClienteID);
                    cmd.Parameters.AddWithValue("@total", reg.Total);

                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {i} venta(s)";
                }
                catch (SqlException ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
            return mensaje;
        }

        // Acción para editar una compra(formulario)
        public ActionResult EditVenta(Int32 id)
        {
            Ventas venta = Venta().FirstOrDefault(c => c.VentaID == id);
            return View(venta);
        }

        // Acción para editar una Compra (post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ventas reg)
        {
            if (ModelState.IsValid)
            {
                ViewBag.mensaje = ActualizarVentas(reg);
                return RedirectToAction("ListarVentas");
            }
            return View("EditVenta", reg);
        }


        // Acción para ver los detalles de un Ventas
        public ActionResult DetailsVenta(int id)
        {
            Ventas venta = Venta().FirstOrDefault(c => c.VentaID == id);
            return View(venta);
        }






















    }
}