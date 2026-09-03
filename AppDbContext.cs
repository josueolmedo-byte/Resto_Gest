using Microsoft.EntityFrameworkCore;

namespace Resto_Gest
{
    public class AppDbContext : DbContext
    {
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<ItemMenu> ItemsMenu { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RestoGestDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}