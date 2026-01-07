USE [master]
GO
/****** Object:  Database [farm2homeDB]    Script Date: 1/7/2026 4:20:53 PM ******/
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'farm2homeDB')
DROP DATABASE [farm2homeDB]
GO
CREATE DATABASE [farm2homeDB]
GO
ALTER DATABASE [farm2homeDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [farm2homeDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [farm2homeDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [farm2homeDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [farm2homeDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [farm2homeDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [farm2homeDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [farm2homeDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [farm2homeDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [farm2homeDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [farm2homeDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [farm2homeDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [farm2homeDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [farm2homeDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [farm2homeDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [farm2homeDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [farm2homeDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [farm2homeDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [farm2homeDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [farm2homeDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [farm2homeDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [farm2homeDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [farm2homeDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [farm2homeDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [farm2homeDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [farm2homeDB] SET  MULTI_USER 
GO
ALTER DATABASE [farm2homeDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [farm2homeDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [farm2homeDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [farm2homeDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [farm2homeDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [farm2homeDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [farm2homeDB] SET QUERY_STORE = OFF
GO
USE [farm2homeDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItems]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CartId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_CartItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](7, 0) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[IsReviewed] [bit] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[ShipToName] [nvarchar](100) NOT NULL,
	[ShipToAddress] [nvarchar](500) NOT NULL,
	[ShipToPhone] [nvarchar](10) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[TotalAmount] [decimal](9, 0) NOT NULL,
	[DiscountAmount] [decimal](9, 0) NOT NULL,
	[FinalAmount] [decimal](9, 0) NOT NULL,
	[Status] [int] NOT NULL,
	[Channel] [int] NOT NULL,
	[CustomerId] [nvarchar](450) NULL,
	[VoucherId] [int] NULL,
	[PaymentMethod] [int] NOT NULL,
	[IsReviewed] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductBatches]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductBatches](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ImportDate] [date] NOT NULL,
	[ExpireDate] [date] NOT NULL,
	[Quantity] [int] NOT NULL,
	[RemainingQuantity] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_ProductBatches] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductImages]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Price] [decimal](7, 0) NOT NULL,
	[ImageUrl] [nvarchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[CategoryName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [int] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[OrderId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vouchers]    Script Date: 1/7/2026 4:20:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vouchers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[MinimumPrice] [decimal](7, 0) NOT NULL,
	[DícountMax] [decimal](7, 0) NOT NULL,
	[DiscountPecent] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Vouchers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20251128164131_update-datatype', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20251129025832_addCart', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20251130162338_AddCategoryCollection', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20251201005416_categoryName', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20251213051449_UpdateDateTypeForProductBatch', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20251231045315_voucher', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260105145638_review', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20260105153304_review2', N'9.0.0')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1165f3ed-6b4d-4d8f-8f61-f1be856976be', N'Manager', N'MANAGER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'6a052660-2bcd-4641-b87d-9338f1c7b864', N'Staff', N'STAFF', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'b80eb8bb-acd5-403d-8114-d4800a21748d', N'Customer', N'CUSTOMER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'fc0aaec2-8e9f-48f4-826e-765e866448e9', N'Administrator', N'ADMINISTRATOR', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6663f043-e0fd-4ca1-8e1e-b7818eeb10b6', N'1165f3ed-6b4d-4d8f-8f61-f1be856976be')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'510c8c11-c6f2-4d5a-9e82-11e0b2953d98', N'b80eb8bb-acd5-403d-8114-d4800a21748d')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a147c6a9-354a-47fa-89ed-c180a24f9963', N'b80eb8bb-acd5-403d-8114-d4800a21748d')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0c6ab855-bc61-48a4-86bf-54c78f6f7c64', N'fc0aaec2-8e9f-48f4-826e-765e866448e9')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Address], [Gender], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0c6ab855-bc61-48a4-86bf-54c78f6f7c64', N'Quản trị viên', NULL, NULL, N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEBs9ivjZ9+oagNHB4lBecYhhG5q1Y5gQBmVBNlUvtOhESgminDi1QnT5GSVvGu6hrA==', N'FARXBER4CK5VOHV4T2KPGH7JHDRI2OXB', N'8a3d44b8-c182-494f-82da-edbef2609b4f', N'0999999999', 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Address], [Gender], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'510c8c11-c6f2-4d5a-9e82-11e0b2953d98', N'Võ Đình Thiệu', N'abc', NULL, N'thieu@gmail.com', N'THIEU@GMAIL.COM', N'thieu@gmail.com', N'THIEU@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEMjT1mqPkeAZcxbCvnlR2EzG8m0+7Mf4fw3cNZXHWngYg6vN2x3Mk8+egOyqcc/WIQ==', N'27GBAAATMDACYNWZ3HLLKYA2RWDDDMUH', N'1f84a3b0-4500-471b-a80c-e19902c2a00d', N'0971679073', 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Address], [Gender], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'6663f043-e0fd-4ca1-8e1e-b7818eeb10b6', N'', NULL, NULL, N'abc123@gmail.com', N'ABC123@GMAIL.COM', N'abc123@gmail.com', N'ABC123@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEEdGgICVSZePosyK3tcTcs7mJPXcDV2VJ2idefUeUQLSNH8m4OQQWzJUauvAcX2IIQ==', N'UEWVGDYAJSZAFCNHCUFIELS6AVL3AFET', N'0379b17e-1ddd-4dc0-9b0c-f2afab97ec80', NULL, 0, 0, CAST(N'2025-12-20T13:15:35.6023589+00:00' AS DateTimeOffset), 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [FullName], [Address], [Gender], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'a147c6a9-354a-47fa-89ed-c180a24f9963', N'hihi', N'123', NULL, N'thieu@123', N'THIEU@123', N'thieu@123', N'THIEU@123', 0, N'AQAAAAIAAYagAAAAEKczSsWU4hm6lDe1xCNP5Bt/43ElQUUm2TcFNjSvtn2baOpEpsCXw2U9qvyw4j32hw==', N'E2AXCAXNJS7XLDJUYTSOIP67CG65A4GR', N'e34d03ff-4d13-4b3f-ad2d-c906f7122595', N'0908', 0, 0, CAST(N'2025-12-26T04:20:44.5911997+00:00' AS DateTimeOffset), 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name]) VALUES (3, N'Trái cây sấy')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (8, N'Trà')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (9, N'Nông sản')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (10, N'Hạt đặc sản')
INSERT [dbo].[Categories] ([Id], [Name]) VALUES (11, N'Bánh kẹo')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetails] ON 

INSERT [dbo].[OrderDetails] ([Id], [Quantity], [Price], [OrderId], [ProductId], [IsReviewed]) VALUES (1, 1, CAST(105000 AS Decimal(7, 0)), 1, 32, 0)
INSERT [dbo].[OrderDetails] ([Id], [Quantity], [Price], [OrderId], [ProductId], [IsReviewed]) VALUES (2, 1, CAST(131000 AS Decimal(7, 0)), 2, 31, 0)
INSERT [dbo].[OrderDetails] ([Id], [Quantity], [Price], [OrderId], [ProductId], [IsReviewed]) VALUES (3, 1, CAST(131000 AS Decimal(7, 0)), 3, 31, 1)
INSERT [dbo].[OrderDetails] ([Id], [Quantity], [Price], [OrderId], [ProductId], [IsReviewed]) VALUES (4, 1, CAST(76000 AS Decimal(7, 0)), 3, 33, 1)
INSERT [dbo].[OrderDetails] ([Id], [Quantity], [Price], [OrderId], [ProductId], [IsReviewed]) VALUES (5, 12, CAST(131000 AS Decimal(7, 0)), 4, 31, 0)
INSERT [dbo].[OrderDetails] ([Id], [Quantity], [Price], [OrderId], [ProductId], [IsReviewed]) VALUES (6, 11, CAST(105000 AS Decimal(7, 0)), 4, 32, 0)
INSERT [dbo].[OrderDetails] ([Id], [Quantity], [Price], [OrderId], [ProductId], [IsReviewed]) VALUES (7, 1, CAST(88000 AS Decimal(7, 0)), 11, 68, 0)
INSERT [dbo].[OrderDetails] ([Id], [Quantity], [Price], [OrderId], [ProductId], [IsReviewed]) VALUES (8, 1, CAST(100000 AS Decimal(7, 0)), 11, 72, 0)
INSERT [dbo].[OrderDetails] ([Id], [Quantity], [Price], [OrderId], [ProductId], [IsReviewed]) VALUES (9, 3, CAST(131000 AS Decimal(7, 0)), 12, 31, 0)
INSERT [dbo].[OrderDetails] ([Id], [Quantity], [Price], [OrderId], [ProductId], [IsReviewed]) VALUES (10, 5, CAST(131000 AS Decimal(7, 0)), 13, 31, 0)
SET IDENTITY_INSERT [dbo].[OrderDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([Id], [OrderDate], [ShipToName], [ShipToAddress], [ShipToPhone], [Note], [TotalAmount], [DiscountAmount], [FinalAmount], [Status], [Channel], [CustomerId], [VoucherId], [PaymentMethod], [IsReviewed]) VALUES (1, CAST(N'2026-01-05T18:11:42.1530981' AS DateTime2), N'hihi', N'123', N'0908', NULL, CAST(105000 AS Decimal(9, 0)), CAST(0 AS Decimal(9, 0)), CAST(105003 AS Decimal(9, 0)), 4, 0, N'a147c6a9-354a-47fa-89ed-c180a24f9963', NULL, 0, 0)
INSERT [dbo].[Orders] ([Id], [OrderDate], [ShipToName], [ShipToAddress], [ShipToPhone], [Note], [TotalAmount], [DiscountAmount], [FinalAmount], [Status], [Channel], [CustomerId], [VoucherId], [PaymentMethod], [IsReviewed]) VALUES (2, CAST(N'2026-01-05T21:23:55.7935652' AS DateTime2), N'hihi', N'123', N'0908', NULL, CAST(131000 AS Decimal(9, 0)), CAST(0 AS Decimal(9, 0)), CAST(131003 AS Decimal(9, 0)), 3, 0, N'a147c6a9-354a-47fa-89ed-c180a24f9963', NULL, 0, 0)
INSERT [dbo].[Orders] ([Id], [OrderDate], [ShipToName], [ShipToAddress], [ShipToPhone], [Note], [TotalAmount], [DiscountAmount], [FinalAmount], [Status], [Channel], [CustomerId], [VoucherId], [PaymentMethod], [IsReviewed]) VALUES (3, CAST(N'2026-01-05T22:37:58.9109001' AS DateTime2), N'hihi', N'123', N'0908', NULL, CAST(207000 AS Decimal(9, 0)), CAST(0 AS Decimal(9, 0)), CAST(207003 AS Decimal(9, 0)), 1, 0, N'a147c6a9-354a-47fa-89ed-c180a24f9963', NULL, 0, 0)
INSERT [dbo].[Orders] ([Id], [OrderDate], [ShipToName], [ShipToAddress], [ShipToPhone], [Note], [TotalAmount], [DiscountAmount], [FinalAmount], [Status], [Channel], [CustomerId], [VoucherId], [PaymentMethod], [IsReviewed]) VALUES (4, CAST(N'2026-01-06T22:04:10.4099672' AS DateTime2), N'anh minh', N'Mua tại cửa hàng', N'123', NULL, CAST(2727000 AS Decimal(9, 0)), CAST(0 AS Decimal(9, 0)), CAST(2727000 AS Decimal(9, 0)), 0, 1, NULL, NULL, 1, 0)
INSERT [dbo].[Orders] ([Id], [OrderDate], [ShipToName], [ShipToAddress], [ShipToPhone], [Note], [TotalAmount], [DiscountAmount], [FinalAmount], [Status], [Channel], [CustomerId], [VoucherId], [PaymentMethod], [IsReviewed]) VALUES (11, CAST(N'2026-01-07T09:38:41.3144355' AS DateTime2), N'hihi', N'123', N'0908', NULL, CAST(188000 AS Decimal(9, 0)), CAST(0 AS Decimal(9, 0)), CAST(218000 AS Decimal(9, 0)), 4, 0, N'a147c6a9-354a-47fa-89ed-c180a24f9963', NULL, 0, 0)
INSERT [dbo].[Orders] ([Id], [OrderDate], [ShipToName], [ShipToAddress], [ShipToPhone], [Note], [TotalAmount], [DiscountAmount], [FinalAmount], [Status], [Channel], [CustomerId], [VoucherId], [PaymentMethod], [IsReviewed]) VALUES (12, CAST(N'2026-01-07T09:58:55.1837878' AS DateTime2), N'hihi', N'123', N'0908', NULL, CAST(393000 AS Decimal(9, 0)), CAST(0 AS Decimal(9, 0)), CAST(423000 AS Decimal(9, 0)), 4, 0, N'a147c6a9-354a-47fa-89ed-c180a24f9963', NULL, 0, 0)
INSERT [dbo].[Orders] ([Id], [OrderDate], [ShipToName], [ShipToAddress], [ShipToPhone], [Note], [TotalAmount], [DiscountAmount], [FinalAmount], [Status], [Channel], [CustomerId], [VoucherId], [PaymentMethod], [IsReviewed]) VALUES (13, CAST(N'2026-01-07T10:22:43.5073849' AS DateTime2), N'hihi', N'123', N'0908', NULL, CAST(655000 AS Decimal(9, 0)), CAST(0 AS Decimal(9, 0)), CAST(685000 AS Decimal(9, 0)), 0, 0, N'a147c6a9-354a-47fa-89ed-c180a24f9963', NULL, 0, 0)
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductBatches] ON 

INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (4, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 32)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (5, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 33)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (6, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 36)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (7, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 37)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (8, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 38)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (9, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 41)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (10, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 55)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (11, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 66)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (12, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 67)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (13, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 68)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (14, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 69)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (15, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 70)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (16, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 71)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (17, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 72)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (18, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 73)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (19, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 87)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (20, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 89)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (21, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 91)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (22, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 92)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (23, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 97)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (24, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 98)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (25, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 106)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (26, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 108)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (27, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 121)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (28, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 122)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (29, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 123)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (30, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 124)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (31, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 127)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (32, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 128)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (33, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 130)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (34, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 131)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (35, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 133)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (36, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 135)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (37, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 136)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (38, CAST(N'2026-01-01' AS Date), CAST(N'2026-07-01' AS Date), 50, 50, 137)
INSERT [dbo].[ProductBatches] ([Id], [ImportDate], [ExpireDate], [Quantity], [RemainingQuantity], [ProductId]) VALUES (39, CAST(N'2026-01-01' AS Date), CAST(N'2026-10-08' AS Date), 20, 20, 31)
SET IDENTITY_INSERT [dbo].[ProductBatches] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (31, N'Cà phê bột, đặc sản Langfarm', N'Cà phê bột, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(131000 AS Decimal(7, 0)), N'1727754774133_Ca_phe_bot_dac_san_Langfarm___00001_L.jpg', 1, 11, N'Bánh kẹo')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (32, N'Cà phê nguyên hạt đặc sản Langfarm', N'Cà phê nguyên hạt đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(105000 AS Decimal(7, 0)), N'1761620702103_CP7A9295_L.jpg', 1, 11, N'Bánh kẹo')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (33, N'Hồng trà, đặc sản Langfarm', N'Hồng trà, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(76000 AS Decimal(7, 0)), N'1727765742158_Hong_tra_dac_san_Langfarm___00001_L.jpg', 1, 11, N'Bánh kẹo')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (36, N'Trà atisô túi lọc, thảo mộc Thái Bảo x Langfarm', N'Trà atisô túi lọc, thảo mộc Thái Bảo x Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(72000 AS Decimal(7, 0)), N'1727766407640_Tra_atiso_tui_loc_thao_moc_Thai_Bao_x_Langfarm___00001_L.jpg', 1, 8, N'Trà')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (37, N'Trà cỏ ngọt túi lọc, thảo mộc Thái Bảo x Langfarm', N'Trà cỏ ngọt túi lọc, thảo mộc Thái Bảo x Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(62000 AS Decimal(7, 0)), N'1727766458744_Tra_co_ngot_tui_loc_thao_moc_Thai_Bao_x_Langfarm___00001_L.jpg', 1, 8, N'Trà')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (38, N'Trà đắng túi lọc, thảo mộc Thái Bảo x Langfarm', N'Trà đắng túi lọc, thảo mộc Thái Bảo x Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(53000 AS Decimal(7, 0)), N'1727766549234_Tra_dang_tui_loc_thao_moc_Thai_Bao_x_Langfarm___00001_L.jpg', 1, 8, N'Trà')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (41, N'Trà gừng hoà tan, đặc sản Thái Bảo', N'Trà gừng hoà tan, đặc sản Thái Bảo là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(25000 AS Decimal(7, 0)), N'1727765671320_Tra_gung_hoa_tan_dac_san_Thai_Bao_x_Langfarm___00001_L.jpg', 1, 8, N'Trà')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (55, N'Trà sencha hoa cúc, Matchi Matcha x Langfarm', N'Trà sencha hoa cúc, Matchi Matcha x Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(107000 AS Decimal(7, 0)), N'1727765548492_Tra_sencha_hoa_cuc_Matchi_Matcha_x_Langfarm___00001_L.jpg', 1, 8, N'Trà')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (66, N'Bánh biscotti nông sản Đà Lạt, đặc sản Langfarm', N'Bánh biscotti nông sản Đà Lạt, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(113000 AS Decimal(7, 0)), N'1753260259403_CP7A8037_L.jpg', 1, 11, N'Bánh kẹo')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (67, N'Bánh cookie macaron Đà Lạt, đặc sản Langfarm', N'Bánh cookie macaron Đà Lạt, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(102000 AS Decimal(7, 0)), N'1747966099635_1000841___Banh_cookie_macaron_Da_Lat__80g__hu__mau_tobita__Langfarm___06_L.jpg', 1, 11, N'Bánh kẹo')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (68, N'Bánh flan yến sấy thăng hoa, đặc sản Yumsea x Langfarm', N'Bánh flan yến sấy thăng hoa, đặc sản Yumsea x Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(88000 AS Decimal(7, 0)), N'1727697562042_Banh_flan_yen_say_thang_hoa_dac_san_Yumsea_x_Langfarm___00001_L.jpg', 1, 11, N'Bánh kẹo')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (69, N'Bánh hạt đặc sản Langfarm', N'Bánh hạt đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(153000 AS Decimal(7, 0)), N'1751512387680_CP7A5776_L.jpg', 1, 11, N'Bánh kẹo')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (70, N'Bắp rang tỏi ớt đặc sản Yumsea x Langfarm', N'Bắp rang tỏi ớt đặc sản Yumsea x Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(61000 AS Decimal(7, 0)), N'1727689993651_Bap_rang_toi_ot_dac_san_Yumsea_x_Langfarm___00001_L.jpg', 1, 11, N'Bánh kẹo')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (71, N'Bò khô xé sợi ăn liền, đặc sản Yumsea x Langfarm', N'Bò khô xé sợi ăn liền, đặc sản Yumsea x Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(162000 AS Decimal(7, 0)), N'1727758503252_Bo_kho_xe_soi_dac_san_Yumsea_x_Langfarm___00001_L.jpg', 1, 11, N'Bánh kẹo')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (72, N'Cá cơm kho ăn liền, đặc sản Yumsea x Langfarm', N'Cá cơm kho ăn liền, đặc sản Yumsea x Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(100000 AS Decimal(7, 0)), N'1727766285077_Ca_com_kho_an_lien_dac_san_Yumsea_x_Langfarm___00001_L.jpg', 1, 11, N'Bánh kẹo')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (73, N'Cá cơm sấy giòn ăn liền, đặc sản Yumsea x Langfarm', N'Cá cơm sấy giòn ăn liền, đặc sản Yumsea x Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(83000 AS Decimal(7, 0)), N'1727766536916_Ca_com_say_gion_an_lien_dac_san_Yumsea_x_Langfarm___00001_L.jpg', 1, 11, N'Bánh kẹo')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (87, N'Đậu hoà lan wasabi, đặc sản Langfarm', N'Đậu hoà lan wasabi, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(44000 AS Decimal(7, 0)), N'1727750809158_Dau_hoa_lan_wasabi_dac_san_Langfarm___00001_L.jpg', 1, 10, N'Hạt đặc sản')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (89, N'Đậu phộng sấy rau củ, đặc sản Langfarm', N'Đậu phộng sấy rau củ, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(35000 AS Decimal(7, 0)), N'1727750876164_Dau_phong_say_rau_cu_dac_san_Langfarm___00001_L.jpg', 1, 10, N'Hạt đặc sản')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (91, N'Granola siêu hạt đặc sản Langfarm', N'Granola siêu hạt đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(65000 AS Decimal(7, 0)), N'1727750621904_Granola_dac_san_Langfarm___00003_L.jpg', 1, 10, N'Hạt đặc sản')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (92, N'Hạnh nhân rang muối biển, đặc sản Langfarm', N'Hạnh nhân rang muối biển, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(141000 AS Decimal(7, 0)), N'1727750967296_Hanh_nhan_rang_muoi_bien_dac_san_Langfarm___00001_L.jpg', 1, 10, N'Hạt đặc sản')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (97, N'Bông atisô sấy khô, đặc sản Langfarm', N'Bông atisô sấy khô, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(159000 AS Decimal(7, 0)), N'1727752002267_Bong_atiso_say_kho_dac_san_Langfarm___00001_L.jpg', 1, 9, N'Nông sản')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (98, N'Bột bồ công anh, đặc sản Bột Lá x Langfarm', N'Bột bồ công anh, đặc sản Bột Lá x Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(159000 AS Decimal(7, 0)), N'1727751141163_Bot_bo_cong_anh_dac_san_Bot_La_x_Langfarm___00001_L.jpg', 1, 9, N'Nông sản')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (106, N'Cao atisô, đặc sản Langfarm', N'Cao atisô, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(147000 AS Decimal(7, 0)), N'1727751906096_Cao_atiso_thao_moc_Langfarm___00001_L.jpg', 1, 9, N'Nông sản')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (108, N'Combo yến baby thiên nhiên Yumsea x Langfarm', N'Combo yến baby thiên nhiên Yumsea x Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(103000 AS Decimal(7, 0)), N'1727752816873_Combo_yen_baby_thien_nhien_Yumsea_x_Langfarm___00001_L.jpg', 1, 9, N'Nông sản')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (121, N'Bưởi sấy dẻo, đặc sản Langfarm', N'Bưởi sấy dẻo, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(80000 AS Decimal(7, 0)), N'1727692017699_1000854___Buoi_say_deo__200g__hu__mau_tobita__Langfarm___00001_L.jpg', 1, 3, N'Trái cây sấy')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (122, N'Cam sấy dẻo, đặc sản Langfarm', N'Cam sấy dẻo, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(102000 AS Decimal(7, 0)), N'1727691938659_Cam_say_deo_dac_san_Langfarm___00001_L.jpg', 1, 3, N'Trái cây sấy')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (123, N'Chanh dây sấy dẻo, đặc sản Langfarm', N'Chanh dây sấy dẻo, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(94000 AS Decimal(7, 0)), N'1727691895288_Chanh_day_say_deo_dac_san_Langfarm___00001_L.jpg', 1, 3, N'Trái cây sấy')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (124, N'Chuối laba Đà Lạt sấy dẻo, đặc sản Langfarm', N'Chuối laba Đà Lạt sấy dẻo, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(68000 AS Decimal(7, 0)), N'1727692209377_Chuoi_la_ba_Da_Lat_say_deo_dac_san_Langfarm___00001_L.jpg', 1, 3, N'Trái cây sấy')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (127, N'Đu đủ sấy dẻo, đặc sản Langfarm', N'Đu đủ sấy dẻo, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(79000 AS Decimal(7, 0)), N'1727692688079_Du_du_say_deo_dac_san_Langfarm___00001_L.jpg', 1, 3, N'Trái cây sấy')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (128, N'Hồng chén sấy dẻo, đặc sản Langfarm', N'Hồng chén sấy dẻo, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(136000 AS Decimal(7, 0)), N'1727692878586_Hong_chen_say_deo_dac_san_Langfarm___00001_L.jpg', 1, 3, N'Trái cây sấy')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (130, N'Hồng sấy treo, đặc sản Langfarm', N'Hồng sấy treo, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(175000 AS Decimal(7, 0)), N'1727693214818_Hong_say_treo_Nhat_Ban_dac_san_Langfarm___00001_L.jpg', 1, 3, N'Trái cây sấy')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (131, N'Khoai lang mật sấy nguyên củ, đặc sản Langfarm', N'Khoai lang mật sấy nguyên củ, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(108000 AS Decimal(7, 0)), N'1727693407924_Khoai_lang_mat_say_nguyen_cu_dac_san_Langfarm___00001_L.jpg', 1, 3, N'Trái cây sấy')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (133, N'Khoai lang tím sấy giòn, đặc sản Langfarm', N'Khoai lang tím sấy giòn, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(38000 AS Decimal(7, 0)), N'1727756260488_Khoai_lang_tim_say_gion_dac_san_Langfarm___00001_L.jpg', 1, 3, N'Trái cây sấy')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (135, N'Khoai môn sấy giòn, đặc sản Langfarm', N'Khoai môn sấy giòn, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(44000 AS Decimal(7, 0)), N'1727757040768_Khoai_mon_say_gion_dac_san_Langfarm___00001_L.jpg', 1, 3, N'Trái cây sấy')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (136, N'Long nhãn sấy dẻo, đặc sản Langfarm', N'Long nhãn sấy dẻo, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(84000 AS Decimal(7, 0)), N'1727693629410_Long_nhan_say_deo_dac_san_Langfarm___00001_L.jpg', 1, 3, N'Trái cây sấy')
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageUrl], [IsActive], [CategoryId], [CategoryName]) VALUES (137, N'Mận sấy dẻo, đặc sản Langfarm', N'Mận sấy dẻo, đặc sản Langfarm là sản phẩm chất lượng cao, được chọn lọc kỹ lưỡng từ nguồn nguyên liệu tự nhiên, hương vị đặc trưng, phù hợp sử dụng hằng ngày hoặc làm quà tặng.', CAST(82000 AS Decimal(7, 0)), N'1727693769764_Man_say_deo_dac_san_Langfarm___00001_L.jpg', 1, 3, N'Trái cây sấy')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Reviews] ON 

INSERT [dbo].[Reviews] ([Id], [Rating], [Content], [CreatedAt], [OrderId], [UserId], [ProductId]) VALUES (1, 5, N'dddddd', CAST(N'2026-01-05T23:06:03.3206247' AS DateTime2), 3, N'a147c6a9-354a-47fa-89ed-c180a24f9963', 31)
INSERT [dbo].[Reviews] ([Id], [Rating], [Content], [CreatedAt], [OrderId], [UserId], [ProductId]) VALUES (2, 2, N'tốt', CAST(N'2026-01-05T23:06:49.2176042' AS DateTime2), 3, N'a147c6a9-354a-47fa-89ed-c180a24f9963', 33)
SET IDENTITY_INSERT [dbo].[Reviews] OFF
GO
SET IDENTITY_INSERT [dbo].[Vouchers] ON 

INSERT [dbo].[Vouchers] ([Id], [Code], [MinimumPrice], [DícountMax], [DiscountPecent], [Quantity], [StartDate], [EndDate], [IsActive]) VALUES (1, N'CHAOBAN', CAST(1 AS Decimal(7, 0)), CAST(1 AS Decimal(7, 0)), 2, 10, CAST(N'2025-12-02T00:00:00.0000000' AS DateTime2), CAST(N'2026-12-10T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Vouchers] ([Id], [Code], [MinimumPrice], [DícountMax], [DiscountPecent], [Quantity], [StartDate], [EndDate], [IsActive]) VALUES (2, N'Hello', CAST(12 AS Decimal(7, 0)), CAST(12 AS Decimal(7, 0)), 10, 100, CAST(N'2026-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2026-01-31T00:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Vouchers] ([Id], [Code], [MinimumPrice], [DícountMax], [DiscountPecent], [Quantity], [StartDate], [EndDate], [IsActive]) VALUES (3, N'THIEU', CAST(131000 AS Decimal(7, 0)), CAST(20000 AS Decimal(7, 0)), 10, 1, CAST(N'2026-01-03T00:00:00.0000000' AS DateTime2), CAST(N'2026-01-10T00:00:00.0000000' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Vouchers] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CartItems_CartId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_CartItems_CartId] ON [dbo].[CartItems]
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CartItems_ProductId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_CartItems_ProductId] ON [dbo].[CartItems]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Carts_UserId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Carts_UserId] ON [dbo].[Carts]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetails_OrderId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_OrderId] ON [dbo].[OrderDetails]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderDetails_ProductId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_OrderDetails_ProductId] ON [dbo].[OrderDetails]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Orders_CustomerId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_CustomerId] ON [dbo].[Orders]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_VoucherId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_VoucherId] ON [dbo].[Orders]
(
	[VoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductBatches_ProductId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductBatches_ProductId] ON [dbo].[ProductBatches]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductImages_ProductId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_ProductImages_ProductId] ON [dbo].[ProductImages]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_CategoryId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Products_CategoryId] ON [dbo].[Products]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_OrderId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_OrderId] ON [dbo].[Reviews]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_ProductId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_ProductId] ON [dbo].[Reviews]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Reviews_UserId]    Script Date: 1/7/2026 4:20:53 PM ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_UserId] ON [dbo].[Reviews]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [FullName]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsReviewed]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsReviewed]
GO
ALTER TABLE [dbo].[Reviews] ADD  DEFAULT ((0)) FOR [ProductId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CartItems]  WITH NOCHECK ADD  CONSTRAINT [FK_CartItems_Carts_CartId] FOREIGN KEY([CartId])
REFERENCES [dbo].[Carts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartItems] NOCHECK CONSTRAINT [FK_CartItems_Carts_CartId]
GO
ALTER TABLE [dbo].[CartItems]  WITH NOCHECK ADD  CONSTRAINT [FK_CartItems_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartItems] NOCHECK CONSTRAINT [FK_CartItems_Products_ProductId]
GO
ALTER TABLE [dbo].[Carts]  WITH NOCHECK ADD  CONSTRAINT [FK_Carts_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Carts] NOCHECK CONSTRAINT [FK_Carts_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] NOCHECK CONSTRAINT [FK_OrderDetails_Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH NOCHECK ADD  CONSTRAINT [FK_OrderDetails_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] NOCHECK CONSTRAINT [FK_OrderDetails_Products_ProductId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_AspNetUsers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_AspNetUsers_CustomerId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Vouchers_VoucherId] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Vouchers] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Vouchers_VoucherId]
GO
ALTER TABLE [dbo].[ProductBatches]  WITH NOCHECK ADD  CONSTRAINT [FK_ProductBatches_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductBatches] NOCHECK CONSTRAINT [FK_ProductBatches_Products_ProductId]
GO
ALTER TABLE [dbo].[ProductImages]  WITH NOCHECK ADD  CONSTRAINT [FK_ProductImages_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductImages] NOCHECK CONSTRAINT [FK_ProductImages_Products_ProductId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Orders_OrderId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Products_ProductId]
GO
USE [master]
GO
ALTER DATABASE [farm2homeDB] SET  READ_WRITE 
GO
