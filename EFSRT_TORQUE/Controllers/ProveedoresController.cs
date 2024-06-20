using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EFSRT_TORQUE.Models;
using System.Collections;

namespace EFSRT_TORQUE.Controllers
{
    public class ProveedoresController : Controller
    {
        //para listar los Proveedores
        IEnumerable<Proveedores> Proveedor()
        {
            List<Proveedores> provTemporal = new List<Proveedores>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                string notificacion = "";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_listarProveedores", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        // Proveedores() => sale de el nombre definido en el enumerable IEnumerable<Proveedores>
                        provTemporal.Add(new Proveedores()
                        {
                            ProveedorID = rdr.GetInt32(0),
                            Nombre = rdr.GetString(1),
                            Telefono = rdr.GetString(2),
                            Email = rdr.GetString(3),
                            Direccion = rdr.GetString(4),
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
                return provTemporal;
            }
        }

        // GET: Proveedores
        //txt saludos
        public ActionResult ListarProveedores()
        {
            return View(Proveedor());
        }

        // Método para agregar un cliente
        string AgregarProveedor(Proveedores reg)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_agregarProveedor", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@proveedorId", reg.ProveedorID);
                    cmd.Parameters.AddWithValue("@nombre", reg.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", reg.Telefono);
                    cmd.Parameters.AddWithValue("@email", reg.Email);
                    cmd.Parameters.AddWithValue("@direccion", reg.Direccion);


                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha insertado {i} proveedor(es)";
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

        // Acción para crear un nuevo cliente (formulario)
        public ActionResult CreateProveedor()
        {
            return View(new Proveedores());
        }

        // Acción para crear un nuevo cliente (post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Proveedores reg)
        {
            if (ModelState.IsValid)
            {
                ViewBag.mensaje = AgregarProveedor(reg);
                return RedirectToAction("ListarProveedores");
            }

            return View("CreateProveedor", reg);
        }

        string EliminarProveedor(int ProveedorID)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_eliminarProveedor", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@proveedorId", ProveedorID);
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

        
        // Método para actualizar un proveedor
        string ActualizarProveedor(Proveedores reg)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_actualizarProveedores", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@proveedorId", reg.ProveedorID);
                    cmd.Parameters.AddWithValue("@nombre", reg.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", reg.Telefono);
                    cmd.Parameters.AddWithValue("@email", reg.Email);
                    cmd.Parameters.AddWithValue("@direccion", reg.Direccion);

                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {i} proveedor(es)";
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

        // Acción para editar un proveedor (formulario)
        public ActionResult EditProveedor(Int32 id)
        {
            Proveedores proveedor = Proveedor().FirstOrDefault(c => c.ProveedorID == id);
            return View(proveedor);
        }

        // Acción para editar un proveedor (post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Proveedores reg)
        {
            if (ModelState.IsValid)
            {
                ViewBag.mensaje = ActualizarProveedor(reg);
                return RedirectToAction("ListarProveedores");
            }
            return View("EditProveedor", reg);
        }


        public ActionResult DeleteProveedor(int id)
        {
            ViewBag.mensaje = EliminarProveedor(id);
            return View("DeleteProveedor");
        }

        // Acción para ver los detalles de un Proveedores
        public ActionResult DetailsProveedor(int id)
        {
            Proveedores proveedores = Proveedor().FirstOrDefault(c => c.ProveedorID == id);
            return View(proveedores);
        }




    }
}
