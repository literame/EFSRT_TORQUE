-- Crear la base de datos
CREATE DATABASE TorqueG64;
GO

-- Seleccionar la base de datos recién creada
USE TorqueG64;
GO

--TABLAS

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

-- Crear la tabla Clientes sin la columna de identidad para ClienteID
CREATE TABLE Clientes (
    ClienteID VARCHAR(5) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Telefono VARCHAR(15) NULL,
    Email VARCHAR(100) UNIQUE NULL,
    Direccion VARCHAR(255) NULL
);
GO

---Table Ventas

CREATE TABLE Ventas (
    VentaID INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATE NOT NULL,
    ClienteID VARCHAR(5) NULL,
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

-- Crear la tabla Compras
CREATE TABLE Compras (
    CompraID INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATE NOT NULL,
    ProveedorID INT NULL,
    Total DECIMAL(10, 2) NOT NULL CHECK (Total >= 0),
    CONSTRAINT FK_Compras_Proveedores FOREIGN KEY (ProveedorID) REFERENCES Proveedores(ProveedorID)
);
GO

-- Crear la tabla DetallesCompra
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

-- ingresar registros para cada tabla

-- Insertar registros en la tabla Proveedores
SET IDENTITY_INSERT Proveedores ON;
INSERT INTO Proveedores (ProveedorID,Nombre, Telefono, Email, Direccion)
VALUES 
(1, 'Proveedor A', '123456789', 'proveedorA@example.com', 'Calle 1'),
(2, 'Proveedor B', '987654321', 'proveedorB@example.com', 'Calle 2'),
(3, 'Proveedor C', '456789123', 'proveedorC@example.com', 'Calle 3'),
(4, 'Proveedor D', '654321987', 'proveedorD@example.com', 'Calle 4'),
(5, 'Proveedor E', '321987654', 'proveedorE@example.com', 'Calle 5'),
(6, 'Proveedor F', '789456123', 'proveedorF@example.com', 'Calle 6');
SET IDENTITY_INSERT Proveedores OFF;
GO

SELECT * FROM Proveedores

-- Insertar registros en la tabla Productos
SET IDENTITY_INSERT Productos ON;
INSERT INTO Productos (ProductoID, Descripcion, Precio, Stock, ProveedorID)
VALUES 
(1, 'Descripción del Producto 1', 100.00, 50, 1),
(2, 'Descripción del Producto 2', 200.00, 30, 2),
(3, 'Descripción del Producto 3', 150.00, 20, 1),
(4, 'Descripción del Producto 4', 250.00, 10, 3),
(5, 'Descripción del Producto 5', 175.00, 25, 4),
(6, 'Descripción del Producto 6', 225.00, 15, 5);
SET IDENTITY_INSERT Productos OFF;
GO

select * from Productos


-- Insertar registros en la tabla Clientes
INSERT INTO Clientes (ClienteID, Nombre, Telefono, Email, Direccion)
VALUES 
('C001', 'Cliente 1', '123123123', 'cliente1@example.com', 'Dirección 1'),
('C002', 'Cliente 2', '321321321', 'cliente2@example.com', 'Dirección 2'),
('C003', 'Cliente 3', '456456456', 'cliente3@example.com', 'Dirección 3'),
('C004', 'Cliente 4', '654654654', 'cliente4@example.com', 'Dirección 4'),
('C005', 'Cliente 5', '789789789', 'cliente5@example.com', 'Dirección 5'),
('C006', 'Cliente 6', '987987987', 'cliente6@example.com', 'Dirección 6');

select * from Clientes

-- Insertar registros en la tabla Ventas
INSERT INTO Ventas (Fecha, ClienteID, Total)
VALUES 
('2024-06-01', 'C001', 500.00),
('2024-06-02', 'C002', 300.00),
('2024-06-03', 'C003', 200.00),
('2024-06-04', 'C004', 400.00),
('2024-06-05', 'C005', 350.00),
('2024-06-06', 'C006', 450.00);

select * from Ventas

-- Insertar registros en la tabla DetallesVenta
INSERT INTO DetallesVenta (VentaID, ProductoID, Cantidad, PrecioVenta)
VALUES 
(1, 1, 2, 100.00),
(1, 2, 1, 200.00),
(2, 3, 1, 150.00),
(3, 4, 1, 250.00),
(4, 5, 2, 175.00),
(5, 6, 3, 225.00);

select * from DetallesVenta

-- Insertar registros en la tabla Compras
INSERT INTO Compras (Fecha, ProveedorID, Total)
VALUES 
('2024-06-01', 1, 300.00),
('2024-06-02', 2, 200.00),
('2024-06-03', 3, 400.00),
('2024-06-04', 4, 250.00),
('2024-06-05', 5, 350.00),
('2024-06-06', 6, 450.00);

select * from Compras

-- Insertar registros en la tabla DetallesCompra
INSERT INTO DetallesCompra (CompraID, ProductoID, Cantidad, PrecioCompra)
VALUES 
(1, 1, 2, 80.00),
(1, 2, 1, 150.00),
(2, 3, 1, 120.00),
(3, 4, 1, 200.00),
(4, 5, 2, 130.00),
(5, 6, 3, 180.00);

select * from DetallesCompra






CREATE PROCEDURE usp_agregarCliente
    @clienteId NVARCHAR(50),
    @nombre NVARCHAR(100),
    @telefono NVARCHAR(20),
    @email NVARCHAR(100),
    @direccion NVARCHAR(200)
AS
BEGIN
    INSERT INTO Clientes (ClienteID, Nombre, Telefono, Email, Direccion)
    VALUES (@clienteId, @nombre, @telefono, @email, @direccion);
END