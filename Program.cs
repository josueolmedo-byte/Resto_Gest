using Twilio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Resto_Gest
{
    public class Program
    {
        public static List<Pedido> Pedidos = new List<Pedido>();

        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();

                if (!db.Mesas.Any())
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        db.Mesas.Add(new Mesa(i));
                    }
                    db.SaveChanges();
                }
            }

            int opcion = 0;
            do
            {
                Console.Clear();

                // Guardar el color original para restaurarlo después
                ConsoleColor originalColor = Console.ForegroundColor;

                // --- ENCABEZADO ELEGANTE RESTOGEST ---
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(@"╔══════════════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine(@"║                                                                                          ║");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(@"║        ██████╗ ███████╗███████╗████████╗██████╗  ██████╗ ███████╗███████╗████████╗       ║");
                Console.WriteLine(@"║        ██╔══██╗██╔════╝██╔════╝╚══██╔══╝██╔══██╗██╔════╝ ██╔════╝██╔════╝╚══██╔══╝       ║");
                Console.WriteLine(@"║        ██████╔╝█████╗  ███████╗   ██║   ██║  ██║██║  ███╗█████╗  ███████╗   ██║          ║");
                Console.WriteLine(@"║        ██╔══██╗██╔══╝  ╚════██║   ██║   ██║  ██║██║   ██║██╔══╝  ╚════██║   ██║          ║");
                Console.WriteLine(@"║        ██║  ██║███████╗███████║   ██║   ██████╔╝╚██████╔╝███████╗███████║   ██║          ║");
                Console.WriteLine(@"║        ╚═╝  ╚═╝╚══════╝╚══════╝   ╚═╝   ╚═════╝  ╚═════╝ ╚══════╝╚══════╝   ╚═╝          ║");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(@"║                                                                                          ║");
                Console.WriteLine(@"║                           S I S T E M A   D E   P E D I D O S                            ║");
                Console.WriteLine(@"║                                                                                          ║");
                Console.WriteLine(@"╚══════════════════════════════════════════════════════════════════════════════════════════╝");
                Console.WriteLine();

                // --- MENÚ DE OPCIONES ---
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("  [1] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Gestionar Menú (Platos y Bebidas)");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("  [2] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Ver Estado de Mesas");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("  [3] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Aperturar Mesa y Registrar Pedido");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("  [4] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Ver Cola de Pedidos en Cocina");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("  [5] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Generar Cuenta y Procesar Pago");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write("  [6] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Ver Reporte de Ventas del Turno");

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("  [7] ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Salir");

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("────────────────────────────────────────────────────────────────────");

                // --- PROMPT DE ENTRADA ---
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(" ❯ Seleccione una opción: ");

                // Restablecer el color por defecto
                Console.ForegroundColor = originalColor;

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

        public static void GestionarMenu()
        {
            Console.Clear();

            // Guardar el color original para restaurarlo después
            ConsoleColor originalColor = Console.ForegroundColor;

            // --- ENCABEZADO ELEGANTE SUBMENÚ MENÚ DIGITAL ---
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"╔══════════════════════════════════════════════════════════════════╗");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"║                  GESTIÓN DEL MENÚ DIGITAL                        ║");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"╚══════════════════════════════════════════════════════════════════╝");
            Console.WriteLine();

            // --- OPCIONES DEL SUBMENÚ ---
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("  [1] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Registrar nuevo Plato/Bebida");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("  [2] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Ver Menú de Platos Registrados");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("  [3] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Editar Plato/Bebida");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("  [4] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Eliminar Plato/Bebida");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("────────────────────────────────────────────────────────────────────");

            // --- PROMPT DE ENTRADA ---
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(" ❯ Seleccione una opción: ");

            // Restablecer el color original para la entrada del usuario
            Console.ForegroundColor = originalColor;
            string op = Console.ReadLine() ?? "";

            using (var db = new AppDbContext())
            {
                if (op == "1")
                {
                    Console.Write("\nNombre del plato/bebida: ");
                    string nombre = Console.ReadLine() ?? "";

                    Console.Write("Precio ($): ");
                    double.TryParse(Console.ReadLine(), out double precio);

                    Console.Write("Categoría (Plato Fuerte / Entrada / Bebida / Postre): ");
                    string categoria = Console.ReadLine() ?? "";

                    ItemMenu nuevoItem = new ItemMenu(0, nombre, precio, categoria);
                    db.ItemsMenu.Add(nuevoItem);
                    db.SaveChanges();

                    Console.WriteLine("\n¡Plato/Bebida registrado exitosamente en SQL Server!");
                }
                else if (op == "2")
                {
                    Console.WriteLine("\n══ MENÚ ACTUAL DEL RESTAURANTE (SQL SERVER) ══");
                    var listaMenu = db.ItemsMenu.ToList();

                    if (listaMenu.Count == 0)
                    {
                        Console.WriteLine("El menú está vacío por ahora.");
                    }
                    else
                    {
                        foreach (var item in listaMenu)
                        {
                            Console.WriteLine($"ID: {item.Id} | {item.Nombre} | Precio: ${item.Precio:F2} | Cat: {item.Categoria}");
                        }
                    }
                }
                else if (op == "3")
                {
                    Console.WriteLine("\n══ EDITAR ÍTEM DEL MENÚ ══");
                    var listaMenu = db.ItemsMenu.ToList();
                    if (listaMenu.Count == 0)
                    {
                        Console.WriteLine("No hay ítems en el menú para editar.");
                        return;
                    }

                    foreach (var item in listaMenu)
                    {
                        Console.WriteLine($"ID: {item.Id} | {item.Nombre} | Precio: ${item.Precio:F2}");
                    }

                    Console.Write("\nIngrese el ID del ítem a editar: ");
                    if (int.TryParse(Console.ReadLine(), out int idEditar))
                    {
                        var itemAEditar = db.ItemsMenu.Find(idEditar);

                        if (itemAEditar != null)
                        {
                            Console.Write($"Nuevo Nombre (Actual: {itemAEditar.Nombre}): ");
                            string nuevoNombre = Console.ReadLine() ?? "";
                            if (!string.IsNullOrWhiteSpace(nuevoNombre)) itemAEditar.Nombre = nuevoNombre;

                            Console.Write($"Nuevo Precio (Actual: ${itemAEditar.Precio:F2}): ");
                            string nuevoPrecioStr = Console.ReadLine() ?? "";
                            if (double.TryParse(nuevoPrecioStr, out double nuevoPrecio)) itemAEditar.Precio = nuevoPrecio;

                            Console.Write($"Nueva Categoría (Actual: {itemAEditar.Categoria}): ");
                            string nuevaCat = Console.ReadLine() ?? "";
                            if (!string.IsNullOrWhiteSpace(nuevaCat)) itemAEditar.Categoria = nuevaCat;

                            db.SaveChanges();
                            Console.WriteLine("\n¡Ítem actualizado con éxito en SQL Server!");
                        }
                        else
                        {
                            Console.WriteLine("\nNo se encontró ningún ítem con ese ID.");
                        }
                    }
                }
                else if (op == "4")
                {
                    Console.WriteLine("\n══ ELIMINAR ÍTEM DEL MENÚ ══");
                    var listaMenu = db.ItemsMenu.ToList();
                    if (listaMenu.Count == 0)
                    {
                        Console.WriteLine("No hay ítems en el menú para eliminar.");
                        return;
                    }

                    foreach (var item in listaMenu)
                    {
                        Console.WriteLine($"ID: {item.Id} | {item.Nombre} | Precio: ${item.Precio:F2}");
                    }

                    Console.Write("\nIngrese el ID del ítem a eliminar: ");
                    if (int.TryParse(Console.ReadLine(), out int idEliminar))
                    {
                        var itemAEliminar = db.ItemsMenu.Find(idEliminar);

                        if (itemAEliminar != null)
                        {
                            db.ItemsMenu.Remove(itemAEliminar);
                            db.SaveChanges();
                            Console.WriteLine("\n¡Ítem eliminado correctamente de SQL Server!");
                        }
                        else
                        {
                            Console.WriteLine("\nNo se encontró ningún ítem con ese ID.");
                        }
                    }
                }
            }
        }

        public static void VerEstadoMesas()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║          ESTADO DE LAS MESAS           ║");
            Console.WriteLine("╚════════════════════════════════════════╝");

            using (var db = new AppDbContext())
            {
                var listaMesas = db.Mesas.OrderBy(m => m.Numero).ToList();

                foreach (var mesa in listaMesas)
                {
                    Console.WriteLine($"Mesa #{mesa.Numero} ---> Estado: [{mesa.Estado}]");
                }
            }
        }

        public static void AperturarMesaYPedido()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║        APERTURAR MESA Y TOMAR PEDIDO   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();

            using (var db = new AppDbContext())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Ingrese el número de mesa a aperturar: ");
                Console.ResetColor();

                if (int.TryParse(Console.ReadLine(), out int numMesa))
                {
                    var mesa = db.Mesas.Find(numMesa);

                    if (mesa == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Mesa no encontrada.");
                        Console.ResetColor();
                        return;
                    }

                    if (mesa.Estado == "Ocupada")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Esta mesa ya se encuentra ocupada.");
                        Console.ResetColor();
                        return;
                    }

                    var menuDisponible = db.ItemsMenu.ToList();
                    if (menuDisponible.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nNo hay platos en el menú. Registre platos primero.");
                        Console.ResetColor();
                        return;
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("Nombre del mesero responsable: ");
                    Console.ResetColor();
                    string mesero = Console.ReadLine() ?? "";

                    int nuevoIdPedido = Pedidos.Count + 1;
                    Pedido nuevoPedido = new Pedido(nuevoIdPedido, numMesa, mesero);

                    string agregarMas = "S";
                    while (agregarMas.ToUpper() == "S")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n══ MENÚ DISPONIBLE ══");
                        Console.ResetColor();
                        foreach (var item in menuDisponible)
                        {
                            Console.WriteLine($"ID: {item.Id} | {item.Nombre} - ${item.Precio:F2}");
                        }

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Ingrese el ID del plato/bebida a añadir: ");
                        Console.ResetColor();

                        if (int.TryParse(Console.ReadLine(), out int idItem))
                        {
                            var itemElegido = menuDisponible.Find(i => i.Id == idItem);
                            if (itemElegido != null)
                            {
                                nuevoPedido.Platos.Add(itemElegido);

                                // Contamos si el mismo plato se ha pedido repetido en este pedido
                                int cantidadActual = nuevoPedido.Platos.Count(p => p.Id == itemElegido.Id);

                                Console.ForegroundColor = ConsoleColor.Green;
                                if (cantidadActual > 1)
                                {
                                    Console.WriteLine($"{itemElegido.Nombre} agregado al pedido! (x{cantidadActual})");
                                }
                                else
                                {
                                    Console.WriteLine($"{itemElegido.Nombre} agregado al pedido!");
                                }
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("ID de item no válido.");
                                Console.ResetColor();
                            }
                        }

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("¿Desea agregar otro plato? (S/N): ");
                        Console.ResetColor();
                        agregarMas = Console.ReadLine() ?? "N";
                    }

                    if (nuevoPedido.Platos.Count > 0)
                    {
                        Pedidos.Add(nuevoPedido);
                        mesa.Estado = "Ocupada";
                        db.SaveChanges();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\nPedido #{nuevoPedido.Id} registrado exitosamente para la Mesa #{numMesa}!");
                        Console.ResetColor();
                    }
                }
            }
        }

        public static void VerColaCocina()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║          COLA DE PEDIDOS EN COCINA     ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();

            if (Pedidos.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No hay pedidos registrados en cocina.");
                Console.ResetColor();
                return;
            }

            foreach (var ped in Pedidos)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nPedido #{ped.Id} | Mesa #{ped.NumeroMesa} | Mesero: {ped.Mesero} | Estado: {ped.Estado}");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Platos/Bebidas a preparar:");
                Console.ResetColor();

                // Agrupación por ID para consolidar repetidos en cocina
                var platosAgrupados = ped.Platos.GroupBy(p => p.Id);

                foreach (var grupo in platosAgrupados)
                {
                    var plato = grupo.First();
                    int cantidad = grupo.Count();

                    if (cantidad > 1)
                    {
                        Console.WriteLine($"  - {plato.Nombre} ({plato.Categoria}) x{cantidad}");
                    }
                    else
                    {
                        Console.WriteLine($"  - {plato.Nombre} ({plato.Categoria})");
                    }
                }
            }
        }

        public static void ProcesarPagoMesa()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║       GENERAR CUENTA Y PROCESAR PAGO   ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Ingrese el número de mesa a cobrar: ");
            Console.ResetColor();

            if (int.TryParse(Console.ReadLine(), out int numMesa))
            {
                Pedido? pedidoMesa = Pedidos.Find(p => p.NumeroMesa == numMesa);

                if (pedidoMesa != null)
                {
                    double subtotal = 0;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n══ DETALLE DE CONSUMO (MESA {numMesa}) ══");
                    Console.ResetColor();

                    // Agrupación de items repetidos y cálculo acumulado
                    var platosAgrupados = pedidoMesa.Platos.GroupBy(p => p.Id);

                    foreach (var grupo in platosAgrupados)
                    {
                        var item = grupo.First();
                        int cantidad = grupo.Count();
                        double totalItem = item.Precio * cantidad;

                        if (cantidad > 1)
                        {
                            Console.WriteLine($"- {item.Nombre} x{cantidad}: ${totalItem:F2} (${item.Precio:F2} c/u)");
                        }
                        else
                        {
                            Console.WriteLine($"- {item.Nombre}: ${item.Precio:F2}");
                        }

                        subtotal += totalItem;
                    }

                    double iva = subtotal * 0.15;
                    double total = subtotal + iva;

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("════════════════════════════════════════════════");
                    Console.ResetColor();
                    Console.WriteLine($"Subtotal:   ${subtotal:F2}");
                    Console.WriteLine($"IVA (15%):  ${iva:F2}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"TOTAL:      ${total:F2}");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("════════════════════════════════════════════════");
                    Console.ResetColor();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("¿Confirmar pago y cerrar mesa? (S/N): ");
                    Console.ResetColor();
                    string op = Console.ReadLine() ?? "";

                    if (op.ToUpper() == "S")
                    {
                        using (var db = new AppDbContext())
                        {
                            var mesaDb = db.Mesas.Find(numMesa);
                            if (mesaDb != null)
                            {
                                mesaDb.Estado = "Libre";
                            }

                            Venta nuevaVenta = new Venta
                            {
                                NumeroMesa = numMesa,
                                Total = (decimal)total,
                                Fecha = DateTime.Now
                            };

                            db.Ventas.Add(nuevaVenta);
                            db.SaveChanges();
                        }

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n══ ENVÍO DE FACTURA DIGITAL ══");
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Ingrese el correo electrónico del cliente: ");
                        Console.ResetColor();
                        string correoCliente = Console.ReadLine() ?? "";

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("Ingrese el número de celular del cliente (ej: +5939XXXXXXXX): ");
                        Console.ResetColor();
                        string celularCliente = Console.ReadLine() ?? "";

                        if (!string.IsNullOrEmpty(correoCliente))
                        {
                            EnviarCorreoReal(correoCliente, "Factura de Consumo - RestoGest", $"Gracias por su compra. Total: ${total:F2}");
                        }

                        if (!string.IsNullOrEmpty(celularCliente))
                        {
                            EnviarWhatsAppReal(celularCliente, $"¡Hola! Su factura de RestoGest por un total de ${total:F2} ha sido procesada con éxito.");
                        }

                        Pedidos.Remove(pedidoMesa);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nPago procesado con éxito, mesa liberada y venta registrada en SQL Server!");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNo se encontró ningún pedido activo para esa mesa.");
                    Console.ResetColor();
                }
            }
        }

        public static void VerReporteVentas()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║        REPORTE DE VENTAS DEL TURNO     ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine("1. Ver Historial de Ventas");
            Console.WriteLine("2. Anular / Eliminar Venta por ID");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Seleccione una opción: ");
            Console.ResetColor();
            string op = Console.ReadLine() ?? "";

            using (var db = new AppDbContext())
            {
                if (op == "1")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n══ DETALLE DE VENTAS REGISTRADAS (SQL SERVER) ══");
                    Console.ResetColor();

                    var historialVentas = db.Ventas.ToList();

                    if (historialVentas.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No se han registrado ventas en la base de datos aún.");
                        Console.ResetColor();
                    }
                    else
                    {
                        decimal totalGeneral = 0;
                        foreach (var v in historialVentas)
                        {
                            Console.WriteLine($"Venta #{v.Id} | Mesa {v.NumeroMesa} | Total: ${v.Total:F2} | Hora: {v.Fecha:HH:mm:ss}");
                            totalGeneral += v.Total;
                        }

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("════════════════════════════════════════════════");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Total recaudado en caja: ${totalGeneral:F2}");
                        Console.ResetColor();
                    }
                }
                else if (op == "2")
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n══ ANULAR / ELIMINAR VENTA ══");
                    Console.ResetColor();

                    var historialVentas = db.Ventas.ToList();

                    if (historialVentas.Count == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No hay ventas registradas para eliminar.");
                        Console.ResetColor();
                        return;
                    }

                    foreach (var v in historialVentas)
                    {
                        Console.WriteLine($"Venta #{v.Id} | Mesa {v.NumeroMesa} | Total: ${v.Total:F2} | Fecha: {v.Fecha}");
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("\nIngrese el ID de la venta a eliminar: ");
                    Console.ResetColor();

                    if (int.TryParse(Console.ReadLine(), out int idVenta))
                    {
                        var ventaAEliminar = db.Ventas.Find(idVenta);

                        if (ventaAEliminar != null)
                        {
                            db.Ventas.Remove(ventaAEliminar);
                            db.SaveChanges();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\nVenta #{idVenta} eliminada correctamente de SQL Server!");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nNo se encontró ninguna venta con ese ID.");
                            Console.ResetColor();
                        }
                    }
                }
            }
        }

        public static void EnviarCorreoReal(string destinatario, string asunto, string cuerpoMensaje)
        {
            try
            {
                string remitente = "arielsebastianveliz@gmail.com";
                string passwordApp = "exul mvvh wrhy tdyp";

                System.Net.Mail.MailMessage mensaje = new System.Net.Mail.MailMessage();
                mensaje.From = new System.Net.Mail.MailAddress(remitente, "Sistema RestoGest");
                mensaje.To.Add(destinatario);
                mensaje.Subject = asunto;
                mensaje.Body = cuerpoMensaje;
                mensaje.IsBodyHtml = false;

                System.Net.Mail.SmtpClient clienteSmtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                clienteSmtp.Credentials = new System.Net.NetworkCredential(remitente, passwordApp);
                clienteSmtp.EnableSsl = true;

                clienteSmtp.Send(mensaje);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[EMAIL] ¡Correo de factura enviado exitosamente al destinatario!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[EMAIL ERROR] No se pudo enviar el correo: {ex.Message}");
                Console.ResetColor();
            }
        }

        public static void EnviarWhatsAppReal(string numeroCelular, string mensajeTexto)
        {
            try
            {
                string numeroLimpio = numeroCelular.Replace("+", "").Trim();
                string mensajeCodificado = Uri.EscapeDataString(mensajeTexto);
                string urlWhatsApp = $"https://wa.me/{numeroLimpio}?text={mensajeCodificado}";

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = urlWhatsApp,
                    UseShellExecute = true
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[WHATSAPP] ¡Ventana de WhatsApp abierta con la factura lista para enviar!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[WHATSAPP ERROR] No se pudo abrir WhatsApp: {ex.Message}");
                Console.ResetColor();
            }
        }

    }
}