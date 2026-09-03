using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Resto_Gest
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
