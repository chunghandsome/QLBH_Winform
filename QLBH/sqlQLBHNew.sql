USE [master]
GO
/****** Object:  Database [QLBH]    Script Date: 6/1/2020 7:06:44 PM ******/
CREATE DATABASE [QLBH]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLBH', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\QLBH.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QLBH_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\QLBH_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QLBH] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLBH].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLBH] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLBH] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLBH] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLBH] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLBH] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLBH] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QLBH] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLBH] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLBH] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLBH] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLBH] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLBH] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLBH] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLBH] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLBH] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QLBH] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLBH] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLBH] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLBH] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLBH] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLBH] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLBH] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLBH] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLBH] SET  MULTI_USER 
GO
ALTER DATABASE [QLBH] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLBH] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLBH] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLBH] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QLBH] SET DELAYED_DURABILITY = DISABLED 
GO
USE [QLBH]
GO
/****** Object:  Table [dbo].[Attribute]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attribute](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[value] [nvarchar](225) NULL,
	[type] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bill]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[total] [float] NULL,
	[maHoaDon] [nvarchar](255) NULL,
	[userPhone] [varchar](255) NULL,
	[create_at] [timestamp] NOT NULL,
	[adminPhone] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[BillDetail]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillDetail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NULL,
	[bill_id] [int] NULL,
	[quantity] [int] NOT NULL,
	[created_at] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](225) NULL,
	[created_at] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](225) NULL,
	[cat_id] [int] NULL,
	[code] [varchar](50) NULL,
	[price] [float] NULL,
	[image] [varchar](225) NULL,
	[quantity] [int] NULL,
	[created_at] [timestamp] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[productAttr]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productAttr](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[product_id] [int] NULL,
	[attr_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](225) NULL,
	[phone] [varchar](50) NULL,
	[email] [varchar](225) NULL,
	[password] [varchar](225) NULL,
	[role] [tinyint] NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD FOREIGN KEY([bill_id])
REFERENCES [dbo].[Bill] ([id])
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD FOREIGN KEY([product_id])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([cat_id])
REFERENCES [dbo].[Category] ([id])
GO
ALTER TABLE [dbo].[productAttr]  WITH CHECK ADD FOREIGN KEY([attr_id])
REFERENCES [dbo].[Attribute] ([id])
GO
ALTER TABLE [dbo].[productAttr]  WITH CHECK ADD FOREIGN KEY([product_id])
REFERENCES [dbo].[Product] ([id])
GO
/****** Object:  StoredProcedure [dbo].[createAttr]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[createAttr]
(
@type int ,
@value varchar(225)
)as
BEGIN
insert into Attribute(type,value) values(@type,@value)
END;
GO
/****** Object:  StoredProcedure [dbo].[getAttrByProductId]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[getAttrByProductId]
(
@id int 
)
as
begin

select Attribute.value , Attribute.type from Attribute 
join productAttr on Attribute.id = productAttr.attr_id where productAttr.product_id = @id 

END
GO
/****** Object:  StoredProcedure [dbo].[getBillDetail]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[getBillDetail](
@BillId int
)
as
begin
select BillDetail.id, Product.code ,Product.name ,Category.Name as "Mame cate", Product.price, BillDetail.quantity from Product
join Category on Product.cat_id = Category.id
join BillDetail on Product.id = BillDetail.product_id
where BillDetail.bill_id = @BillId
END
GO
/****** Object:  StoredProcedure [dbo].[getBillDetailbyBill]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[getBillDetailbyBill](
@BillId int
)
as
begin
select Product.code ,Product.name ,Category.Name, Product.price, BillDetail.quantity from Product
join Category on Product.cat_id = Category.id
join BillDetail on Product.id = BillDetail.product_id
END
GO
/****** Object:  StoredProcedure [dbo].[getBillDetailbyBill1]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[getBillDetailbyBill1](
@BillId int
)
as
begin
select BillDetail.id, Product.code ,Product.name ,Category.Name as "Mame cate", Product.price, BillDetail.quantity from Product
join Category on Product.cat_id = Category.id
join BillDetail on Product.id = BillDetail.product_id
where BillDetail.bill_id = @BillId
END

GO
/****** Object:  StoredProcedure [dbo].[getInfoToEditBillDetail]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROc [dbo].[getInfoToEditBillDetail] 
(
@id int
)
as
begin
select Product.name , Product.price , Category.Name , Product.code , BillDetail.quantity , BillDetail.id from Product
join Category on Product.cat_id = Category.id
join BillDetail on Product.id = BillDetail.product_id
where BillDetail.id = @id
end

GO
/****** Object:  StoredProcedure [dbo].[getInfoToEditBillDetail1]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROc [dbo].[getInfoToEditBillDetail1] 
(
@id int
)
as
begin
select Product.name , Product.price , Category.Name as "NameCate" , Product.code , BillDetail.quantity , BillDetail.id from Product
join Category on Product.cat_id = Category.id
join BillDetail on Product.id = BillDetail.product_id
where BillDetail.id = @id
end

GO
/****** Object:  StoredProcedure [dbo].[GetInfoToEditBillDetail2]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetInfoToEditBillDetail2]
(
@idHoaDon int
)
as
begin
 select DISTINCT Bill.maHoaDon , Bill.userPhone , Bill.adminPhone from Bill join 
 BillDetail on BillDetail.bill_id = Bill.id 
where BillDetail.id = @idHoaDon
end
GO
/****** Object:  StoredProcedure [dbo].[getInfoToEditBillDetail3]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROc [dbo].[getInfoToEditBillDetail3]
(
@id int
)
as
begin
select Product.name , Product.price , Category.Name as "NameCate" , Product.code , BillDetail.quantity , BillDetail.id from Product
join Category on Product.cat_id = Category.id
join BillDetail on Product.id = BillDetail.product_id
where BillDetail.id = @id
end
GO
/****** Object:  StoredProcedure [dbo].[getPrice]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[getPrice](
@code int

)
as
declare @price int
begin
select BillDetail.quantity  from Product join BillDetail on Product.id = BillDetail.product_id where Product.code = @code

end

GO
/****** Object:  StoredProcedure [dbo].[getPrice1]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[getPrice1](
@code int

)
as
declare @price int
begin
select Product.price , BillDetail.quantity  from Product join BillDetail on Product.id = BillDetail.product_id 
where Billdetail.bill_id = @code

end

GO
/****** Object:  StoredProcedure [dbo].[getProduct]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[getProduct]
as
begin
select Product.id , Product.code , Product.name , Product.price , Category.Name as "Loai san pham", Product.quantity , Product.image from Product
join Category on Product.cat_id = Category.id
END
GO
/****** Object:  StoredProcedure [dbo].[getProductByCode]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[getProductByCode]
(
@code int 
)

as
begin
select Product.id as "Id sản phẩm", Product.name as "Tên sản phẩm" , Product.price as "Giá sản phẩm",Product.code as "Mã sản phẩm" , Category.Name as "Tên danh mục" ,Product.image as "Ảnh sản phẩm" from Product
join Category on Product.cat_id = Category.id  where Product.code = @code

END
GO
/****** Object:  StoredProcedure [dbo].[getTotal]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[getTotal](
@code int

)
as
declare @price int
begin
select Product.price from Product join BillDetail on Product.id = BillDetail.product_id where Product.code = @code

end
GO
/****** Object:  StoredProcedure [dbo].[searchByCode]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[searchByCode](
@Code int 
)
as
begin
 select  Product.name , Product.price , Category.Name as "Loai san pham", Product.quantity , Product.image from Product 
 join Category on Product.cat_id = Category.id
 where  Product.code = @Code
end

GO
/****** Object:  StoredProcedure [dbo].[selectProduct]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[selectProduct]
AS
	select * from Product
GO;
GO
/****** Object:  StoredProcedure [dbo].[selectProductcate]    Script Date: 6/1/2020 7:06:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[selectProductcate]
as
begin
select Product.id , Product.code , Product.name , Product.price , Category.Name as "Loai san pham", Product.quantity , Product.image from Product
join Category on Product.cat_id = Category.id
END

GO
USE [master]
GO
ALTER DATABASE [QLBH] SET  READ_WRITE 
GO
