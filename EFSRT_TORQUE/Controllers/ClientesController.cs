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
    public class ClientesController : Controller
    {
        //para listar los clientes
        IEnumerable<Clientes> clientes()
        {
            List<Clientes> cliTemporal = new List<Clientes>();
            SqlConnection
            conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString
                );
            conn.Open();

            //definimos un comando: SELECT * FROM Clientes , este listara todos los elementos de la bd
            string query = "SELECT * FROM Clientes";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // Clientes() => sale de el nombre definido en el enumerable IEnumerable<Clientes>
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
        // GET: Clientes
        //txt saludos


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

        public ActionResult ListarClientes()
        //es como decir un index de la pagina con la lista de cleintes
        {
            return View(clientes());
        }

        public ActionResult CreateCliente()
        {
            return View(new Clientes());
        }

        [HttpPost]
        public ActionResult Create(Clientes reg)
        {
            // recibe los datos en reg, ejecuto y almaceno en un ViewBag
            ViewBag.mensaje = AgregarCliente(reg);
            //refrescar la vista
            return View(reg);
        }





    }
}
