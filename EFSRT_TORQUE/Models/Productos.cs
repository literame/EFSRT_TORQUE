using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EFSRT_TORQUE.Models
{
    public class Productos
    {
        [Display(Name = "ID_PRODUCTO")] public int ProductoID { get; set; }
        [Display(Name = "NOMBRE")] public string Nombre { get; set; }
        [Display(Name = "DESCRIPCION")] public string Descripcion { get; set; }
        [Display(Name = "PRECIO")] public decimal Precio { get; set; }
        [Display(Name = "STOCK")] public int Stock { get; set; }
        [Display(Name = "ID_RUC")] public int ProveedorID { get; set; }
    }
}