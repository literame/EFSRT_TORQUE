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
    public class ProductosController : Controller
    {
        //para listar los productos
        private IEnumerable<Productos> productos()
        {
            List<Productos> prodTemporal = new List<Productos>();
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString
            );
            conn.Open();

            string query = "SELECT * FROM Productos";
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                prodTemporal.Add(new Productos
                {
                    ProductoID = rdr.GetInt32(0),
                    Descripcion = rdr.GetString(1),
                    Precio = rdr.GetDecimal(2),
                    Stock = rdr.GetInt32(3),
                    ProveedorID = rdr.GetInt32(4)
                });
            }

            rdr.Close();
            conn.Close();
            return prodTemporal;
        }

        // GET: Productos
        public ActionResult ListarProductos()
        {
            return View(productos());
        }

        string AgregarProducto(Productos reg)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_agregarProducto", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@productoId", reg.ProductoID);
                    cmd.Parameters.AddWithValue("@descripcion", reg.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", reg.Precio);
                    cmd.Parameters.AddWithValue("@stock", reg.Stock);
                    cmd.Parameters.AddWithValue("@proveedorId", reg.ProveedorID);

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


        string EliminarProducto(string ProductoID)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_eliminarProducto", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@productoId", ProductoID);
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


        string ActualizarProducto(Productos reg)
        {
            string mensaje = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("usp_actualizarProductos", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@productoId", reg.ProductoID);
                    cmd.Parameters.AddWithValue("@descripcion", reg.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", reg.Precio);
                    cmd.Parameters.AddWithValue("@stock", reg.Stock);
                    cmd.Parameters.AddWithValue("@proveedorId", reg.ProveedorID);

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



        // Acción para crear un nuevo Productos (formulario)
        public ActionResult CreateProductos()
        {
            return View(new Productos());
        }

        // Acción para crear un nuevo Productos (post)
        public ActionResult Create(Productos reg)
        {
            if (ModelState.IsValid)
            {
                ViewBag.mensaje = AgregarProducto(reg);
                return RedirectToAction("ListarProductos");
            }
            return View("CreateProducto", reg);
        }

        // Acción para eliminar un Productos
        public ActionResult DeleteProducto(string id)
        {
            ViewBag.mensaje = EliminarProducto(id);
            return View("DeleteProducto");
        }

        // Acción para editar un Productos (formulario)
        public ActionResult EditProducto(int id)
        {
            Productos producto = productos().FirstOrDefault(c => c.ProductoID == id);
            return View(producto);
        }

        // Acción para editar un Productos (post)
        [HttpPost]
        public ActionResult Edit(Productos reg)
        {
            ViewBag.mensaje = ActualizarProducto(reg);
            return RedirectToAction("ListarProductos");
        }

        // Acción para ver los detalles de un Productos
        public ActionResult DetailsProducto(int id)
        {
            Productos producto = productos().FirstOrDefault(c => c.ProductoID == id);
            return View(producto);
        }
    }




}
