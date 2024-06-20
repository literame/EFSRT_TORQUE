using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EFSRT_TORQUE.Models
{
    public class Ventas
    {
        [Display(Name = "NRO. VENTA")] public int VentaID { get; set; }
        [Display(Name = "FECHA DE EMISIÓN")] public DateTime Fecha { get; set; }
        [Display(Name = "NRO. CLIENTE")] public string ClienteID { get; set; }
        [Display(Name = "TOTAL")] public decimal Total { get; set; }
    }
}