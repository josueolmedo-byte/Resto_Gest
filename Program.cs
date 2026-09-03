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
            Console.WriteLine("========================================");
            Console.WriteLine("        GESTIÓN DEL MENÚ DIGITAL        ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar nuevo Plato/Bebida");
            Console.WriteLine("2. Ver Menú de Platos Registrados");
            Console.WriteLine("3. Editar Plato/Bebida");
            Console.WriteLine("4. Eliminar Plato/Bebida");
            Console.Write("Seleccione una opción: ");
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
                    Console.WriteLine("\n--- MENÚ ACTUAL DEL RESTAURANTE (SQL SERVER) ---");
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
                    Console.WriteLine("\n--- EDITAR ÍTEM DEL MENÚ ---");
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
                    Console.WriteLine("\n--- ELIMINAR ÍTEM DEL MENÚ ---");
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
            Console.WriteLine("========================================");
            Console.WriteLine("          ESTADO DE LAS MESAS           ");
            Console.WriteLine("========================================");

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
            Console.WriteLine("========================================");
            Console.WriteLine("    APERTURAR MESA Y REGISTRAR PEDIDO   ");
            Console.WriteLine("========================================");

            Console.Write("Ingrese el número de mesa (1 al 5): ");
            if (int.TryParse(Console.ReadLine(), out int numMesa))
            {
                using (var db = new AppDbContext())
                {
                    var mesa = db.Mesas.Find(numMesa);
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

                    var menuDisponible = db.ItemsMenu.ToList();
                    if (menuDisponible.Count == 0)
                    {
                        Console.WriteLine("\nNo hay platos en el menú. Registre platos primero.");
                        return;
                    }

                    Console.Write("Nombre del mesero responsable: ");
                    string mesero = Console.ReadLine() ?? "";

                    int nuevoIdPedido = Pedidos.Count + 1;
                    Pedido nuevoPedido = new Pedido(nuevoIdPedido, numMesa, mesero);

                    string agregarMas = "S";
                    while (agregarMas.ToUpper() == "S")
                    {
                        Console.WriteLine("\n--- MENÚ DISPONIBLE ---");
                        foreach (var item in menuDisponible)
                        {
                            Console.WriteLine($"ID: {item.Id} | {item.Nombre} - ${item.Precio:F2}");
                        }

                        Console.Write("Ingrese el ID del plato/bebida a añadir: ");
                        if (int.TryParse(Console.ReadLine(), out int idItem))
                        {
                            var itemElegido = menuDisponible.Find(i => i.Id == idItem);
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
                        agregarMas = Console.ReadLine() ?? "N";
                    }

                    if (nuevoPedido.Platos.Count > 0)
                    {
                        Pedidos.Add(nuevoPedido);
                        mesa.Estado = "Ocupada";
                        db.SaveChanges();

                        Console.WriteLine($"\n¡Pedido #{nuevoPedido.Id} registrado exitosamente para la Mesa #{numMesa}!");
                    }
                }
            }
        }

        public static void VerColaCocina()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("        COLA DE PEDIDOS EN COCINA        ");
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
                Pedido? pedidoMesa = Pedidos.Find(p => p.NumeroMesa == numMesa);

                if (pedidoMesa != null)
                {
                    double subtotal = 0;
                    Console.WriteLine($"\n--- DETALLE DE CONSUMO (MESA {numMesa}) ---");
                    foreach (var item in pedidoMesa.Platos)
                    {
                        Console.WriteLine($"- {item.Nombre}: ${item.Precio:F2}");
                        subtotal += item.Precio;
                    }

                    double iva = subtotal * 0.15;
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

                        Console.WriteLine("\n--- ENVÍO DE FACTURA DIGITAL ---");
                        Console.Write("Ingrese el correo electrónico del cliente: ");
                        string correoCliente = Console.ReadLine() ?? "";

                        Console.Write("Ingrese el número de celular del cliente (ej: +5939XXXXXXXX): ");
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

                        Console.WriteLine("\n¡Pago procesado con éxito, mesa liberada y venta registrada en SQL Server!");
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
            Console.WriteLine("1. Ver Historial de Ventas");
            Console.WriteLine("2. Anular / Eliminar Venta por ID");
            Console.Write("Seleccione una opción: ");
            string op = Console.ReadLine() ?? "";

            using (var db = new AppDbContext())
            {
                if (op == "1")
                {
                    Console.WriteLine("\n--- DETALLE DE VENTAS REGISTRADAS (SQL SERVER) ---");
                    var historialVentas = db.Ventas.ToList();

                    if (historialVentas.Count == 0)
                    {
                        Console.WriteLine("No se han registrado ventas en la base de datos aún.");
                    }
                    else
                    {
                        decimal totalGeneral = 0;
                        foreach (var v in historialVentas)
                        {
                            Console.WriteLine($"Venta #{v.Id} | Mesa {v.NumeroMesa} | Total: ${v.Total:F2} | Hora: {v.Fecha:HH:mm:ss}");
                            totalGeneral += v.Total;
                        }
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine($"Total recaudado en caja: ${totalGeneral:F2}");
                    }
                }
                else if (op == "2")
                {
                    Console.WriteLine("\n--- ANULAR / ELIMINAR VENTA ---");
                    var historialVentas = db.Ventas.ToList();

                    if (historialVentas.Count == 0)
                    {
                        Console.WriteLine("No hay ventas registradas para eliminar.");
                        return;
                    }

                    foreach (var v in historialVentas)
                    {
                        Console.WriteLine($"Venta #{v.Id} | Mesa {v.NumeroMesa} | Total: ${v.Total:F2} | Fecha: {v.Fecha}");
                    }

                    Console.Write("\nIngrese el ID de la venta a eliminar: ");
                    if (int.TryParse(Console.ReadLine(), out int idVenta))
                    {
                        var ventaAEliminar = db.Ventas.Find(idVenta);

                        if (ventaAEliminar != null)
                        {
                            db.Ventas.Remove(ventaAEliminar);
                            db.SaveChanges();
                            Console.WriteLine($"\n¡Venta #{idVenta} eliminada correctamente de SQL Server!");
                        }
                        else
                        {
                            Console.WriteLine("\nNo se encontró ninguna venta con ese ID.");
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
                Console.WriteLine("\n[EMAIL] ¡Correo de factura enviado exitosamente al destinatario!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[EMAIL ERROR] No se pudo enviar el correo: {ex.Message}");
            }
        }

        public static void EnviarWhatsAppReal(string numeroCelular, string mensajeTexto)
        {
            try
            {
                // Limpiamos el número por si acaso
                string numeroLimpio = numeroCelular.Replace("+", "").Trim();

                // Codificamos el mensaje para que la URL lo entienda sin espacios ni tildes extrañas
                string mensajeCodificado = Uri.EscapeDataString(mensajeTexto);

                // Creamos la URL oficial de WhatsApp
                string urlWhatsApp = $"https://wa.me/{numeroLimpio}?text={mensajeCodificado}";

                // Abrimos el navegador o la app predeterminada de Windows con el mensaje preparado
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = urlWhatsApp,
                    UseShellExecute = true
                });

                Console.WriteLine("\n[WHATSAPP] ¡Ventana de WhatsApp abierta con la factura lista para enviar!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[WHATSAPP ERROR] No se pudo abrir WhatsApp: {ex.Message}");
            }
        }
    }
}