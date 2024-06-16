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
    public class ClientesController : Controller
    {
        // Método para listar los clientes
        IEnumerable<Clientes> Cliente()
        {
            List<Clientes> cliTemporal = new List<Clientes>();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString);
            conn.Open();

            string query = "SELECT * FROM Clientes";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                cliTemporal.Add(new Clientes()
                {
                    ClienteID = rdr.GetString(0),
                    Nombre = rdr.GetString(1),
                    Telefono = rdr.GetString(2),
                    Email = rdr.GetString(3),
                    Direccion = rdr.GetString(4),
                });
            }
            rdr.Close();
            conn.Close();
            return cliTemporal;
        }

        // Método para agregar un cliente
        string AgregarCliente(Clientes reg)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_agregarCliente", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@clienteId", reg.ClienteID);
                    cmd.Parameters.AddWithValue("@nombre", reg.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", reg.Telefono);
                    cmd.Parameters.AddWithValue("@email", reg.Email);
                    cmd.Parameters.AddWithValue("@direccion", reg.Direccion);

                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha insertado {i} socio(s)";
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

        // Método para eliminar un cliente
        string EliminarCliente(string clienteId)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_eliminarCliente", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@clienteId", clienteId);
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

        // Método para actualizar un cliente
        string ActualizarCliente(Clientes reg)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_actualizarCliente", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@clienteId", reg.ClienteID);
                    cmd.Parameters.AddWithValue("@nombre", reg.Nombre);
                    cmd.Parameters.AddWithValue("@telefono", reg.Telefono);
                    cmd.Parameters.AddWithValue("@email", reg.Email);
                    cmd.Parameters.AddWithValue("@direccion", reg.Direccion);

                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha actualizado {i} socio(s)";
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

        // Acción para listar los clientes
        public ActionResult ListarClientes()
        {
            return View(Cliente());
        }

        // Acción para crear un nuevo cliente (formulario)
        public ActionResult CreateCliente()
        {
            return View(new Clientes());
        }

        // Acción para crear un nuevo cliente (post)
        public ActionResult Create(Clientes reg)
        {
            if (ModelState.IsValid)
            {
                ViewBag.mensaje = AgregarCliente(reg);
                return RedirectToAction("ListarClientes");
            }
            return View("CreateCliente", reg);
        }

        // Acción para eliminar un cliente
        public ActionResult DeleteCliente(string id)
        {
            ViewBag.mensaje = EliminarCliente(id);
            return View("DeleteCliente");
        }

        // Acción para editar un cliente (formulario)
        public ActionResult EditCliente(string id)
        {
            Clientes cliente = Cliente().FirstOrDefault(c => c.ClienteID == id);
            return View(cliente);
        }

        // Acción para editar un cliente (post)
        [HttpPost]
        public ActionResult Edit(Clientes reg)
        {
            ViewBag.mensaje = ActualizarCliente(reg);
            return RedirectToAction("ListarClientes");
        }

        // Acción para ver los detalles de un cliente
        public ActionResult DetailsCliente(string id)
        {
            Clientes cliente = Cliente().FirstOrDefault(c => c.ClienteID == id);
            return View(cliente);
        }
    }
}
