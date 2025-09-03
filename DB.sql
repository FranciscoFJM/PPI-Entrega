IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'PPI')
BEGIN
    CREATE DATABASE PPI
END
GO
USE PPI
CREATE TABLE Orders(
    Id int identity(1,1) primary key,
    AccountId int not null,
    Quantity bigint not null,
    Price decimal (16,2) not null,
    OperationTypeId nvarchar(1) not null,
    StatusId Int not null,
    AssetId int not null,
    TotalAmount decimal (16,4) not null
)


CREATE TABLE AssetTypes (
    Id int primary key,
    Description nvarchar(100) NOT NULL,
);


CREATE TABLE Assets (
    Id int primary key,
    Ticker nvarchar(100) NOT NULL,
    Name nvarchar(100) NOT NULL,
    UnitPrice decimal(16, 2) NOT NULL,
    AssetTypeId int NOT NULL
);


CREATE TABLE OrderStates (
    Id int primary key,
    Description nvarchar(100) NOT NULL,
);


CREATE TABLE OperationTypes (
    Id nvarchar(1) primary key,
    Description nvarchar(100) NOT NULL,
);


ALTER TABLE Assets
ADD CONSTRAINT FK_Assets_AssetTypes FOREIGN KEY (AssetTypeId) REFERENCES AssetTypes (Id)

ALTER TABLE Orders
ADD CONSTRAINT FK_OrdersByOperationTypes FOREIGN KEY (OperationTypeId) REFERENCES OperationTypes (Id)

ALTER TABLE Orders
ADD CONSTRAINT FK_OrdersByOrderStates FOREIGN KEY (StatusId) REFERENCES OrderStates (Id)

ALTER TABLE Orders
ADD CONSTRAINT FK_OrdersByAssets FOREIGN KEY (AssetId) REFERENCES Assets (Id)

IF NOT EXISTS (SELECT 1 FROM AssetTypes)
BEGIN
    INSERT INTO AssetTypes (Id, Description) VALUES (1, N'Acción');
    INSERT INTO AssetTypes (Id, Description) VALUES (2, N'Bono');
    INSERT INTO AssetTypes (Id, Description) VALUES (3, N'FCI');
END

IF NOT EXISTS (SELECT 1 FROM Assets)
BEGIN
    INSERT INTO Assets (Id, Ticker, Name, UnitPrice, AssetTypeId) VALUES (1, N'AAPL', N'Apple', 177.97, 1);
    INSERT INTO Assets (Id, Ticker, Name, UnitPrice, AssetTypeId) VALUES (2, N'GOOGL', N'Alphabet Inc', 138.21, 1);
    INSERT INTO Assets (Id, Ticker, Name, UnitPrice, AssetTypeId) VALUES (3, N'MSFT', N'Microsoft', 329.04, 1);
    INSERT INTO Assets (Id, Ticker, Name, UnitPrice, AssetTypeId) VALUES (4, N'KO', N'Coca Cola', 58.3, 1);
    INSERT INTO Assets (Id, Ticker, Name, UnitPrice, AssetTypeId) VALUES (5, N'WMT', N'Walmart', 163.42, 1);
    INSERT INTO Assets (Id, Ticker, Name, UnitPrice, AssetTypeId) VALUES (6, N'AL30', N'BONOS ARGENTINA USD 2030 L.A', 307.4, 2);
    INSERT INTO Assets (Id, Ticker, Name, UnitPrice, AssetTypeId) VALUES (7, N'GD30', N'Bonos Globales Argentina USD Step Up 2030', 336.1, 2);
    INSERT INTO Assets (Id, Ticker, Name, UnitPrice, AssetTypeId) VALUES (8, N'Delta.Pesos', N'Delta Pesos Class A', 0.0181, 3);
    INSERT INTO Assets (Id, Ticker, Name, UnitPrice, AssetTypeId) VALUES (9, N'Fima.Premium', N'Fima Premium Clase A', 0.0317, 3);
END

IF NOT EXISTS (SELECT 1 FROM OrderStates)
BEGIN
    INSERT INTO OrderStates (Id, Description) VALUES (0, N'En proceso');
    INSERT INTO OrderStates (Id, Description) VALUES (1, N'Ejecutada');
    INSERT INTO OrderStates (Id, Description) VALUES (3, N'Cancelada');
END

IF NOT EXISTS (SELECT 1 FROM OperationTypes)
BEGIN
    INSERT INTO OperationTypes (Id, Description) VALUES ('C', N'Compra');
    INSERT INTO OperationTypes(Id, Description) VALUES ('V', N'Venta');
END