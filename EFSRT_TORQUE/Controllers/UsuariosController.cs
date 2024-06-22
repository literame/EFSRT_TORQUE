using System;
using System.Collections.Generic;
using EFSRT_TORQUE.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;


namespace EFSRT_TORQUE.Controllers
{
    public class UsuariosController : Controller
    {
        private SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString);

        // Acción para mostrar el formulario de registro
        public ActionResult Registro()
        {
            return View();
        }

        // Acción para manejar el registro de un usuario POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (NombreUsuario, Contrasena, Nombre, Rol) VALUES (@NombreUsuario, @Contrasena, @Nombre, @Rol)", conn))
                {
                    cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                    cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena); // Nota: En un proyecto real, deberías hash la contraseña
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@Rol", usuario.Rol);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    return RedirectToAction("Login");
                }
            }

            return View(usuario);
        }

        // Acción para mostrar el formulario de inicio de sesión
        public ActionResult Login()
        {
            return View();
        }

        // Acción para manejar el inicio de sesión del usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            string usuario = "";
            // Simulación de autenticación
            string query = "SELECT NombreUsuario FROM Usuarios WHERE NombreUsuario = @NombreUsuario AND Contrasena = @Contrasena";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NombreUsuario", model.NombreUsuario);
                cmd.Parameters.AddWithValue("@Contrasena", model.Contrasena); // Recuerda, en un proyecto real, la contraseña debería estar encriptada

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // El usuario ha sido autenticado correctamente
                    FormsAuthentication.SetAuthCookie(model.NombreUsuario, model.RememberMe);
                    conn.Close();
                    usuario = model.NombreUsuario.ToString(); 
                    ViewBag.Usuario = usuario;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Las credenciales son incorrectas, mostrar mensaje de error
                    ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                    conn.Close();
                    return View(model);
                }
            }
        }

            // Acción para manejar el cierre de sesión del usuario
            public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
