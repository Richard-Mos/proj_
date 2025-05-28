CREATE DATABASE [DEMV5];
GO

USE [DEMV5];


CREATE TABLE [ProductTypes]
(
	[ProductTypeId] INT PRIMARY KEY,
	[ProductTypeName] NVARCHAR(20) NOT NULL,
	[ProductTypeRate] DECIMAL(18, 2) DEFAULT 0
);

CREATE TABLE [Products]
(
	[ProductId] INT PRIMARY KEY,
	[ProductTypeId] INT NOT NULL,
	[ProductName] NVARCHAR(150) NOT NULL,
	[ProductLong] INT,
	[ProductWidth] INT,
	[ProductHeight] INT,
	[ProductArticle] NVARCHAR(30) NOT NULL,
	[ProductMinPrice] DECIMAL(18, 2) DEFAULT 0,
	FOREIGN KEY([ProductTypeId]) REFERENCES [ProductTypes]([ProductTypeId])
);

CREATE TABLE [MaterialTypes]
(
	[MaterialTypeId] INT PRIMARY KEY,
	[MaterialTypeName] NVARCHAR(30) NOT NULL,
	[MaterialTypeDefectRate] DECIMAL(18, 2) DEFAULT 0
);

CREATE TABLE [UnitTypes]
(
	[UnitTypeId] INT PRIMARY KEY,
	[UnitTypeName] NVARCHAR(30) NOT NULL,
);

CREATE TABLE [Materials]
(
	[MaterialId] INT PRIMARY KEY,
	[MaterialName] NVARCHAR(150) NOT NULL,
	[MaterialTypeId] INT NOT NULL,
	[MaterialPrice] DECIMAL(18, 2) DEFAULT 0,
	[MaterialStorageAmount] DECIMAL(18, 2) DEFAULT 0,
	[MaterialMinAmount] DECIMAL(18, 2) DEFAULT 0,
	[MaterialPackAmount] DECIMAL(18, 2) DEFAULT 0,
	[UnitTypeId] INT NOT NULL,
	FOREIGN KEY([MaterialTypeId]) REFERENCES [MaterialTypes]([MaterialTypeId]),
	FOREIGN KEY([UnitTypeId]) REFERENCES [UnitTypes]([UnitTypeId])
);

CREATE TABLE [MaterialProducts]
(
	[MaterialProductId] INT PRIMARY KEY,
	[MaterialId] INT NOT NULL,
	[ProductId] INT NOT NULL,
	[MaterialAmount] DECIMAL(18, 2) DEFAULT 0,
	FOREIGN KEY([MaterialId]) REFERENCES [Materials]([MaterialId]),
	FOREIGN KEY([ProductId]) REFERENCES [Products]([ProductId])
);