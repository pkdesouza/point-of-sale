DROP DATABASE IF EXISTS pointOfSale;
GO
CREATE DATABASE pointOfSale;
GO
USE pointOfSale;
GO 
PRINT('CREATE Bill')
CREATE TABLE Bill(
	Id INT IDENTITY(1,1),
	Value MONEY NOT NULL,
	PRIMARY KEY(Id)
);
GO;
PRINT('CREATE COIN')
CREATE TABLE Coin(
	Id INT IDENTITY(1,1),
	Value MONEY NOT NULL,
	PRIMARY KEY(Id)
);
GO;

INSERT INTO Bill VALUES(100.0,50.0,20.0,10.0);
INSERT INTO COIN VALUES(50.0,10.0,5.0,1.0);
