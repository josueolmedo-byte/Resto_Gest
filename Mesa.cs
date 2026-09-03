using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resto_Gest
{
    public class Mesa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Impide que SQL Server intente generar el ID automáticamente
        public int Numero { get; set; }

        public string Estado { get; set; }

        public Mesa()
        {
            Estado = "Libre";
        }

        public Mesa(int numero)
        {
            Numero = numero;
            Estado = "Libre";
        }
    }
}