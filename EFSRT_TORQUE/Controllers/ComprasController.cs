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
        IEnumerable<Compras> Compra()
        {
            List<Compras> compTemporal = new List<Compras>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                string notificacion = "";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_listarComprar", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        compTemporal.Add(new Compras()
                        {
                            CompraID = rdr.GetInt32(0),
                            Fecha = rdr.GetDateTime(1),
                            ProveedorID = rdr.GetInt32(2),
                            Total = rdr.GetDecimal(3),
                        });
                    }
                }
                catch (SqlException ex)
                {
                    notificacion = ex.Message;
                }
                finally
                {

                    conn.Close();
                }
                return compTemporal;
            } 
        }



        // GET: Compras
        //txt saludos
        public ActionResult ListarCompras()
        {
            return View(Compra());
        }


        // Método para agregar un producto
        string AgregarCompra(Compras reg)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_agregarCompra", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@compraId", reg.CompraID);
                    cmd.Parameters.AddWithValue("@fecha", reg.Fecha);
                    cmd.Parameters.AddWithValue("@proveedorId", reg.ProveedorID);
                    cmd.Parameters.AddWithValue("@total", reg.Total);

                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha agregado {i} compra(s)";
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


        public ActionResult CreateCompra()
        {
            return View(new Compras());
        }

        // Acción para crear un nueva compra (post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Compras reg)
        {
            if (ModelState.IsValid)
            {
                ViewBag.mensaje = AgregarCompra(reg);
                return RedirectToAction("ListarCompras");
            }

            return View("CreateCompra", reg);
        }




        string EliminarCompra(int CompraId)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_eliminarCompra", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@compraId", CompraId);
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha eliminado {i} socio(s)";
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


        // Método para actualizar una compra
        string ActualizarCompras(Compras reg)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_actualizarCompras", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@compraId", reg.CompraID);
                    cmd.Parameters.AddWithValue("@fecha", reg.Fecha);
                    cmd.Parameters.AddWithValue("@proveedorId", reg.ProveedorID);
                    cmd.Parameters.AddWithValue("@total", reg.Total);

                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {i} compra(s)";
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
        public ActionResult EditCompra(Int32 id)
        {
            Compras compra = Compra().FirstOrDefault(c => c.CompraID == id);
            return View(compra);
        }

        // Acción para editar una Compra (post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Compras reg)
        {
            if (ModelState.IsValid)
            {
                ViewBag.mensaje = ActualizarCompras(reg);
                return RedirectToAction("ListarCompras");
            }
            return View("EditCompra", reg);
        }



        // Acción para eliminar un Compra
        public ActionResult DeleteCompra(int id)
        {
            ViewBag.mensaje = EliminarCompra(id);
            return View("DeleteCompra");
        }

        // Acción para ver los detalles de un Compras
        public ActionResult DetailsCompra(int id)
        {
            Compras compra = Compra().FirstOrDefault(c => c.CompraID == id);
            return View(compra);
        }


    }

}

