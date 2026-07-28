using System;
using System.Collections.Generic;
using System.Text;

namespace Resto_Gest
{
    public class Pedido
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public List<ItemMenu> Platos { get; set; } = new List<ItemMenu>();
        public string Estado { get; set; } // "Pendiente", "En Preparacion", "Entregado"
        public string Mesero { get; set; }
        public Pedido(int id, int numeroMesa, string mesero)
        {
            Id = id;
            NumeroMesa = numeroMesa;
            Mesero = mesero;
            Estado = "Pendiente";
        }
    }
}