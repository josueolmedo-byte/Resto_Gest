using System;
using System.Collections.Generic;
using Resto_Gest;

namespace RestoGest
{
    public class Program
    {
        // Listas globales que usará todo el sistema para guardar los datos
        public static List<ItemMenu> MenuPlatos = new List<ItemMenu>();
        public static List<Mesa> Mesas = new List<Mesa>();
        public static List<Pedido> Pedidos = new List<Pedido>();

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
                            VerEstadoMesas();
                            break;
                        case 3:
                            AperturarMesaYPedido();
                            break;
                        case 4:
                            Console.WriteLine("\n[En desarrollo por Integrante 3]");
                            break;
                        case 5:
                            Console.WriteLine("\n[En desarrollo por Integrante 3]");
                            break;
                        case 6:
                            Console.WriteLine("\n[En desarrollo por Integrante 3]");
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

        public static void VerEstadoMesas()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("         ESTADO DE LAS MESAS            ");
            Console.WriteLine("========================================");

            foreach (var mesa in Mesas)
            {
                Console.WriteLine($"Mesa #{mesa.Numero} ---> Estado: [{mesa.Estado}]");
            }
        }

        public static void AperturarMesaYPedido()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("    APERTURAR MESA Y REGISTRAR PEDIDO   ");
            Console.WriteLine("========================================");

            Console.Write("Ingrese el número de mesa (1 al 5): ");
            if (int.TryParse(Console.ReadLine(), out int numMesa))
            {
                Mesa mesa = Mesas.Find(m => m.Numero == numMesa);
                if (mesa == null)
                {
                    Console.WriteLine("Mesa no encontrada.");
                    return;
                }

                if (mesa.Estado == "Ocupada")
                {
                    Console.WriteLine("Esta mesa ya se encuentra ocupada.");
                    return;
                }

                Console.Write("Nombre del mesero responsable: ");
                string mesero = Console.ReadLine();

                int nuevoIdPedido = Pedidos.Count + 1;
                Pedido nuevoPedido = new Pedido(nuevoIdPedido, numMesa, mesero);

                if (MenuPlatos.Count == 0)
                {
                    Console.WriteLine("\nNo hay platos en el menú.");
                    return;
                }

                string agregarMas = "S";
                while (agregarMas.ToUpper() == "S")
                {
                    Console.WriteLine("\n--- MENÚ DISPONIBLE ---");
                    foreach (var item in MenuPlatos)
                    {
                        Console.WriteLine($"ID: {item.Id} | {item.Nombre} - ${item.Precio}");
                    }

                    Console.Write("Ingrese el ID del plato/bebida a añadir: ");
                    if (int.TryParse(Console.ReadLine(), out int idItem))
                    {
                        ItemMenu itemElegido = MenuPlatos.Find(i => i.Id == idItem);
                        if (itemElegido != null)
                        {
                            nuevoPedido.Platos.Add(itemElegido);
                            Console.WriteLine($"¡{itemElegido.Nombre} agregado al pedido!");
                        }
                        else
                        {
                            Console.WriteLine("ID de item no válido.");
                        }
                    }

                    Console.Write("¿Desea agregar otro plato? (S/N): ");
                    agregarMas = Console.ReadLine();
                }

                if (nuevoPedido.Platos.Count > 0)
                {
                    Pedidos.Add(nuevoPedido);
                    mesa.Estado = "Ocupada";
                    Console.WriteLine($"\n¡Pedido #{nuevoPedido.Id} registrado exitosamente para la Mesa #{numMesa}!");
                }
            }
        }
    }
}