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
            List<Clientes> prodTemporal = new List<Clientes>();
            SqlConnection conn = null;
            conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["cadena"].ConnectionString
                );
            conn.Open();

            //definimos un comando: SELECT * FROM Clientes , este listara todos los elementos de la bd
            string query = "SELECT * FROM Clientes";
            SqlCommand cmd = new SqlCommand(query, conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                // Clientes() => sale de el nombre definido en el enumerable IEnumerable<Clientes>
                prodTemporal.Add(new Clientes()
                {
                    ClienteID = rdr.GetInt32(0),
                    Nombre = rdr.GetString(1),
                    Telefono = rdr.GetString(1),
                    Email = rdr.GetString(1),
                    Direccion = rdr.GetString(1),
                });
}
            rdr.Close();
            conn.Close();
            return prodTemporal;
        }
        // GET: Clientes
        //txt saludos
        public ActionResult Index()
        {   return View();
       }
    }
}
