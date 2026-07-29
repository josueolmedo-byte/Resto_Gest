using System;
using System.Collections.Generic;

namespace Resto_Gest
{
    public class Program
    {
        // Listas globales que usará todo el sistema para guardar los datos
        public static List<ItemMenu> MenuPlatos = new List<ItemMenu>();
        public static List<Mesa> Mesas = new List<Mesa>();
        public static List<Pedido> Pedidos = new List<Pedido>();

        // Variable global para las ventas 
        public static double TotalVentasDelDia = 0;

        static void Main(string[] args)
        {
            // Creamos 5 mesas iniciales automáticamente
            for (int i = 1; i <= 5; i++)
            {
                Mesas.Add(new Mesa(i));
            }

            int opcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("    RESTOGEST - SISTEMA DE PEDIDOS     ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Gestionar Menú (Platos y Bebidas)");
                Console.WriteLine("2. Ver Estado de Mesas");
                Console.WriteLine("3. Aperturar Mesa y Registrar Pedido");
                Console.WriteLine("4. Ver Cola de Pedidos en Cocina");
                Console.WriteLine("5. Generar Cuenta y Procesar Pago");
                Console.WriteLine("6. Ver Reporte de Ventas del Turno");
                Console.WriteLine("7. Salir");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            GestionarMenu();
                            break;
                        case 2:
                            Console.WriteLine("\n[En desarrollo por Integrante 2]");
                            break;
                        case 3:
                            Console.WriteLine("\n[En desarrollo por Integrante 2]");
                            break;
                        case 4:
                            VerColaCocina();
                            break;
                        case 5:
                            ProcesarPagoMesa();
                            break;
                        case 6:
                            VerReporteVentas();
                            break;
                        case 7:
                            Console.WriteLine("\nSaliendo del sistema...");
                            break;
                        default:
                            Console.WriteLine("\nOpción no válida.");
                            break;
                    }
                }
                if (opcion != 7)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            } while (opcion != 7);
        }

        public static void VerColaCocina()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("       COLA DE PEDIDOS EN COCINA        ");
            Console.WriteLine("========================================");
            if (Pedidos.Count == 0)
            {
                Console.WriteLine("No hay pedidos registrados en cocina.");
                return;
            }

            foreach (var ped in Pedidos)
            {
                Console.WriteLine($"\nPedido #{ped.Id} | Mesa #{ped.NumeroMesa} | Mesero: {ped.Mesero} | Estado: {ped.Estado}");
                Console.WriteLine("Platos/Bebidas a preparar:");
                foreach (var plato in ped.Platos)
                {
                    Console.WriteLine($"  - {plato.Nombre} ({plato.Categoria})");
                }
            }
        }

        public static void ProcesarPagoMesa()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("     GENERAR CUENTA Y PROCESAR PAGO     ");
            Console.WriteLine("========================================");
            Console.Write("Ingrese el número de mesa a cobrar: ");

            if (int.TryParse(Console.ReadLine(), out int numMesa))
            {
                // Se agrega ?? "" para evitar advertencias de posible valor nulo
                Pedido? pedidoMesa = Pedidos.Find(p => p.NumeroMesa == numMesa);

                if (pedidoMesa != null)
                {
                    double subtotal = 0;
                    Console.WriteLine($"\n--- DETALLE DE CONSUMO (MESA {numMesa}) ---");
                    foreach (var item in pedidoMesa.Platos)
                    {
                        Console.WriteLine($"- {item.Nombre}: ${item.Precio}");
                        subtotal += item.Precio;
                    }

                    double iva = subtotal * 0.15; // 15% IVA
                    double total = subtotal + iva;

                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine($"Subtotal:   ${subtotal:F2}");
                    Console.WriteLine($"IVA (15%):  ${iva:F2}");
                    Console.WriteLine($"TOTAL:      ${total:F2}");
                    Console.WriteLine("----------------------------------------");

                    Console.Write("¿Confirmar pago y cerrar mesa? (S/N): ");
                    string op = Console.ReadLine() ?? "";
                    if (op.ToUpper() == "S")
                    {
                        TotalVentasDelDia += total; // Acumular venta

                        // Cambiar estado de la mesa a Libre
                        Mesa? mesa = Mesas.Find(m => m.Numero == numMesa);
                        if (mesa != null) mesa.Estado = "Libre";

                        // Eliminar el pedido de la lista activa
                        Pedidos.Remove(pedidoMesa);

                        // =========================================================
                        //  PERSISTENCIA EN ARCHIVO JSON (Guardar registro de venta)
                        // =========================================================
                        List<Venta> historialVentas = ArchivoJson.Cargar<Venta>("ventas.json");

                        Venta nuevaVenta = new Venta
                        {
                            Id = historialVentas.Count + 1,
                            NumeroMesa = numMesa,
                            Total = (decimal)total,
                            Fecha = DateTime.Now
                        };

                        historialVentas.Add(nuevaVenta);
                        ArchivoJson.Guardar("ventas.json", historialVentas);
                        // =========================================================

                        Console.WriteLine("\n¡Pago procesado con éxito y mesa liberada!");
                        Console.WriteLine($"[JSON] Venta guardada permanentemente en ventas.json (ID Venta: #{nuevaVenta.Id})");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo se encontró ningún pedido activo para esa mesa.");
                }
            }
        }

        public static void VerReporteVentas()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("     REPORTE DE VENTAS DEL TURNO        ");
            Console.WriteLine("========================================");

            // cargar historial desde el archivo JSON
            List<Venta> historialVentas = ArchivoJson.Cargar<Venta>("Ventas.json");

            if (historialVentas.Count == 0)
            {
                Console.WriteLine("\nNo se han registrado ventas en la base de datos aún.");
            }
            else
            {
                Console.WriteLine("\n--- DETALLE DE VENTAS REGISTRADAS (JSON) ---");
                decimal totalGeneral = 0;

                foreach (var v in historialVentas)
                {
                    Console.WriteLine($"Venta #{v.Id} | Mesa {v.NumeroMesa} | Total: ${v.Total:F2} | Hora: {v.Fecha:HH:mm:ss}");
                    totalGeneral += v.Total;
                }

                Console.WriteLine("---------------------------------------");
                Console.WriteLine($"Total recaudado en caja hoy: ${totalGeneral:F2}");
            }

            Console.WriteLine("===========================================");

        }
        public static void GestionarMenu()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("        GESTIÓN DEL MENÚ DIGITAL        ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar nuevo Plato/Bebida");
            Console.WriteLine("2. Ver Menú de Platos Registrados");
            Console.Write("Seleccione una opción: ");
            string op = Console.ReadLine();

            if (op == "1")
            {
                Console.Write("Nombre del plato/bebida: ");
                string nombre = Console.ReadLine();

                Console.Write("Precio ($): ");
                double.TryParse(Console.ReadLine(), out double precio);

                Console.Write("Categoría (Plato Fuerte / Entrada / Bebida / Postre): ");
                string categoria = Console.ReadLine();

                int nuevoId = MenuPlatos.Count + 1;
                MenuPlatos.Add(new ItemMenu(nuevoId, nombre, precio, categoria));

                Console.WriteLine("\n¡Plato/Bebida registrado en el menú con éxito!");
            }
            else if (op == "2")
            {
                Console.WriteLine("\n--- MENÚ ACTUAL DEL RESTAURANTE ---");
                if (MenuPlatos.Count == 0)
                {
                    Console.WriteLine("El menú está vacío por ahora.");
                }
                else
                {
                    foreach (var item in MenuPlatos)
                    {
                        Console.WriteLine($"ID: {item.Id} | {item.Nombre} | Precio: ${item.Precio:F2} | Cat: {item.Categoria}");
                    }
                }
            }
        }
    }
}