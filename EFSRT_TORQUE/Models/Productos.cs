using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EFSRT_TORQUE.Models
{
    public class Productos
    {
        [Display(Name = "NRO. PRODUCTO")] public int ProductoID { get; set; }
        [Display(Name = "NOMBRE")] public string Descripcion { get; set; }
        [Display(Name = "PRECIO")] public decimal Precio { get; set; }
        [Display(Name = "STOCK")] public int Stock { get; set; }
        [Display(Name = "NRO. PROVEEDOR")] public int ProveedorID { get; set; }
    }
}