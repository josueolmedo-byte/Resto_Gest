# Resto_Gest
# RestoGest - Sistema de Gestión de Pedidos

Módulo de Cocina, Cobros y Ventas (Integrante 3 - Ariel)

Se implementaron las siguientes funcionalidades dentro del sistema:

- Cola de Pedidos en Cocina (VerColaCocina): Muestra el listado de pedidos pendientes a preparar con sus respectivos platos y datos.
- Procesamiento de Pagos (ProcesarPagoMesa): Desglosa el consumo de la mesa, calcula el 15% de IVA, procesa el cobro, acumula la venta y libera la mesa.
- Reporte de Ventas (VerReporteVentas): Muestra el total acumulado de las ventas del turno (TotalVentasDelDia).

Los módulos correspondientes a la gestión del menú y el control de mesas serán documentados por sus respectivos integrantes.

2026-07-24
  Creación de la clase ItemMenu con sus propiedades y constructor
  Creación de las clases base Mesa y Pedido
  Creación de la estructura del menú principal en Program.cs
  Implementación de listas estáticas para almacenamiento en memoria (ItemMenu, Mesas, Pedidos)
  Desarrollo de la función GestionarMenu para registrar y listar platos/bebidas con validaciones básicas
