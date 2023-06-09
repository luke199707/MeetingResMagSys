USE [master]
GO
/****** Object:  Database [MeetingResMagSys]    Script Date: 2021/11/14 10:06:46 ******/
CREATE DATABASE [MeetingResMagSys]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MeetingResMagSys', FILENAME = N'D:\SQLserver2019\MSSQL15.MSSQLSERVER\MSSQL\DATA\MeetingResMagSys.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MeetingResMagSys_log', FILENAME = N'D:\SQLserver2019\MSSQL15.MSSQLSERVER\MSSQL\DATA\MeetingResMagSys_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MeetingResMagSys] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MeetingResMagSys].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MeetingResMagSys] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET ARITHABORT OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MeetingResMagSys] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MeetingResMagSys] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MeetingResMagSys] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MeetingResMagSys] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET RECOVERY FULL 
GO
ALTER DATABASE [MeetingResMagSys] SET  MULTI_USER 
GO
ALTER DATABASE [MeetingResMagSys] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MeetingResMagSys] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MeetingResMagSys] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MeetingResMagSys] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MeetingResMagSys] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MeetingResMagSys', N'ON'
GO
ALTER DATABASE [MeetingResMagSys] SET QUERY_STORE = OFF
GO
USE [MeetingResMagSys]
GO
/****** Object:  Table [dbo].[AllUser]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllUser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [varchar](50) NULL,
	[name] [varchar](30) NULL,
	[pwd] [varchar](50) NULL,
	[organizationId] [varchar](50) NULL,
	[organizationName] [varchar](30) NULL,
	[departmentName] [varchar](30) NULL,
	[Email] [varchar](50) NULL,
	[phone] [varchar](50) NULL,
	[role] [varchar](50) NULL,
	[available] [varchar](10) NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_AllUser] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DataDictionary]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataDictionary](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[parentId] [varchar](50) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[code] [varchar](50) NOT NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_DataDictionary_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[departmentId] [varchar](50) NULL,
	[name] [varchar](50) NULL,
	[introduction] [varchar](max) NULL,
	[superiorDepartment] [varchar](50) NULL,
	[supervisor] [varchar](50) NULL,
	[officeArea] [varchar](50) NULL,
	[type] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[organizationId] [varchar](50) NULL,
	[organizationName] [varchar](30) NULL,
	[time] [datetime] NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FunctionModel]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FunctionModel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[parentId] [varchar](50) NULL,
	[modelName] [varchar](50) NULL,
	[childId] [varchar](50) NULL,
	[currentId] [varchar](50) NULL,
	[url] [varchar](50) NULL,
	[css] [varchar](max) NULL,
	[target] [varchar](50) NULL,
	[time] [datetime] NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_FunctionModel_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingMember]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingMember](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[meetingId] [varchar](50) NULL,
	[userId] [varchar](50) NULL,
	[organizationId] [varchar](50) NULL,
	[organizationName] [varchar](30) NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_MeetingMember] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingReservation]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingReservation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[meetingId] [varchar](50) NULL,
	[title] [varchar](50) NULL,
	[meetingRoom] [varchar](50) NULL,
	[introduction] [varchar](max) NULL,
	[time] [varchar](50) NULL,
	[startTime] [varchar](50) NULL,
	[endTime] [varchar](50) NULL,
	[booker] [varchar](30) NULL,
	[department] [varchar](50) NULL,
	[state] [varchar](20) NULL,
	[reviewer] [varchar](50) NULL,
	[organizationId] [varchar](50) NULL,
	[orderTime] [varchar](50) NULL,
	[remark] [varchar](max) NULL,
	[refuseReason] [varchar](max) NULL,
 CONSTRAINT [PK_MeetingReservation_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingRoom]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingRoom](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[roomId] [varchar](50) NULL,
	[name] [varchar](30) NULL,
	[image] [varchar](50) NULL,
	[officeArea] [varchar](50) NULL,
	[position] [varchar](max) NULL,
	[capacity] [varchar](10) NULL,
	[type] [varchar](50) NULL,
	[introduction] [varchar](max) NULL,
	[facility] [varchar](max) NULL,
	[attention] [varchar](max) NULL,
	[resDepartment] [varchar](50) NULL,
	[director] [varchar](50) NULL,
	[useRole] [varchar](max) NULL,
	[useDepartment] [varchar](50) NULL,
	[available] [varchar](10) NULL,
	[reason] [varchar](max) NULL,
	[organizationId] [varchar](50) NULL,
	[isCheck] [varchar](10) NULL,
	[time] [datetime] NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_MeetingRoom] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingRoomBanDep]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingRoomBanDep](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[roomId] [varchar](50) NULL,
	[departmentId] [varchar](50) NULL,
 CONSTRAINT [PK_RoomBanDep] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingRoomFacility]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingRoomFacility](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[roomId] [varchar](50) NULL,
	[facilityId] [varchar](50) NULL,
 CONSTRAINT [PK_MeetingRoomFacility] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingRoomType]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingRoomType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RoomTypeId] [varchar](30) NULL,
	[name] [varchar](50) NULL,
	[introduction] [varchar](max) NULL,
	[organizationId] [varchar](50) NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_MeetingRoomType_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OfficeArea]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfficeArea](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[officeAreaId] [varchar](50) NULL,
	[name] [varchar](30) NULL,
	[superiorArea] [varchar](50) NULL,
	[address] [varchar](max) NULL,
	[phone] [varchar](50) NULL,
	[serviceDirector] [varchar](50) NULL,
	[introduction] [varchar](50) NULL,
	[organizationId] [varchar](50) NULL,
	[organizationName] [varchar](30) NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_OfficeArea] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[organizationId] [varchar](50) NULL,
	[name] [varchar](50) NULL,
	[introduction] [varchar](max) NULL,
	[logo] [varchar](max) NULL,
	[reseStart] [varchar](50) NULL,
	[reseEnd] [varchar](50) NULL,
	[timeUnit] [varchar](50) NULL,
	[signIn] [varchar](10) NULL,
	[sameTimeAttend] [varchar](10) NULL,
	[time] [datetime] NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_Organization_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[roleId] [varchar](50) NULL,
	[roleName] [varchar](50) NULL,
	[organizationId] [varchar](50) NULL,
	[defaultRole] [varchar](30) NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_Role_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleRight]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleRight](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[roleId] [varchar](50) NULL,
	[roleName] [varchar](50) NULL,
	[rightCode] [varchar](50) NULL,
	[organizationId] [varchar](50) NULL,
	[organizationName] [varchar](30) NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_RoleRight_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomFacility]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomFacility](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[facilityId] [varchar](50) NULL,
	[name] [varchar](50) NULL,
	[introduction] [varchar](max) NULL,
	[organizationId] [varchar](50) NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_RoomFacility] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tenant]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenant](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [varchar](50) NULL,
	[organizationId] [varchar](50) NULL,
	[remark] [varchar](max) NULL,
 CONSTRAINT [PK_Tenant_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AllUser] ON 

INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (1, N'UID20210619000001', N'001', N'123', N'ORG202106220015', NULL, N'无', N'2509824873@qq.com', N'', N'租户', N'可用', N'')
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (2, N'UID20210620000002', N'002', N'002', N'ORG202106220016', NULL, N'无', N'002@qq.com', NULL, N'租户', N'可用', NULL)
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (3, N'UID20210621000003', N'003', N'003', N'ORG202106220016', NULL, N'无', N'003@qq.com', NULL, N'普通用户', N'可用', NULL)
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (4, N'UID20210621000004', N'004', N'004', N'ORG202106290019', NULL, N'无', N'004@qq.com', NULL, N'租户', N'可用', NULL)
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (5, N'UID20210621000005', N'005', N'005', N'ORG202106220016', NULL, N'无', N'005@qq.com', NULL, N'普通用户', N'可用', NULL)
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (6, N'UID20210621000006', N'006', N'006', NULL, NULL, N'无', N'006@qq.com', NULL, N'新用户', N'可用', NULL)
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (8, N'UID20210628000008', N'admin', N'123', N'0', NULL, N'无', N'admin@qq.com', N'123456', N'服务提供商', N'可用', N'系统超级管理员')
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (9, N'UID20210629000009', N'007', N'007', N'ORG202106290018', NULL, N'无', N'007@qq.com', N'111111111', N'租户', N'可用', N'')
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (11, N'UID20210913000010', N'0011', N'0011', N'ORG202106220015', NULL, N'DEP20211023000002', N'1134@qq.com', N'', N'普通用户', N'可用', N'')
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (12, N'UID20210628000011', N'admin2', N'123', N'0', NULL, N'无', N'123@qq.com', N'123456', N'服务提供商', N'可用', N'系统超级管理员')
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (13, N'UID20210916000012', N'916', N'916', N'ORG202109160020', NULL, N'无', N'916@qq.com', NULL, N'租户', N'可用', NULL)
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (14, N'UID20211012000013', N'999', N'999', N'ORG202110120021', NULL, N'无', N'001@001.com', NULL, N'租户', N'可用', NULL)
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (15, N'UID20211012000014', N'888', N'888', N'ORG202110120021', NULL, N'无', N'001@0018.com', NULL, N'普通用户', N'可用', NULL)
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (16, N'UID20211018000015', N'0012', N'0012', N'ORG202106220015', NULL, N'DEP20211023000003', N'111111@qq.com', N'', N'信院财务', N'可用', N'')
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (17, N'UID20211019000016', N'008', N'123', NULL, NULL, N'无', N'008@008.com', NULL, N'新用户', N'可用', NULL)
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (18, N'UID20211019000017', N'009', N'123', NULL, NULL, N'无', N'009@009.com', NULL, N'新用户', N'可用', NULL)
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (19, N'UID20211020000018', N'张三', N'123', N'ORG202110200023', NULL, N'无', N'123@163.com', NULL, N'租户', N'可用', NULL)
INSERT [dbo].[AllUser] ([id], [userId], [name], [pwd], [organizationId], [organizationName], [departmentName], [Email], [phone], [role], [available], [remark]) VALUES (20, N'UID20211026000019', N'李四', N'123', NULL, NULL, N'无', N'123@126.com', NULL, N'新用户', N'可用', NULL)
SET IDENTITY_INSERT [dbo].[AllUser] OFF
SET IDENTITY_INSERT [dbo].[DataDictionary] ON 

INSERT [dbo].[DataDictionary] ([id], [parentId], [name], [code], [remark]) VALUES (1, N'0', N'数据字典根节点', N'00', NULL)
INSERT [dbo].[DataDictionary] ([id], [parentId], [name], [code], [remark]) VALUES (3, N'1', N'会议预订时间间隔', N'001', N'')
INSERT [dbo].[DataDictionary] ([id], [parentId], [name], [code], [remark]) VALUES (4, N'3', N'15', N'001001', N'')
INSERT [dbo].[DataDictionary] ([id], [parentId], [name], [code], [remark]) VALUES (5, N'3', N'30', N'001002', N'')
INSERT [dbo].[DataDictionary] ([id], [parentId], [name], [code], [remark]) VALUES (6, N'3', N'60', N'001003', N'')
SET IDENTITY_INSERT [dbo].[DataDictionary] OFF
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([id], [departmentId], [name], [introduction], [superiorDepartment], [supervisor], [officeArea], [type], [Email], [organizationId], [organizationName], [time], [remark]) VALUES (3, N'DEP20211023000002', N'部门1', N'', N'无', N'UID20210619000001', NULL, NULL, NULL, N'ORG202106220015', NULL, CAST(N'2021-10-23T16:26:11.990' AS DateTime), N'')
INSERT [dbo].[Department] ([id], [departmentId], [name], [introduction], [superiorDepartment], [supervisor], [officeArea], [type], [Email], [organizationId], [organizationName], [time], [remark]) VALUES (4, N'DEP20211023000003', N'部门2', N'', N'无', N'UID20210619000001', NULL, NULL, NULL, N'ORG202106220015', NULL, CAST(N'2021-10-23T16:26:22.113' AS DateTime), N'')
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[FunctionModel] ON 

INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (1, N'0', N'所有模块', N'0', N'1', N'#', NULL, N'right', CAST(N'2021-06-28T19:41:39.677' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (2, N'1', N'租户管理', N'001', N'01', N'#', N'fa fa-group fa-fw', N'right', CAST(N'2021-06-28T19:43:18.877' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (3, N'1', N'系统管理', N'002', N'02', N'#', N'fa fa-cogs fa-fw', N'right', CAST(N'2021-06-28T19:45:04.993' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (4, N'1', N'会议室预订', N'003', N'03', N'#', N'fa fa-hand-pointer-o fa-fw', N'right', CAST(N'2021-06-28T19:46:10.327' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (6, N'1', N'我的群组', N'005', N'05', N'#', N'fa fa-group fa-fw', N'right', CAST(N'2021-06-28T19:47:38.543' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (7, N'1', N'预定统计', N'006', N'06', N'#', N'fa fa-bar-chart fa-fw', N'right', CAST(N'2021-06-28T19:49:16.273' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (8, N'1', N'会议室管理', N'007', N'07', N'#', N'fa fa-building-o fa-fw', N'right', CAST(N'2021-10-22T10:30:55.743' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (9, N'1', N'组织架构', N'008', N'08', N'#', N'fa fa-sitemap fa-fw', N'right', CAST(N'2021-06-28T19:50:49.610' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (10, N'1', N'组织信息管理', N'009', N'09', N'#', N'fa fa-cog fa-fw', N'right', CAST(N'2021-06-28T19:51:36.213' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (11, N'1', N'会议室预订审核', N'010', N'10', N'#', N'fa fa-gavel fa-fw', N'right', CAST(N'2021-06-28T19:52:59.327' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (12, N'1', N'个人资料管理', N'011', N'11', N'#', N'fa fa-user-circle-o fa-fw', N'right', CAST(N'2021-06-28T19:53:35.023' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (27, N'02', N'功能模块管理', N'0002001', N'002001', N'../Pages/ModelSet.aspx', N'', N'right', CAST(N'2021-07-22T19:29:47.507' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (28, N'02', N'默认角色管理', N'0002002', N'002002', N'../Pages/SetDefaultRoleRight.aspx', N'', N'right', CAST(N'2021-07-06T10:24:41.473' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (30, N'07', N'会议室管理', N'0007002', N'007002', N'../Pages/MagMeetRoom.aspx', N'', N'right', CAST(N'2021-07-15T16:14:48.770' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (31, N'11', N'个人资料管理', N'0011001', N'011001', N'../Pages/MagPersonalData.aspx', N'', N'right', CAST(N'2021-07-19T20:47:43.673' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (32, N'11', N'密码修改', N'0011002', N'011002', N'../Pages/MagPwd.aspx', N'', N'right', CAST(N'2021-07-21T20:39:19.680' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (33, N'01', N'租户管理', N'0001001', N'001001', N'../Pages/MagTenant.aspx', N'', N'right', CAST(N'2021-07-26T19:55:09.967' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (34, N'03', N'会议室预订', N'0003001', N'003001', N'../Pages/MeetingOrderByRoom.aspx', N'', N'right', CAST(N'2021-11-01T10:36:25.900' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (36, N'1', N'我的会议', N'004', N'04', N'#', N'fa fa-calendar-check-o fa-fw', N'right', CAST(N'2021-09-13T17:13:27.270' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (37, N'04', N'我的会议', N'0004001', N'004001', N'../Pages/MagMyMeeting.aspx', N'', N'right', CAST(N'2021-09-13T17:18:19.133' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (38, N'07', N'办公区域管理', N'0007003', N'007003', N'../Pages/MagOfficeArea.aspx', N'', N'right', CAST(N'2021-09-18T19:36:00.123' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (39, N'07', N'会议室类型管理', N'0007004', N'007004', N'../Pages/MagMeetRoomType.aspx', N'', N'right', CAST(N'2021-09-18T19:36:36.830' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (40, N'10', N'会议室预订审核', N'0010001', N'010001', N'../Pages/MeetingReview.aspx', N'', N'right', CAST(N'2021-09-26T17:13:00.150' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (41, N'04', N'待审核会议', N'0004002', N'004002', N'../Pages/DisplayUnusualMeeting.aspx', N'', N'right', CAST(N'2021-09-27T19:56:35.087' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (42, N'08', N'部门管理', N'0008001', N'008001', N'../Pages/MagDepartment.aspx', N'', N'right', CAST(N'2021-09-28T20:28:42.403' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (43, N'08', N'成员管理', N'0008002', N'008002', N'../Pages/MagOrgMember.aspx', N'', N'right', CAST(N'2021-09-29T17:04:17.827' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (44, N'08', N'角色管理', N'0008003', N'008003', N'../Pages/MagOrgRole.aspx', N'', N'right', CAST(N'2021-10-07T15:02:12.560' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (45, N'09', N'组织信息管理', N'0009001', N'009001', N'../Pages/MagOrgInfo.aspx', N'', N'right', CAST(N'2021-10-12T18:37:34.320' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (46, N'02', N'数据字典维护', N'0002003', N'002003', N'../Pages/MagDataDic.aspx', N'', N'right', CAST(N'2021-10-18T16:39:12.667' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (47, N'07', N'会议室设施管理', N'0007005', N'007005', N'../Pages/MagRoomFacility.aspx', N'', N'right', CAST(N'2021-10-21T19:37:54.573' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (48, N'06', N'按会议室统计', N'0006001', N'006001', N'../Pages/StatisticsMeetingRoom.aspx', N'', N'right', CAST(N'2021-10-29T17:46:10.860' AS DateTime), NULL)
INSERT [dbo].[FunctionModel] ([id], [parentId], [modelName], [childId], [currentId], [url], [css], [target], [time], [remark]) VALUES (49, N'06', N'按预订人统计', N'0006002', N'006002', N'../Pages/StatisticsBooker.aspx', N'', N'right', CAST(N'2021-10-30T15:02:23.260' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[FunctionModel] OFF
SET IDENTITY_INSERT [dbo].[MeetingMember] ON 

INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (27, N'MRID202111030001', N'UID20210619000001', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (28, N'MRID202111030002', N'UID20210619000001', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (29, N'MRID202111030003', N'UID20210619000001', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (30, N'MRID202111030003', N'UID20211018000015', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (31, N'MRID202111030003', N'UID20210913000010', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (37, N'MRID202111080005', N'UID20210913000010', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (38, N'MRID202111080005', N'UID20211018000015', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (39, N'MRID202111080005', N'UID20210619000001', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (40, N'MRID202111050004', N'UID20210913000010', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (41, N'MRID202111050004', N'UID20211018000015', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (42, N'MRID202111050004', N'UID20210619000001', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (44, N'MRID202111080006', N'UID20210619000001', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (45, N'MRID202111090007', N'UID20210619000001', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (46, N'MRID202111090008', N'UID20210913000010', NULL, NULL, NULL)
INSERT [dbo].[MeetingMember] ([id], [meetingId], [userId], [organizationId], [organizationName], [remark]) VALUES (47, N'MRID202111130009', N'UID20210619000001', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[MeetingMember] OFF
SET IDENTITY_INSERT [dbo].[MeetingReservation] ON 

INSERT [dbo].[MeetingReservation] ([id], [meetingId], [title], [meetingRoom], [introduction], [time], [startTime], [endTime], [booker], [department], [state], [reviewer], [organizationId], [orderTime], [remark], [refuseReason]) VALUES (37, N'MRID202111030001', N'001预订', N'MRID202107150002', N'', N'2021-11-04', N'2021-11-04T08:00', N'2021-11-04T20:00', N'UID20210619000001', NULL, N'正常', NULL, N'ORG202106220015', N'2021/11/3 14:29:17', N'', NULL)
INSERT [dbo].[MeetingReservation] ([id], [meetingId], [title], [meetingRoom], [introduction], [time], [startTime], [endTime], [booker], [department], [state], [reviewer], [organizationId], [orderTime], [remark], [refuseReason]) VALUES (38, N'MRID202111030002', N'测试', N'MRID202107150002', N'', N'2021-11-02', N'2021-11-02T09:00', N'2021-11-02T10:00', N'UID20210619000001', NULL, N'正常', NULL, N'ORG202106220015', N'2021/11/3 14:29:55', N'', NULL)
INSERT [dbo].[MeetingReservation] ([id], [meetingId], [title], [meetingRoom], [introduction], [time], [startTime], [endTime], [booker], [department], [state], [reviewer], [organizationId], [orderTime], [remark], [refuseReason]) VALUES (39, N'MRID202111030003', N'测试', N'MRID202107150002', N'', N'2021-11-11', N'2021-11-11T13:00', N'2021-11-11T14:15', N'UID20210913000010', NULL, N'正常', NULL, N'ORG202106220015', N'2021/11/3 14:30:43', N'', NULL)
INSERT [dbo].[MeetingReservation] ([id], [meetingId], [title], [meetingRoom], [introduction], [time], [startTime], [endTime], [booker], [department], [state], [reviewer], [organizationId], [orderTime], [remark], [refuseReason]) VALUES (46, N'MRID202111050004', N'测试', N'MRID202107150002', N'', N'2021-11-08', N'2021-11-08T19:56', N'2021-11-08T21:00', N'UID20210619000001', NULL, N'正常', NULL, N'ORG202106220015', N'2021/11/5 19:01:47', N'', NULL)
INSERT [dbo].[MeetingReservation] ([id], [meetingId], [title], [meetingRoom], [introduction], [time], [startTime], [endTime], [booker], [department], [state], [reviewer], [organizationId], [orderTime], [remark], [refuseReason]) VALUES (47, N'MRID202111080005', N'测试', N'MRID202107150002', N'', N'2021-11-08', N'2021-11-08T17:00', N'2021-11-08T17:30', N'UID20210619000001', NULL, N'正常', NULL, N'ORG202106220015', N'2021/11/8 17:13:26', N'', NULL)
INSERT [dbo].[MeetingReservation] ([id], [meetingId], [title], [meetingRoom], [introduction], [time], [startTime], [endTime], [booker], [department], [state], [reviewer], [organizationId], [orderTime], [remark], [refuseReason]) VALUES (48, N'MRID202111080006', N'测试', N'MRID202107150002', N'', N'2021-11-09', N'2021-11-09T21:04', N'2021-11-09T22:00', N'UID20210619000001', NULL, N'正常', NULL, N'ORG202106220015', N'2021/11/8 20:09:59', N'', NULL)
INSERT [dbo].[MeetingReservation] ([id], [meetingId], [title], [meetingRoom], [introduction], [time], [startTime], [endTime], [booker], [department], [state], [reviewer], [organizationId], [orderTime], [remark], [refuseReason]) VALUES (49, N'MRID202111090007', N'测试', N'MRID202107150002', N'', N'2021-11-20', N'2021-11-20T08:00', N'2021-11-20T10:00', N'UID20210619000001', NULL, N'正常', NULL, N'ORG202106220015', N'2021/11/9 14:18:55', N'', NULL)
INSERT [dbo].[MeetingReservation] ([id], [meetingId], [title], [meetingRoom], [introduction], [time], [startTime], [endTime], [booker], [department], [state], [reviewer], [organizationId], [orderTime], [remark], [refuseReason]) VALUES (50, N'MRID202111090008', N'测试', N'MRID202109160017', N'', N'2021-11-11', N'2021-11-11T08:00', N'2021-11-11T10:15', N'UID20210913000010', NULL, N'待审核', N'UID20210619000001', N'ORG202106220015', N'2021/11/9 14:56:02', N'', NULL)
INSERT [dbo].[MeetingReservation] ([id], [meetingId], [title], [meetingRoom], [introduction], [time], [startTime], [endTime], [booker], [department], [state], [reviewer], [organizationId], [orderTime], [remark], [refuseReason]) VALUES (51, N'MRID202111130009', N'测试', N'MRID202107150002', N'', N'2021-11-14', N'2021-11-14T08:00', N'2021-11-14T09:00', N'UID20210619000001', NULL, N'正常', NULL, N'ORG202106220015', N'2021/11/13 16:24:46', N'', NULL)
SET IDENTITY_INSERT [dbo].[MeetingReservation] OFF
SET IDENTITY_INSERT [dbo].[MeetingRoom] ON 

INSERT [dbo].[MeetingRoom] ([id], [roomId], [name], [image], [officeArea], [position], [capacity], [type], [introduction], [facility], [attention], [resDepartment], [director], [useRole], [useDepartment], [available], [reason], [organizationId], [isCheck], [time], [remark]) VALUES (2, N'MRID202107150002', N'101', NULL, N'OAID202107080004', N'2', N'30', N'RTID202109150004', N'', N'', N'', N'无', N'UID20210619000001', N'', N'', N'可用', NULL, N'ORG202106220015', N'否', CAST(N'2021-09-18T20:02:34.350' AS DateTime), N'')
INSERT [dbo].[MeetingRoom] ([id], [roomId], [name], [image], [officeArea], [position], [capacity], [type], [introduction], [facility], [attention], [resDepartment], [director], [useRole], [useDepartment], [available], [reason], [organizationId], [isCheck], [time], [remark]) VALUES (4, N'MRID202108270004', N'401', NULL, N'OAID202107080001', N'', N'', N'RTID202109150002', N'', N'', N'', N'无', N'UID20210628000008', N'', N'', N'可用', NULL, N'0', N'否', CAST(N'2021-09-18T20:02:34.453' AS DateTime), N'')
INSERT [dbo].[MeetingRoom] ([id], [roomId], [name], [image], [officeArea], [position], [capacity], [type], [introduction], [facility], [attention], [resDepartment], [director], [useRole], [useDepartment], [available], [reason], [organizationId], [isCheck], [time], [remark]) VALUES (5, N'MRID202108270005', N'402', NULL, N'OAID202107080001', N'', N'', N'RTID202109150002', N'', N'', N'', N'无', N'UID20210628000008', N'', N'', N'可用', NULL, N'0', N'是', CAST(N'2021-09-18T20:02:34.503' AS DateTime), N'')
INSERT [dbo].[MeetingRoom] ([id], [roomId], [name], [image], [officeArea], [position], [capacity], [type], [introduction], [facility], [attention], [resDepartment], [director], [useRole], [useDepartment], [available], [reason], [organizationId], [isCheck], [time], [remark]) VALUES (17, N'MRID202109160017', N'201', NULL, N'OAID202107080004', N'', N'50', N'RTID202109150004', N'', N'', N'', N'无', N'UID20210619000001', N'', N'', N'可用', NULL, N'ORG202106220015', N'是', CAST(N'2021-09-18T20:02:35.060' AS DateTime), N'')
INSERT [dbo].[MeetingRoom] ([id], [roomId], [name], [image], [officeArea], [position], [capacity], [type], [introduction], [facility], [attention], [resDepartment], [director], [useRole], [useDepartment], [available], [reason], [organizationId], [isCheck], [time], [remark]) VALUES (18, N'MRID202109160018', N'424', NULL, N'OAID202109160006', N'', N'50', N'RTID202109160005', N'', N'', N'', N'无', N'UID20210916000012', N'', N'', N'可用', NULL, N'ORG202109160020', N'否', CAST(N'2021-09-18T20:02:35.113' AS DateTime), N'')
INSERT [dbo].[MeetingRoom] ([id], [roomId], [name], [image], [officeArea], [position], [capacity], [type], [introduction], [facility], [attention], [resDepartment], [director], [useRole], [useDepartment], [available], [reason], [organizationId], [isCheck], [time], [remark]) VALUES (19, N'MRID202109160019', N'320', NULL, N'OAID202109160007', N'', N'50', N'RTID202109160005', N'', N'', N'', N'无', N'UID20210916000012', N'', N'', N'可用', NULL, N'ORG202109160020', N'否', CAST(N'2021-09-18T20:02:35.160' AS DateTime), N'')
INSERT [dbo].[MeetingRoom] ([id], [roomId], [name], [image], [officeArea], [position], [capacity], [type], [introduction], [facility], [attention], [resDepartment], [director], [useRole], [useDepartment], [available], [reason], [organizationId], [isCheck], [time], [remark]) VALUES (20, N'MRID202110120020', N'302', NULL, N'OAID202110120008', N'', N'', N'RTID202110120004', N'', N'', N'', N'无', N'UID20211012000013', N'', N'', N'可用', NULL, N'ORG202110120021', N'是', NULL, N'')
SET IDENTITY_INSERT [dbo].[MeetingRoom] OFF
SET IDENTITY_INSERT [dbo].[MeetingRoomFacility] ON 

INSERT [dbo].[MeetingRoomFacility] ([id], [roomId], [facilityId]) VALUES (34, N'MRID202108270004', N'RFID202110210002')
INSERT [dbo].[MeetingRoomFacility] ([id], [roomId], [facilityId]) VALUES (35, N'MRID202108270004', N'RFID202110210003')
INSERT [dbo].[MeetingRoomFacility] ([id], [roomId], [facilityId]) VALUES (36, N'MRID202108270004', N'RFID202110210004')
INSERT [dbo].[MeetingRoomFacility] ([id], [roomId], [facilityId]) VALUES (37, N'MRID202108270004', N'RFID202110210005')
INSERT [dbo].[MeetingRoomFacility] ([id], [roomId], [facilityId]) VALUES (38, N'MRID202107150002', N'RFID202110210007')
SET IDENTITY_INSERT [dbo].[MeetingRoomFacility] OFF
SET IDENTITY_INSERT [dbo].[MeetingRoomType] ON 

INSERT [dbo].[MeetingRoomType] ([id], [RoomTypeId], [name], [introduction], [organizationId], [remark]) VALUES (4, N'RTID202109150004', N'新型多媒体会议室', N'多媒体会议室', N'ORG202106220015', N'')
INSERT [dbo].[MeetingRoomType] ([id], [RoomTypeId], [name], [introduction], [organizationId], [remark]) VALUES (7, N'RTID202109160005', N'圆桌会议室', N'', N'ORG202109160020', N'')
INSERT [dbo].[MeetingRoomType] ([id], [RoomTypeId], [name], [introduction], [organizationId], [remark]) VALUES (8, N'RTID202109160006', N'多媒体会议室', N'', N'ORG202109160020', N'')
INSERT [dbo].[MeetingRoomType] ([id], [RoomTypeId], [name], [introduction], [organizationId], [remark]) VALUES (9, N'RTID202109150002', N'多媒体会议室', NULL, N'0', NULL)
INSERT [dbo].[MeetingRoomType] ([id], [RoomTypeId], [name], [introduction], [organizationId], [remark]) VALUES (11, N'RTID202110120004', N'圆桌会议室', N'', N'ORG202110120021', N'')
SET IDENTITY_INSERT [dbo].[MeetingRoomType] OFF
SET IDENTITY_INSERT [dbo].[OfficeArea] ON 

INSERT [dbo].[OfficeArea] ([id], [officeAreaId], [name], [superiorArea], [address], [phone], [serviceDirector], [introduction], [organizationId], [organizationName], [remark]) VALUES (16, N'OAID202107080001', N'信息馆4楼', NULL, N'燕山大学', N'123456', N'UID20210628000008', N'软件实验室', N'0', NULL, N'')
INSERT [dbo].[OfficeArea] ([id], [officeAreaId], [name], [superiorArea], [address], [phone], [serviceDirector], [introduction], [organizationId], [organizationName], [remark]) VALUES (19, N'OAID202107080004', N'信息馆', NULL, N'燕山大学', N'9999', N'UID20210619000001', N'', N'ORG202106220015', NULL, N'')
INSERT [dbo].[OfficeArea] ([id], [officeAreaId], [name], [superiorArea], [address], [phone], [serviceDirector], [introduction], [organizationId], [organizationName], [remark]) VALUES (30, N'OAID202109160006', N'916的4楼', NULL, N'', N'', N'UID20210916000012', N'', N'ORG202109160020', NULL, N'')
INSERT [dbo].[OfficeArea] ([id], [officeAreaId], [name], [superiorArea], [address], [phone], [serviceDirector], [introduction], [organizationId], [organizationName], [remark]) VALUES (31, N'OAID202109160007', N'916的3楼', NULL, N'', N'', N'UID20210916000012', N'', N'ORG202109160020', NULL, N'')
INSERT [dbo].[OfficeArea] ([id], [officeAreaId], [name], [superiorArea], [address], [phone], [serviceDirector], [introduction], [organizationId], [organizationName], [remark]) VALUES (32, N'OAID202110120008', N'424', NULL, N'燕山大学', N'123456', N'UID20211012000013', N'', N'ORG202110120021', NULL, N'')
SET IDENTITY_INSERT [dbo].[OfficeArea] OFF
SET IDENTITY_INSERT [dbo].[Organization] ON 

INSERT [dbo].[Organization] ([id], [organizationId], [name], [introduction], [logo], [reseStart], [reseEnd], [timeUnit], [signIn], [sameTimeAttend], [time], [remark]) VALUES (1, N'ORG202106220015', N'YSU', N'001', NULL, N'08:00', N'23:00', N'15', NULL, NULL, CAST(N'2021-06-22T20:27:50.290' AS DateTime), N'')
INSERT [dbo].[Organization] ([id], [organizationId], [name], [introduction], [logo], [reseStart], [reseEnd], [timeUnit], [signIn], [sameTimeAttend], [time], [remark]) VALUES (2, N'ORG202106220016', N'002', N'002', NULL, N'08:00', N'23:00', N'30', NULL, NULL, CAST(N'2021-06-22T21:02:24.777' AS DateTime), NULL)
INSERT [dbo].[Organization] ([id], [organizationId], [name], [introduction], [logo], [reseStart], [reseEnd], [timeUnit], [signIn], [sameTimeAttend], [time], [remark]) VALUES (3, N'ORG202106290018', N'003', N'003', NULL, N'08:00', N'23:00', N'30', NULL, NULL, CAST(N'2021-06-29T09:59:19.270' AS DateTime), NULL)
INSERT [dbo].[Organization] ([id], [organizationId], [name], [introduction], [logo], [reseStart], [reseEnd], [timeUnit], [signIn], [sameTimeAttend], [time], [remark]) VALUES (4, N'ORG202106290019', N'004', N'004', NULL, N'08:00', N'23:00', N'30', NULL, NULL, CAST(N'2021-06-29T10:07:48.503' AS DateTime), NULL)
INSERT [dbo].[Organization] ([id], [organizationId], [name], [introduction], [logo], [reseStart], [reseEnd], [timeUnit], [signIn], [sameTimeAttend], [time], [remark]) VALUES (5, N'ORG202109160020', N'916', N'916', NULL, N'08:00', N'23:00', N'30', NULL, NULL, CAST(N'2021-09-16T09:50:56.513' AS DateTime), NULL)
INSERT [dbo].[Organization] ([id], [organizationId], [name], [introduction], [logo], [reseStart], [reseEnd], [timeUnit], [signIn], [sameTimeAttend], [time], [remark]) VALUES (7, N'ORG202110120021', N'999', N'999', NULL, N'10:00', N'17:00', N'15', NULL, NULL, CAST(N'2021-10-12T21:29:02.240' AS DateTime), N'')
INSERT [dbo].[Organization] ([id], [organizationId], [name], [introduction], [logo], [reseStart], [reseEnd], [timeUnit], [signIn], [sameTimeAttend], [time], [remark]) VALUES (8, N'0', N'测试组', N'测试组', NULL, N'09:00', N'20:00', N'30', N'false', N'true', CAST(N'2021-10-18T17:03:09.567' AS DateTime), N'测试组')
INSERT [dbo].[Organization] ([id], [organizationId], [name], [introduction], [logo], [reseStart], [reseEnd], [timeUnit], [signIn], [sameTimeAttend], [time], [remark]) VALUES (9, N'ORG202110120022', N'测试组2', N'999', NULL, N'10:00', N'17:00', N'15', N'false', N'true', CAST(N'2021-10-18T17:04:38.457' AS DateTime), N'')
INSERT [dbo].[Organization] ([id], [organizationId], [name], [introduction], [logo], [reseStart], [reseEnd], [timeUnit], [signIn], [sameTimeAttend], [time], [remark]) VALUES (10, N'ORG202110200023', N'张三的组织', N'', NULL, N'08:00', N'23:00', N'30', NULL, NULL, CAST(N'2021-10-20T10:16:30.673' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Organization] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([id], [roleId], [roleName], [organizationId], [defaultRole], [remark]) VALUES (1, N'R202106280001', N'服务提供商', NULL, N'是', N'系统超级管理员')
INSERT [dbo].[Role] ([id], [roleId], [roleName], [organizationId], [defaultRole], [remark]) VALUES (2, N'R202106280002', N'租户', NULL, N'是', N'内置角色')
INSERT [dbo].[Role] ([id], [roleId], [roleName], [organizationId], [defaultRole], [remark]) VALUES (3, N'R202106280003', N'普通用户', NULL, N'是', N'内置角色')
INSERT [dbo].[Role] ([id], [roleId], [roleName], [organizationId], [defaultRole], [remark]) VALUES (4, N'R202106280004', N'信院财务', N'ORG202106220015', N'否', N'123')
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[RoleRight] ON 

INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (254, N'R202106280003', N'普通用户', N'003', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (255, N'R202106280003', N'普通用户', N'003001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (256, N'R202106280003', N'普通用户', N'005', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (257, N'R202106280003', N'普通用户', N'006', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (258, N'R202106280003', N'普通用户', N'010', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (259, N'R202106280003', N'普通用户', N'010001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (260, N'R202106280003', N'普通用户', N'011', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (261, N'R202106280003', N'普通用户', N'011001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (262, N'R202106280003', N'普通用户', N'011002', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (263, N'R202106280003', N'普通用户', N'004', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (264, N'R202106280003', N'普通用户', N'004001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (265, N'R202106280003', N'普通用户', N'004002', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (309, N'R202106280004', N'信院财务', N'010', N'ORG202106220015', NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (310, N'R202106280004', N'信院财务', N'010001', N'ORG202106220015', NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (452, N'R202106280002', N'租户', N'003', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (453, N'R202106280002', N'租户', N'003001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (454, N'R202106280002', N'租户', N'006', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (455, N'R202106280002', N'租户', N'006001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (456, N'R202106280002', N'租户', N'006002', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (457, N'R202106280002', N'租户', N'007', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (458, N'R202106280002', N'租户', N'007002', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (459, N'R202106280002', N'租户', N'007003', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (460, N'R202106280002', N'租户', N'007004', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (461, N'R202106280002', N'租户', N'007005', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (462, N'R202106280002', N'租户', N'008', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (463, N'R202106280002', N'租户', N'008001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (464, N'R202106280002', N'租户', N'008002', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (465, N'R202106280002', N'租户', N'008003', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (466, N'R202106280002', N'租户', N'009', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (467, N'R202106280002', N'租户', N'009001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (468, N'R202106280002', N'租户', N'010', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (469, N'R202106280002', N'租户', N'010001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (470, N'R202106280002', N'租户', N'011', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (471, N'R202106280002', N'租户', N'011001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (472, N'R202106280002', N'租户', N'011002', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (473, N'R202106280002', N'租户', N'004', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (474, N'R202106280002', N'租户', N'004001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (475, N'R202106280002', N'租户', N'004002', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (476, N'R202106280001', N'服务提供商', N'001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (477, N'R202106280001', N'服务提供商', N'001001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (478, N'R202106280001', N'服务提供商', N'002', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (479, N'R202106280001', N'服务提供商', N'002001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (480, N'R202106280001', N'服务提供商', N'002002', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (481, N'R202106280001', N'服务提供商', N'002003', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (482, N'R202106280001', N'服务提供商', N'011', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (483, N'R202106280001', N'服务提供商', N'011001', NULL, NULL, NULL)
INSERT [dbo].[RoleRight] ([id], [roleId], [roleName], [rightCode], [organizationId], [organizationName], [remark]) VALUES (484, N'R202106280001', N'服务提供商', N'011002', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[RoleRight] OFF
SET IDENTITY_INSERT [dbo].[RoomFacility] ON 

INSERT [dbo].[RoomFacility] ([id], [facilityId], [name], [introduction], [organizationId], [remark]) VALUES (2, N'RFID202110210002', N'投影仪', N'', N'0', N'')
INSERT [dbo].[RoomFacility] ([id], [facilityId], [name], [introduction], [organizationId], [remark]) VALUES (3, N'RFID202110210003', N'电脑', N'', N'0', N'')
INSERT [dbo].[RoomFacility] ([id], [facilityId], [name], [introduction], [organizationId], [remark]) VALUES (4, N'RFID202110210004', N'激光笔', N'', N'0', N'')
INSERT [dbo].[RoomFacility] ([id], [facilityId], [name], [introduction], [organizationId], [remark]) VALUES (5, N'RFID202110210005', N'会议桌', N'', N'0', N'')
INSERT [dbo].[RoomFacility] ([id], [facilityId], [name], [introduction], [organizationId], [remark]) VALUES (6, N'RFID202110210006', N'麦克风', N'', N'ORG202106220015', N'')
INSERT [dbo].[RoomFacility] ([id], [facilityId], [name], [introduction], [organizationId], [remark]) VALUES (7, N'RFID202110210007', N'投影仪', N'', N'ORG202106220015', N'')
INSERT [dbo].[RoomFacility] ([id], [facilityId], [name], [introduction], [organizationId], [remark]) VALUES (8, N'RFID202110210008', N'电脑', N'', N'ORG202106220015', N'')
SET IDENTITY_INSERT [dbo].[RoomFacility] OFF
SET IDENTITY_INSERT [dbo].[Tenant] ON 

INSERT [dbo].[Tenant] ([id], [userId], [organizationId], [remark]) VALUES (1, N'UID20210619000001', N'ORG202106220015', NULL)
INSERT [dbo].[Tenant] ([id], [userId], [organizationId], [remark]) VALUES (2, N'UID20210620000002', N'ORG202106220016', NULL)
INSERT [dbo].[Tenant] ([id], [userId], [organizationId], [remark]) VALUES (3, N'UID20210629000009', N'ORG202106290018', NULL)
INSERT [dbo].[Tenant] ([id], [userId], [organizationId], [remark]) VALUES (4, N'UID20210621000004', N'ORG202106290019', NULL)
INSERT [dbo].[Tenant] ([id], [userId], [organizationId], [remark]) VALUES (5, N'UID20210916000012', N'ORG202109160020', NULL)
INSERT [dbo].[Tenant] ([id], [userId], [organizationId], [remark]) VALUES (6, N'UID20211012000013', N'ORG202110120021', NULL)
INSERT [dbo].[Tenant] ([id], [userId], [organizationId], [remark]) VALUES (7, N'UID20211020000018', N'ORG202110200023', NULL)
SET IDENTITY_INSERT [dbo].[Tenant] OFF
ALTER TABLE [dbo].[AllUser] ADD  CONSTRAINT [DF_AllUser_available]  DEFAULT ('true') FOR [available]
GO
ALTER TABLE [dbo].[Department] ADD  CONSTRAINT [DF_Department_time]  DEFAULT (getdate()) FOR [time]
GO
ALTER TABLE [dbo].[FunctionModel] ADD  CONSTRAINT [DF_FunctionModel_time_1]  DEFAULT (getdate()) FOR [time]
GO
ALTER TABLE [dbo].[MeetingReservation] ADD  CONSTRAINT [DF_MeetingReservation_time_1]  DEFAULT (getdate()) FOR [time]
GO
ALTER TABLE [dbo].[MeetingRoom] ADD  CONSTRAINT [DF_MeetingRoom_available]  DEFAULT ('可用') FOR [available]
GO
ALTER TABLE [dbo].[MeetingRoom] ADD  CONSTRAINT [DF_MeetingRoom_time]  DEFAULT (getdate()) FOR [time]
GO
ALTER TABLE [dbo].[Organization] ADD  CONSTRAINT [DF_Organization_signIn_1]  DEFAULT ('false') FOR [signIn]
GO
ALTER TABLE [dbo].[Organization] ADD  CONSTRAINT [DF_Organization_sameTimeAttend_1]  DEFAULT ('true') FOR [sameTimeAttend]
GO
ALTER TABLE [dbo].[Organization] ADD  CONSTRAINT [DF_Organization_time_1]  DEFAULT (getdate()) FOR [time]
GO
/****** Object:  StoredProcedure [dbo].[Pr_GetCountNumber]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Pr_GetCountNumber]  
(  
 @tableName nvarchar(3000),  
 @primaryKey nvarchar(50),  
    @condition nvarchar(max)  
)  
AS  
begin  
DECLARE @sql nvarchar(max)  
 set @sql= 'select count('+ @primaryKey + ') from '+ @tableName +' where ' + @condition  
exec(@sql)  
return  
end 
GO
/****** Object:  StoredProcedure [dbo].[Pr_GetGroupData]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE [dbo].[Pr_GetGroupData]
(
 @tableName nvarchar(3000),
 @columns varchar(1000) = '*',
 @groupcolumns varchar(1000) = '*',
 @condition nvarchar(max),
 @orderByColumn nvarchar(50),
 @isASC bit = 1
 )
AS
begin

	DECLARE @sql nvarchar(max)

	IF @isASC = 1
		BEGIN
			set @sql = 'SELECT ROW_NUMBER() OVER (ORDER BY '+ @orderByColumn+' asc) AS Row,' + @columns+ ' from ' +@tableName + ' where '+ @condition+' group by '+@groupcolumns
		END
	ELSE
		BEGIN
			set @sql = 'SELECT ROW_NUMBER() OVER (ORDER BY '+ @orderByColumn+' desc) AS Row,' + @columns+ ' from ' +@tableName + ' where '+ @condition+' group by '+@groupcolumns
		END
	exec(@sql)
end

GO
/****** Object:  StoredProcedure [dbo].[Pr_GetPagedData]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Pr_GetPagedData]  
(  
 @tableName nvarchar(3000),  
 @condition nvarchar(max),  
 @orderByColumn nvarchar(50),  
 @isASC bit = 1,   
 @startIndex INT,   
 @endIndex INT  
 )  
AS  
begin  
  
 DECLARE @sql nvarchar(max)  
  
 IF @isASC = 1  
  BEGIN  
   set @sql = 'with temptbl as ( SELECT ROW_NUMBER() OVER (ORDER BY '+ @orderByColumn+' asc) AS Row, * from ' +@tableName + ' where '+ @condition+')'  
        +' SELECT * FROM temptbl where Row between '+ str(@startIndex)+ ' and ' +str(@endIndex)  
  END  
 ELSE  
  BEGIN  
   set @sql = 'with temptbl as ( SELECT ROW_NUMBER() OVER (ORDER BY '+ @orderByColumn+' desc) AS Row, * from ' +@tableName + ' where '+ @condition+')'  
        +' SELECT * FROM temptbl where Row between '+ str(@startIndex)+ ' and ' +str(@endIndex)  
  END  
 exec(@sql)  
end  
GO
/****** Object:  StoredProcedure [dbo].[Pr_GetPagedGroupData]    Script Date: 2021/11/14 10:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Pr_GetPagedGroupData]
(
 @tableName nvarchar(3000),
 @columns varchar(1000) = '*',
 @groupcolumns varchar(1000) = '*',
 @condition nvarchar(max),
 @orderByColumn nvarchar(50),
 @isASC bit = 1, 
 @startIndex INT, 
 @endIndex INT
 )
AS
begin

	DECLARE @sql nvarchar(max)

	IF @isASC = 1
		BEGIN
			set @sql = 'with temptbl as ( SELECT ROW_NUMBER() OVER (ORDER BY '+ @orderByColumn+' asc) AS Row,' + @columns+ ' from ' +@tableName + ' where '+ @condition+' group by '+@groupcolumns+')'
					   +' SELECT * FROM temptbl where Row between '+ str(@startIndex)+ ' and ' +str(@endIndex)
		END
	ELSE
		BEGIN
			set @sql = 'with temptbl as ( SELECT ROW_NUMBER() OVER (ORDER BY '+ @orderByColumn+' desc) AS Row,' + @columns+ ' from ' +@tableName + ' where '+ @condition+' group by '+@groupcolumns+')'
					   +' SELECT * FROM temptbl where Row between '+ str(@startIndex)+ ' and ' +str(@endIndex)
		END
	exec(@sql)
end

GO
USE [master]
GO
ALTER DATABASE [MeetingResMagSys] SET  READ_WRITE 
GO
