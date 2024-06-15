using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EFSRT_TORQUE.Models
{
    public class Compras
    {
        [Display(Name = "ID_COMPRA")] public int CompraID { get; set; }
        [Display(Name = "FECHA")] public DateTime Fecha { get; set; }
        [Display(Name = "ID_RUC")] public int ProveedorID { get; set; }
        [Display(Name = "TOTAL")] public decimal Total { get; set; }
 
    }
}

