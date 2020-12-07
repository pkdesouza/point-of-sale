# Projeto Ponto de Venda

## Objetivo
objetivo do projeto contempla em resolver a seguinte situação do cotidiano dos operadores de pontos de venda (PDV): <br><br>
Esses profissionais tem grande responsabilidade em suas mãos e a maior parte do seu tempo é gasto recebendo valores de clientes e, em alguns casos, fornecendo troco. <br> <br>
Seu desafio é criar uma API que leia o valor total a ser pago e o valor efetivamente pago pelo cliente, em seguida informe o menor número de cédulas e moedas que devem ser fornecidas como troco.

## Regras
* Notas disponíveis: **R$ 10,00 - R$ 20,00 - R$ 50,00 - R$ 100,00**
* Moedas disponíveis: **R$0,01 - R$0,05 - R$0,10 - R$0,50**
* Entregar o menor número de notas possíveis

Exemplo:<br><br>
```Valor do Troco: R$ 30,00```<br>
```Resultado Esperado: Entregar 1 nota de R$20,00 e 1 nota de R$10,00```<br><br>
```Valor do Troco: R$ 80,00```<br>
```Resultado Esperado: Entregar 1 nota de R$50,00, 1 nota de R$20,00 e 1 nota de R$10,00```<br>

## Configurações 
Para habilitar ou desabilitar alguma configuração abaixo, é necessário alterar os valores no appsettings da aplicação.
**UseCache** 
```Configuração responsável para adicionar uma camada de cache de 10 minutos ao consumir a base de dados```<br>
**UseAuthentication** 
```Configuração responsável para adicionar uma chave de autenticação na aplicação. Enviar no header: SecretKey: q2JeXqc0OA1OHRAO7KEcnrtazZSeZM16X3NT2Kpa```<br>

## Testes 
Aplicação está coberta por testes para manter a integridade da informação. Para executa-los basta ir no projeto PointOfSaleServiceTests e utilizar o Test Explorer. Os testes foram realizados com dados dinamicos e estaticos.

## Como rodar

### Download do repositório
Acesse o github https://github.com/pkdesouza/point-of-sale, após o acesso, selecione a opção de realizar o clone do projeto.
```
https://github.com/pkdesouza/point-of-sale.git
```

Com isso fazemos o download do codigo fonte que precisamos. Estou ASP .NET 3.1.

### Construindo as imagens

Acesse a pasta raíz do projeto(./PointOfSale/) e construa cada imagem (API e MSSQL) pelo powershell ou equivalente:

```
docker build -t point-of-sale-image -f PointOfSale/Dockerfile .
```
```
docker pull mcr.microsoft.com/mssql/server:2019-latest
```

### Rodando os containers
Na pasta raíz do projeto, execute um de cada vez:

```
docker run --name mssql-container -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Pk@senha06' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```
```
docker container run -d --name point-of-sale-api-container -p 3000:80 point-of-sale-image
```

### Agora faça o restore do banco:
```
docker exec -it mssql-container /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Pk@senha06  -Q "
USE [master]
GO
CREATE DATABASE [pointofsale]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'pointofsale', FILENAME = N'/var/opt/mssql/data/pointofsale.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'pointofsale_log', FILENAME = N'/var/opt/mssql/data/pointofsale_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [pointofsale] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [pointofsale].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [pointofsale] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [pointofsale] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [pointofsale] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [pointofsale] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [pointofsale] SET ARITHABORT OFF 
GO
ALTER DATABASE [pointofsale] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [pointofsale] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [pointofsale] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [pointofsale] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [pointofsale] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [pointofsale] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [pointofsale] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [pointofsale] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [pointofsale] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [pointofsale] SET  ENABLE_BROKER 
GO
ALTER DATABASE [pointofsale] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [pointofsale] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [pointofsale] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [pointofsale] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [pointofsale] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [pointofsale] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [pointofsale] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [pointofsale] SET RECOVERY FULL 
GO
ALTER DATABASE [pointofsale] SET  MULTI_USER 
GO
ALTER DATABASE [pointofsale] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [pointofsale] SET DB_CHAINING OFF 
GO
ALTER DATABASE [pointofsale] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [pointofsale] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [pointofsale] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [pointofsale] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'pointofsale', N'ON'
GO
ALTER DATABASE [pointofsale] SET QUERY_STORE = OFF
GO
USE [pointofsale]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ValueToPay] [money] NOT NULL,
	[TotalValue] [money] NOT NULL,
	[Change] [money] NOT NULL,
	[ChangeMessage] [varchar](255) NOT NULL,
	[RegistrationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Transactions] ADD  DEFAULT (getdate()) FOR [RegistrationDate]
GO
INSERT INTO Bill VALUES(100.0),(50.0),(20.0),(10.0);
INSERT INTO COIN VALUES(50.0),(10.0),(5.0),(1.0);
GO
USE [master]
GO
ALTER DATABASE [pointofsale] SET  READ_WRITE 
GO"

```

