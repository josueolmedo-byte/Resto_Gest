using System;
using System.Collections.Generic;
using System.Text;

namespace Resto_Gest
{
    public class Mesa
    {
        private int Numero { get; set; }
        private string Estado { get; set; } // "Libre", "Ocupada", "Solicitando Cuenta"

        public Mesa(int numero)
        {
            Numero = numero;
            Estado = "Libre";
        }
    }
}
