-- Crear la base de datos
CREATE DATABASE TorqueG46;
GO

-- Seleccionar la base de datos recién creada
USE TorqueG46;
GO
select * from Proveedores
-- Tabla Proveedores
CREATE TABLE Proveedores (
    ProveedorID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(15) NULL,
    Email VARCHAR(100)  NULL,
    Direccion VARCHAR(255) NULL
);
GO

-- Tabla Productos
CREATE TABLE Productos (
    ProductoID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion VARCHAR(100) NULL,
    Precio DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL CHECK (Stock >= 0),
    ProveedorID INT NULL,
    CONSTRAINT FK_Productos_Proveedores FOREIGN KEY (ProveedorID) REFERENCES Proveedores(ProveedorID)
);
GO

-- Tabla Clientes
CREATE TABLE Clientes (
    ClienteID VARCHAR(5) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(15) NULL,
    Email VARCHAR(100) NULL,
    Direccion VARCHAR(255) NULL
);
GO

-- Tabla Ventas
CREATE TABLE Ventas (
    VentaID INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATE NOT NULL,
    ClienteID INT NULL,
    Total DECIMAL(10, 2) NOT NULL CHECK (Total >= 0),
    CONSTRAINT FK_Ventas_Clientes FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID)
);
GO

-- Tabla DetallesVenta
CREATE TABLE DetallesVenta (
    DetalleID INT IDENTITY(1,1) PRIMARY KEY,
    VentaID INT NOT NULL,
    ProductoID INT NOT NULL,
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PrecioVenta DECIMAL(10, 2) NOT NULL CHECK (PrecioVenta >= 0),
    CONSTRAINT FK_DetallesVenta_Ventas FOREIGN KEY (VentaID) REFERENCES Ventas(VentaID),
    CONSTRAINT FK_DetallesVenta_Productos FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);
GO

-- Tabla Compras
CREATE TABLE Compras (
    CompraID INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATE NOT NULL,
    ProveedorID INT NULL,
    Total DECIMAL(10, 2) NOT NULL CHECK (Total >= 0),
    CONSTRAINT FK_Compras_Proveedores FOREIGN KEY (ProveedorID) REFERENCES Proveedores(ProveedorID)
);
GO

-- Tabla DetallesCompra
CREATE TABLE DetallesCompra (
    DetalleID INT IDENTITY(1,1) PRIMARY KEY,
    CompraID INT NOT NULL,
    ProductoID INT NOT NULL,
    Cantidad INT NOT NULL CHECK (Cantidad > 0),
    PrecioCompra DECIMAL(10, 2) NOT NULL CHECK (PrecioCompra >= 0),
    CONSTRAINT FK_DetallesCompra_Compras FOREIGN KEY (CompraID) REFERENCES Compras(CompraID),
    CONSTRAINT FK_DetallesCompra_Productos FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID)
);
GO

-- crear registros de ejemplo 

SET IDENTITY_INSERT Proveedores ON;

INSERT INTO Proveedores (ProveedorID, Nombre, Telefono, Email, Direccion)
VALUES 
(1, 'Proveedor A', '123456789', 'proveedorA@example.com', 'Calle 1'),
(2, 'Proveedor B', '987654321', 'proveedorB@example.com', 'Calle 2'),
(3, 'Proveedor C', '456789123', 'proveedorC@example.com', 'Calle 3'),
(4, 'Proveedor D', '654321987', 'proveedorD@example.com', 'Calle 4'),
(5, 'Proveedor E', '321987654', 'proveedorE@example.com', 'Calle 5'),
(6, 'Proveedor F', '789456123', 'proveedorF@example.com', 'Calle 6');

SET IDENTITY_INSERT Proveedores OFF;

select * from Proveedores


-- registros de muestra para productos

SET IDENTITY_INSERT Productos ON;

INSERT INTO Productos (ProductoID, Nombre, Descripcion, Precio, Stock, ProveedorID)
VALUES 
(1, 'Producto 1', 'Descripción del Producto 1', 100.00, 50, 1),
(2, 'Producto 2', 'Descripción del Producto 2', 200.00, 30, 2),
(3, 'Producto 3', 'Descripción del Producto 3', 150.00, 20, 1),
(4, 'Producto 4', 'Descripción del Producto 4', 250.00, 10, 3),
(5, 'Producto 5', 'Descripción del Producto 5', 175.00, 25, 4),
(6, 'Producto 6', 'Descripción del Producto 6', 225.00, 15, 5);

SET IDENTITY_INSERT Productos OFF;

select * from Productos


-- registros de muestra para ventas

SET IDENTITY_INSERT Ventas ON;

INSERT INTO Ventas (VentaID, Fecha, ClienteID, Total)
VALUES 
(1, '2023-06-01', 'C001', 500.00),
(2, '2023-06-02', 'C002', 300.00),
(3, '2023-06-03', 'C003', 200.00),
(4, '2023-06-04', 'C004', 400.00),
(5, '2023-06-05', 'C005', 350.00),
(6, '2023-06-06', 'C006', 450.00);

SET IDENTITY_INSERT Ventas OFF;
GO

SET IDENTITY_INSERT Compras ON;
INSERT INTO Compras (CompraID, Fecha, ProveedorID, Total)
VALUES 
(1, '2023-05-01', 1, 300.00),
(2, '2023-05-02', 2, 200.00),
(3, '2023-05-03', 3, 400.00),
(4, '2023-05-04', 4, 250.00),
(5, '2023-05-05', 5, 350.00),
(6, '2023-05-06', 6, 450.00);
SET IDENTITY_INSERT Compras OFF;
GO
