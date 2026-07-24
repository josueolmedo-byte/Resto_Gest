using System;
using System.Collections.Generic;
using System.Text;

namespace Resto_Gest
{
    public class Pedido
    {
        private int Id { get; set; }
        private int NumeroMesa { get; set; }
        private List<ItemMenu> Platos { get; set; } = new List<ItemMenu>();
        private string Estado { get; set; } // "Pendiente", "En Preparacion", "Entregado"
        private string Mesero { get; set; }

        public Pedido(int id, int numeroMesa, string mesero)
        {
            Id = id;
            NumeroMesa = numeroMesa;
            Mesero = mesero;
            Estado = "Pendiente";
        }
    }
}