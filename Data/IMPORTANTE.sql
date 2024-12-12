-- ========================================
-- Insertar en la tabla Permiso
-- ========================================
--INSERT INTO Permisos (id_permiso, nombre_permiso, estado_permiso)
--VALUES (1, 'Acceso a Ventas', 1),
  --     (2, 'Acceso a Pedidos', 1),
    --   (3, 'Acceso a Inventarios', 1),
      -- (4, 'Acceso a Reportes', 0),
       --(5, 'Acceso a Configuraciones', 1);

INSERT INTO Permisos (id_permiso, nombre_permiso, estado_permiso) VALUES
(1, 'Rol', 1),          -- Activo
(2, 'Usuario', 1),      -- Activo
(3, 'Acceso', 1),       -- Activo
(4, 'Compra', 1),       -- Activo
(5, 'Proveedores', 1),   -- Activo
(6, 'Productos', 1),     -- Activo
(7, 'Cliente', 1),       -- Activo
(8, 'Orden de producto', 1), -- Activo
(9, 'Venta', 1);

-- ========================================
-- Insertar en la tabla Privilegio
-- ========================================
INSERT INTO Privilegios (id_privilegio, nombre_privilegio)
VALUES (1, 'Leer'),
       (2, 'Editar'),
       (3, 'Crear'),
       (4, 'Elimianr'),
       (5, 'Descargar');

-- ========================================
-- Insertar en la tabla Rol
-- ========================================
INSERT INTO Roles (id_rol, nombre_rol, estado_rol)
VALUES (1, 'Administrador', 1),
       (2, 'Empleado', 1),
       (3, 'Cliente', 1),
       (4, 'Invitado', 0),
       (5, 'Supervisor', 1);

-- ========================================
-- Insertar en la tabla Usuario
-- ========================================
INSERT INTO Usuarios (id_usuario, tarjeta_identidad, correo_usuario, nombre_usuario, contrasena_usuario, cumpleano_usuario, fecha_creacion_usuario, genero_usuario, estado_usuario, fk_rol)
VALUES (1, '1010101010', 'admin@boxnova.com', 'Administrador', '123456', '1985-06-15', '2024-01-01', 'M', 1, 1),
       (2, '2020202020', 'empleado@boxnova.com', 'Empleado1', '123456', '1990-08-10', '2024-02-01', 'F', 1, 2),
       (3, '3030303030', 'cliente@boxnova.com', 'Cliente1', '123456', '1995-12-05', '2024-03-01', 'M', 1, 3);

-- ========================================
-- Insertar en la tabla PerXrolXpriv
-- ========================================
INSERT INTO PerXRolXPriv (id_perxrol, id_per, id_rol, id_priv)
VALUES (1, 1, 1, 1),
       (2, 2, 1, 2),
       (3, 3, 1, 3),
       (4, 4, 1, 4),
       (5, 5, 1, 5);

-- ========================================
-- Insertar en la tabla Producto
-- ========================================
INSERT INTO Producto (NombreProducto, PrecioProducto, StockProducto, CategoriaProducto, ClasificacionProducto, EstadoProducto)
VALUES ('Camiseta', 50.00, 100, 'Ropa', 'Casual', 1),
       ('Jeans', 75.00, 50, 'Ropa', 'Casual', 1),
       ('Tenis', 120.00, 30, 'Calzado', 'Deportivo', 1),
       ('Sudadera', 80.00, 40, 'Ropa', 'Deportiva', 1),
       ('Abrigo', 200.00, 20, 'Ropa', 'Invierno', 1);

-- ========================================
-- Insertar en la tabla CategoriaProducto
-- ========================================
INSERT INTO CategoriaProducto (IdCProd, NombreCProd, EstadoCProd)
VALUES (1, 'Ropa', 1),
       (2, 'Calzado', 1),
       (3, 'Accesorios', 1),
       (4, 'Electrónica', 0),
       (5, 'Hogar', 1);

-- ========================================
-- Insertar en la tabla SubCategoriaProducto
-- ========================================
INSERT INTO SubCategoriaProducto (IdSubCProd, NombreCProd, EstadoCProd, IdCProd)
VALUES (1, 'Casual', 1, 1),
       (2, 'Deportiva', 1, 1),
       (3, 'Formal', 1, 1),
       (4, 'Invierno', 1, 1),
       (5, 'Deportivo', 1, 2);

-- ========================================
-- Insertar en la tabla Cliente
-- ========================================
INSERT INTO Cliente (IdCliente, NombreCliente, ApellidoCliente, CedulaCliente, GeneroCliente, DireccionCliente, TelCliente, EmailCliente, FechaRegistro)
VALUES (1, 'Carlos', 'González', '101112', 'M', 'Calle Falsa 123', '555-0100', 'carlos@boxnova.com', '2024-01-01'),
       (2, 'María', 'Pérez', '202324', 'F', 'Av. Real 456', '555-0200', 'maria@boxnova.com', '2024-02-01'),
       (3, 'Luis', 'Martínez', '303436', 'M', 'Plaza Central 789', '555-0300', 'luis@boxnova.com', '2024-03-01');

-- ========================================
-- Insertar en la tabla Venta
-- ========================================
INSERT INTO Venta (VentaId, Fecha, Total, ClienteId, Estado)
VALUES (1, '2024-12-01', 500.00, 1, 'Completado'),
       (2, '2024-12-02', 300.00, 2, 'Pendiente'),
       (3, '2024-12-03', 700.00, 3, 'Cancelado');

-- ========================================
-- Insertar en la tabla DetalleVenta
-- ========================================
INSERT INTO DetalleVenta (DetalleVentaId, VentaId, ProductoId, Cantidad, Precio, SubTotal)
VALUES (1, 1, 1, 2, 50.00, 100.00),
       (2, 1, 2, 1, 75.00, 75.00),
       (3, 2, 3, 3, 120.00, 360.00),
       (4, 3, 4, 4, 80.00, 320.00);

-- ========================================
-- Insertar en la tabla Pedido
-- ========================================
INSERT INTO Pedido (IdPedido, IdCliente, MontoTotal, FechaDelPedido, EstadoPedido)
VALUES (1, 1, 500.00, '2024-12-01', 'Pendiente'),
       (2, 2, 300.00, '2024-12-02', 'Completado'),
       (3, 3, 700.00, '2024-12-03', 'Fallido');
       (3, 3, 3, 1, 120.00, 120.00);


-- Insertar en la tabla DetallePedido (continuación)
-- ========================================
INSERT INTO DetallePedido (IdDetPedido, IdPedido, IdProducto, Cantidad, PrecUnitario, Subtotal)
VALUES (7, 7, 5, 1, 80.00, 80.00),
       (8, 8, 4, 2, 35.00, 70.00),
       (9, 9, 3, 3, 60.00, 180.00);