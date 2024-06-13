using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EFSRT_TORQUE.Models
{
    public class DetallesCompra
    {
        [Display(Name = "ID_DETALLE")] public int DetalleID { get; set; }
        [Display(Name = "ID_COMPRA")] public int CompraID { get; set; }
        [Display(Name = "ID_PRODUCTO")] public int ProductoID { get; set; }
        [Display(Name = "CANTIDAD")] public int Cantidad { get; set; }
        [Display(Name = "PRECIO_COMPRA")] public decimal PrecioCompra { get; set; }

    }
}