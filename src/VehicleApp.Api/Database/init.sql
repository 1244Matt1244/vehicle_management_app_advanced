IF DB_ID('VehicleApp') IS NULL
BEGIN
    CREATE DATABASE VehicleApp;
END
GO

USE VehicleApp;
GO

IF OBJECT_ID('dbo.VehicleMakes', 'U') IS NULL
BEGIN
    CREATE TABLE VehicleMakes (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Abrv NVARCHAR(10) NOT NULL
    );
END
GO

IF OBJECT_ID('dbo.VehicleModels', 'U') IS NULL
BEGIN
    CREATE TABLE VehicleModels (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        MakeId INT NOT NULL FOREIGN KEY REFERENCES VehicleMakes(Id) ON DELETE CASCADE,
        Name NVARCHAR(100) NOT NULL,
        Abrv NVARCHAR(10) NOT NULL
    );

    CREATE INDEX IX_VehicleModels_MakeId ON VehicleModels(MakeId);
END
GO
