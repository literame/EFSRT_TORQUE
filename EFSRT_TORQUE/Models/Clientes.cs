using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EFSRT_TORQUE.Models
{
    public class Clientes
    {
        [Display(Name = "ID_RUC")] public int ClienteID { get; set; }
        [Display(Name = "NOMBRE")] public string Nombre { get; set; }
        [Display(Name = "TELEFONO")] public string Telefono { get; set; }
        [Display(Name = "EMAIL")] public string Email { get; set; }
        [Display(Name = "DIRECCION")] public string Direccion { get; set; }
    }
}