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

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        public ActionResult Create(Productos producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Guardar en la base de datos
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString))
                    {
                        conn.Open();

                        string query = "INSERT INTO Productos (ProductoID, Descripcion, Precio, Stock, ProveedorID) VALUES (@Nombre, @Descripcion, @Precio, @Stock, @ProveedorID)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ProductoID", producto.ProductoID);
                        cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                        cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                        cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                        cmd.Parameters.AddWithValue("@ProveedorID", producto.ProveedorID);
                        cmd.ExecuteNonQuery();
                    }

                    // Guardar en la lista de productos (si estás usando una lista en memoria)
                    List<Productos> listaProductos = ObtenerProductosDesdeAlgunaFuente(); // Obtén la lista de productos de alguna fuente (base de datos, archivo, etc.)
                    listaProductos.Add(producto); // Agrega el nuevo producto a la lista
                    GuardarProductosEnAlgunaFuente(listaProductos); // Guarda la lista actualizada de productos en alguna fuente

                    return RedirectToAction("ListarProductos");
                }
                return View(producto);
            }
            catch
            {
                return View();
            }
        }

        private List<Productos> ObtenerProductosDesdeAlgunaFuente()
        {
            // Lógica para obtener la lista de productos desde alguna fuente (base de datos, archivo, etc.)
            return new List<Productos>(); // Ejemplo de una lista vacía, ajusta según tu lógica real
        }

        private void GuardarProductosEnAlgunaFuente(List<Productos> listaProductos)
        {
            // Lógica para guardar la lista de productos en alguna fuente (base de datos, archivo, etc.)
            // Aquí puedes implementar la lógica real para guardar en la base de datos o en otro medio
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int id)
        {
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString
            );
            conn.Open();

            string query = "SELECT * FROM Productos WHERE ProductoID = @ProductoID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ProductoID", id);
            SqlDataReader rdr = cmd.ExecuteReader();

            Productos producto = null;
            if (rdr.Read())
            {
                producto = new Productos
                {
                    ProductoID = rdr.GetInt32(0),
                    Descripcion = rdr.GetString(1),
                    Precio = rdr.GetDecimal(2),
                    Stock = rdr.GetInt32(3),
                    ProveedorID = rdr.GetInt32(4)
                };
            }

            rdr.Close();
            conn.Close();
            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Productos producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SqlConnection conn = new SqlConnection(
                        ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString
                    );
                    conn.Open();

                    string query = "UPDATE Productos SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Stock = @Stock, ProveedorID = @ProveedorID WHERE ProductoID = @ProductoID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                    cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                    cmd.Parameters.AddWithValue("@ProveedorID", producto.ProveedorID);
                    cmd.Parameters.AddWithValue("@ProductoID", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    return RedirectToAction("ListarProductos");
                }
                return View(producto);
            }
            catch
            {
                return View();
            }
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id)
        {
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString
            );
            conn.Open();

            string query = "SELECT * FROM Productos WHERE ProductoID = @ProductoID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ProductoID", id);
            SqlDataReader rdr = cmd.ExecuteReader();

            Productos producto = null;
            if (rdr.Read())
            {
                producto = new Productos
                {
                    ProductoID = rdr.GetInt32(0),
                    Descripcion = rdr.GetString(1),
                    Precio = rdr.GetDecimal(2),
                    Stock = rdr.GetInt32(3),
                    ProveedorID = rdr.GetInt32(4)
                };
            }

            rdr.Close();
            conn.Close();
            return View(producto);
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int id)
        {
            SqlConnection conn = new SqlConnection(
                ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString
            );
            conn.Open();

            string query = "SELECT * FROM Productos WHERE ProductoID = @ProductoID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ProductoID", id);
            SqlDataReader rdr = cmd.ExecuteReader();

            Productos producto = null;
            if (rdr.Read())
            {
                producto = new Productos
                {
                    ProductoID = rdr.GetInt32(0),
                    Descripcion = rdr.GetString(1),
                    Precio = rdr.GetDecimal(2),
                    Stock = rdr.GetInt32(3),
                    ProveedorID = rdr.GetInt32(4)
                };
            }

            rdr.Close();
            conn.Close();
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                SqlConnection conn = new SqlConnection(
                    ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString
                );
                conn.Open();

                string query = "DELETE FROM Productos WHERE ProductoID = @ProductoID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductoID", id);
                cmd.ExecuteNonQuery();
                conn.Close();

                return RedirectToAction("ListarProductos");
            }
            catch
            {
                return View();
            }
        }
    }
}
