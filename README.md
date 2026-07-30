# Resto_Gest
# RestoGest - Sistema de Gestión de Pedidos

Módulo de Cocina, Cobros y Ventas (Integrante 3 - Ariel)

Se implementaron las siguientes funcionalidades dentro del sistema:

- Cola de Pedidos en Cocina (VerColaCocina): Muestra el listado de pedidos pendientes a preparar con sus respectivos platos y datos.
- Procesamiento de Pagos (ProcesarPagoMesa): Desglosa el consumo de la mesa, calcula el 15% de IVA, procesa el cobro, acumula la venta y libera la mesa.
- Reporte de Ventas (VerReporteVentas): Muestra el total acumulado de las ventas del turno (TotalVentasDelDia).
- ArchivoJson creado - clase con métodos estáticos para guardar y cargar datos automáticamente desde archivos .json usando JsonSerializer.

Los módulos correspondientes a la gestión del menú y el control de mesas serán documentados por sus respectivos integrantes.

2026-07-24
  Creación de la clase ItemMenu con sus propiedades y constructor
  Creación de las clases base Mesa y Pedido
  Creación de la estructura del menú principal en Program.cs
  Implementación de listas estáticas para almacenamiento en memoria (ItemMenu, Mesas, Pedidos)
  Desarrollo de la función GestionarMenu para registrar y listar platos/bebidas con validaciones básicas


Modulo de Mesas y Registro de Pedidos (Integrante 2 - Joshua)

2026-07-26
Se implementaron funcionalidades relacionadas con la administración de mesas y el registro de pedidos dentro del sistema.

- Gestión de mesas:
  - Registro y control de las mesas disponibles en el restaurante.
  - Administración del estado de las mesas.
  - Asignación de pedidos según la mesa seleccionada.

- Registro de pedidos:
  - Creación de nuevos pedidos realizados por los clientes.
  - Asociación de platos del menú con cada pedido.
  - Almacenamiento de la información de consumo.

- Integración con el sistema principal:
  - Uso de listas para manejar la información de mesas y pedidos.
  - Organización de los datos para permitir la interacción entre clientes, mesas y órdenes.


El módulo permite mantener organizado el flujo de atención del restaurante, facilitando el control de mesas ocupadas, disponibles y los pedidos registrados.
