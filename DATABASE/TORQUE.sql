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
    ClienteID INT IDENTITY(1,1) PRIMARY KEY,
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