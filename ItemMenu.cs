using System;
using System.Collections.Generic;
using System.Text;

namespace Resto_Gest
{
    public class ItemMenu
    {
        // Cambiamos 'private' por 'public' para que Program.cs pueda leerlas
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Categoria { get; set; } // Ejemplo: Plato o Bebida

        public ItemMenu(int id, string nombre, double precio, string categoria)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Categoria = categoria;
        }
    }
}