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
    public class HomeController : Controller
    {

        public decimal ConsultarTotalCompras()
        {
            decimal total = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("ObtenerMontoTotalCompras", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read()) 
                    {
                        total = rdr.GetDecimal(0); // Leer el valor como decimal
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            // Imprimir el total de compras en la consola
            Console.WriteLine("Monto total de compras: " + total);
            return total;
        }

        private decimal ConsultarTotalVentas()
        {
            decimal total = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("ObtenerMontoTotalVentas", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        total = rdr.GetDecimal(0);
                        Console.WriteLine("Monto total de ventas leído: " + total);
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron resultados.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return total;
        }

        public ActionResult Index()
        {
            // Recuperar los datos del usuario desde TempData
            string nombreUsuario = TempData["NombreUsuario"] as string;
            string rolUsuario = TempData["RolUsuario"] as string;

            decimal totalCompras = ConsultarTotalCompras();
            decimal totalVentas = ConsultarTotalVentas();

            decimal diferencia = totalVentas - totalCompras;
            decimal totalsoles = diferencia * 3.80M;

            // Pasar los datos a la vista
            ViewBag.NombreUsuario = nombreUsuario;
            ViewBag.RolUsuario = rolUsuario;
            ViewBag.TotalCompras = totalCompras;
            ViewBag.TotalVentas = totalVentas;
            ViewBag.Diferencia = diferencia;
            ViewBag.TotalSoles = totalsoles;

            return View();
        }


        public ActionResult About()
        {
           

            return View();
        }

        public ActionResult Contact()
        {
        

            return View();
        }
    }
}