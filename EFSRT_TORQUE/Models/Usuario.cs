using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EFSRT_TORQUE.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }

        public string NombreUsuario { get; set; }

        public string Contrasena { get; set; }

        public string Nombre { get; set; }

        public string Rol { get; set; }
    }

    public class LoginViewModel
    {
        public string NombreUsuario { get; set; }

        public string Contrasena { get; set; }

        public bool RememberMe { get; set; }
    }
    }