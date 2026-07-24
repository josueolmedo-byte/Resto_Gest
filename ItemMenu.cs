using System;
using System.Collections.Generic;
using System.Text;

namespace Resto_Gest
{
    public class ItemMenu
    {
        private int Id { get; set; }
        private string Nombre { get; set; }
        private double Precio { get; set; }
        private string Categoria { get; set; } // Ejemplo: Plato o Bebida

        public ItemMenu(int id, string nombre, double precio, string categoria)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Categoria = categoria;
        }
    }
}
