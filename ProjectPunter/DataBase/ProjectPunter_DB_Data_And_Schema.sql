USE [master]
GO
/****** Object:  Database [RHDC]    Script Date: 8/13/2021 11:35:48 AM ******/
CREATE DATABASE [RHDC]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RHDC', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\RHDC.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RHDC_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\RHDC_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RHDC] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RHDC].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RHDC] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RHDC] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RHDC] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RHDC] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RHDC] SET ARITHABORT OFF 
GO
ALTER DATABASE [RHDC] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RHDC] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RHDC] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RHDC] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RHDC] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RHDC] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RHDC] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RHDC] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RHDC] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RHDC] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RHDC] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RHDC] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RHDC] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RHDC] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RHDC] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RHDC] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RHDC] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RHDC] SET RECOVERY FULL 
GO
ALTER DATABASE [RHDC] SET  MULTI_USER 
GO
ALTER DATABASE [RHDC] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RHDC] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RHDC] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RHDC] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RHDC] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RHDC] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'RHDC', N'ON'
GO
ALTER DATABASE [RHDC] SET QUERY_STORE = OFF
GO
USE [RHDC]
GO
/****** Object:  Table [dbo].[tb_batch]    Script Date: 8/13/2021 11:35:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_batch](
	[batch_id] [uniqueidentifier] NOT NULL,
	[diagnostics] [varchar](1000) NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_tb_batch] PRIMARY KEY CLUSTERED 
(
	[batch_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_course]    Script Date: 8/13/2021 11:35:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_course](
	[course_id] [int] NOT NULL,
	[name] [varchar](150) NOT NULL,
	[country_code] [varchar](6) NOT NULL,
	[all_weather] [bit] NULL,
 CONSTRAINT [PK_tb_course] PRIMARY KEY CLUSTERED 
(
	[course_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_event]    Script Date: 8/13/2021 11:35:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_event](
	[event_id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NOT NULL,
	[abandoned] [bit] NULL,
	[surface_type] [varchar](50) NULL,
	[name] [varchar](150) NULL,
	[meeting_url] [varchar](500) NULL,
	[hash_name] [varchar](150) NULL,
	[meeting_type] [varchar](50) NULL,
	[races] [int] NOT NULL,
	[batch_id] [uniqueidentifier] NOT NULL,
	[created] [datetime] NOT NULL,
 CONSTRAINT [PK_tb_event] PRIMARY KEY CLUSTERED 
(
	[event_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather]) VALUES (36, N'Newbury', N'GB', 0)
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather]) VALUES (40, N'Nottingham', N'GB', 0)
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather]) VALUES (80, N'Thirsk', N'GB', 0)
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather]) VALUES (174, N'Newmarket (July)', N'GB', 0)
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather]) VALUES (178, N'Curragh', N'IRE', 0)
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather]) VALUES (200, N'Tramore', N'IRE', 0)
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather]) VALUES (513, N'Wolverhampton', N'GB', 1)
GO
SET IDENTITY_INSERT [dbo].[tb_event] ON 

INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (8, 513, 0, N'Tapeta', N'Wolverhampton_8/13/2021', N'/racecards/513/wolverhampton-aw/2021-08-13', N'wolverhampton-aw', N'Flat', 6, N'facd5139-ba02-4fe9-b23c-f50f03ca56fe', CAST(N'2021-08-13T11:33:13.953' AS DateTime))
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (9, 36, 0, N'', N'Newbury_8/13/2021', N'/racecards/36/newbury/2021-08-13', N'newbury', N'Flat', 8, N'facd5139-ba02-4fe9-b23c-f50f03ca56fe', CAST(N'2021-08-13T11:33:14.077' AS DateTime))
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (10, 40, 0, N'', N'Nottingham_8/13/2021', N'/racecards/40/nottingham/2021-08-13', N'nottingham', N'Flat', 7, N'facd5139-ba02-4fe9-b23c-f50f03ca56fe', CAST(N'2021-08-13T11:33:14.120' AS DateTime))
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (11, 178, 0, N'', N'Curragh_8/13/2021', N'/racecards/178/curragh/2021-08-13', N'curragh', N'Flat', 8, N'facd5139-ba02-4fe9-b23c-f50f03ca56fe', CAST(N'2021-08-13T11:33:14.160' AS DateTime))
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (12, 174, 0, N'', N'Newmarket (July)_8/13/2021', N'/racecards/174/newmarket-july/2021-08-13', N'newmarket-july', N'Flat', 6, N'facd5139-ba02-4fe9-b23c-f50f03ca56fe', CAST(N'2021-08-13T11:33:14.180' AS DateTime))
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (13, 200, 0, N'', N'Tramore_8/13/2021', N'/racecards/200/tramore/2021-08-13', N'tramore', N'Jumps', 7, N'facd5139-ba02-4fe9-b23c-f50f03ca56fe', CAST(N'2021-08-13T11:33:14.203' AS DateTime))
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (14, 80, 0, N'', N'Thirsk_8/13/2021', N'/racecards/80/thirsk/2021-08-13', N'thirsk', N'Flat', 7, N'facd5139-ba02-4fe9-b23c-f50f03ca56fe', CAST(N'2021-08-13T11:33:14.233' AS DateTime))
SET IDENTITY_INSERT [dbo].[tb_event] OFF
GO
ALTER TABLE [dbo].[tb_event]  WITH CHECK ADD  CONSTRAINT [FK_tb_event_tb_course] FOREIGN KEY([course_id])
REFERENCES [dbo].[tb_course] ([course_id])
GO
ALTER TABLE [dbo].[tb_event] CHECK CONSTRAINT [FK_tb_event_tb_course]
GO
USE [master]
GO
ALTER DATABASE [RHDC] SET  READ_WRITE 
GO
