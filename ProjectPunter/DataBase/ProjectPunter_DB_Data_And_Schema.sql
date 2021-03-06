USE [master]
GO
/****** Object:  Database [ProjectPunter]    Script Date: 17/09/2020 12:58:59 ******/
CREATE DATABASE [ProjectPunter]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProjectPunter', FILENAME = N'C:\Users\Craig\ProjectPunter.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProjectPunter_log', FILENAME = N'C:\Users\Craig\ProjectPunter_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ProjectPunter] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProjectPunter].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProjectPunter] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjectPunter] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjectPunter] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjectPunter] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjectPunter] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjectPunter] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProjectPunter] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjectPunter] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectPunter] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjectPunter] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjectPunter] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjectPunter] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjectPunter] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjectPunter] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjectPunter] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProjectPunter] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjectPunter] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjectPunter] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjectPunter] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjectPunter] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjectPunter] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjectPunter] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjectPunter] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProjectPunter] SET  MULTI_USER 
GO
ALTER DATABASE [ProjectPunter] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjectPunter] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjectPunter] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjectPunter] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProjectPunter] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProjectPunter] SET QUERY_STORE = OFF
GO
USE [ProjectPunter]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [ProjectPunter]
GO
/****** Object:  Table [dbo].[tb_class]    Script Date: 17/09/2020 12:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_class](
	[Class_Id] [int] IDENTITY(1,1) NOT NULL,
	[Class_Number] [int] NOT NULL,
	[Class_Description] [varchar](50) NULL,
 CONSTRAINT [PK_tb_class] PRIMARY KEY CLUSTERED 
(
	[Class_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_country]    Script Date: 17/09/2020 12:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_country](
	[Country_Id] [int] IDENTITY(1,1) NOT NULL,
	[Country_Name] [varchar](50) NOT NULL,
	[Country_Code] [nchar](10) NULL,
 CONSTRAINT [PK_tb_country] PRIMARY KEY CLUSTERED 
(
	[Country_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_error_log]    Script Date: 17/09/2020 12:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_error_log](
	[Error_Id] [int] IDENTITY(1,1) NOT NULL,
	[Error_Log] [varchar](max) NULL,
	[Inner_Exception] [varchar](max) NULL,
	[Stack_Trace] [varchar](max) NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_tb_error_log] PRIMARY KEY CLUSTERED 
(
	[Error_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_event]    Script Date: 17/09/2020 12:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_event](
	[Event_Id] [int] IDENTITY(1,1) NOT NULL,
	[Event_Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tb_event] PRIMARY KEY CLUSTERED 
(
	[Event_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_horse]    Script Date: 17/09/2020 12:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_horse](
	[Horse_Id] [int] IDENTITY(1,1) NOT NULL,
	[Horse_Name] [varchar](50) NOT NULL,
	[Date_Of_Birth] [datetime] NULL,
	[IsRetired] [bit] NOT NULL,
	[Notes] [varchar](max) NULL,
	[Age]  AS (datediff(year,[Date_Of_Birth],getdate())),
	[Country] [int] NOT NULL,
 CONSTRAINT [PK_tb_horse] PRIMARY KEY CLUSTERED 
(
	[Horse_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_jockey]    Script Date: 17/09/2020 12:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_jockey](
	[Jockey_Id] [int] IDENTITY(1,1) NOT NULL,
	[Jockey_Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_tb_jockey] PRIMARY KEY CLUSTERED 
(
	[Jockey_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_race]    Script Date: 17/09/2020 12:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_race](
	[Race_Id] [int] IDENTITY(1,1) NOT NULL,
	[Event_Id] [int] NOT NULL,
	[Weather_Id] [int] NOT NULL,
	[Surface_Id] [int] NOT NULL,
	[Race_Type_Id] [int] NOT NULL,
	[Class_Id] [int] NOT NULL,
	[Number_Of_Horses] [int] NOT NULL,
	[IsComplete] [bit] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_tb_race] PRIMARY KEY CLUSTERED 
(
	[Race_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_race_horse]    Script Date: 17/09/2020 12:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_race_horse](
	[Race_Id] [int] NOT NULL,
	[Horse_Id] [int] NOT NULL,
	[Weight] [int] NOT NULL,
	[Age] [int] NOT NULL,
	[Trainer_Id] [int] NOT NULL,
	[Jockey_Id] [int] NOT NULL,
	[Position] [int] NULL,
	[DNF] [bit] NULL,
	[Clean_Race] [bit] NULL,
 CONSTRAINT [PK_tb_race_horse] PRIMARY KEY CLUSTERED 
(
	[Race_Id] ASC,
	[Horse_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_racetype]    Script Date: 17/09/2020 12:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_racetype](
	[Race_Type_Id] [int] IDENTITY(1,1) NOT NULL,
	[Race_Type_Description] [varchar](50) NOT NULL,
	[Hurdle] [bit] NOT NULL,
	[No_Of_Hurdles] [int] NULL,
	[Meters] [int] NOT NULL,
	[furlongs] [int] NOT NULL,
 CONSTRAINT [PK_tb_racetype] PRIMARY KEY CLUSTERED 
(
	[Race_Type_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_sql_error_log]    Script Date: 17/09/2020 12:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_sql_error_log](
	[Sql_Error_Id] [int] IDENTITY(1,1) NOT NULL,
	[Object_Name] [varchar](100) NULL,
	[Error] [varchar](max) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_tb_sql_error_log] PRIMARY KEY CLUSTERED 
(
	[Sql_Error_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_surface]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_surface](
	[Surface_Id] [int] IDENTITY(1,1) NOT NULL,
	[Surface_Description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tb_surface] PRIMARY KEY CLUSTERED 
(
	[Surface_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_trainer]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_trainer](
	[Trainer_Id] [int] IDENTITY(1,1) NOT NULL,
	[Trainer_Name] [varchar](100) NULL,
 CONSTRAINT [PK_tb_trainer] PRIMARY KEY CLUSTERED 
(
	[Trainer_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_weather]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_weather](
	[Weather_Id] [int] IDENTITY(1,1) NOT NULL,
	[Weather_Description] [varchar](100) NULL,
 CONSTRAINT [PK_tb_weather] PRIMARY KEY CLUSTERED 
(
	[Weather_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tb_class] ON 

INSERT [dbo].[tb_class] ([Class_Id], [Class_Number], [Class_Description]) VALUES (1, 1, NULL)
INSERT [dbo].[tb_class] ([Class_Id], [Class_Number], [Class_Description]) VALUES (2, 2, NULL)
INSERT [dbo].[tb_class] ([Class_Id], [Class_Number], [Class_Description]) VALUES (3, 3, NULL)
INSERT [dbo].[tb_class] ([Class_Id], [Class_Number], [Class_Description]) VALUES (4, 4, NULL)
INSERT [dbo].[tb_class] ([Class_Id], [Class_Number], [Class_Description]) VALUES (5, 5, NULL)
SET IDENTITY_INSERT [dbo].[tb_class] OFF
SET IDENTITY_INSERT [dbo].[tb_country] ON 

INSERT [dbo].[tb_country] ([Country_Id], [Country_Name], [Country_Code]) VALUES (1, N'Ireland', N'IRE       ')
INSERT [dbo].[tb_country] ([Country_Id], [Country_Name], [Country_Code]) VALUES (2, N'Great Britain', N'GBR       ')
INSERT [dbo].[tb_country] ([Country_Id], [Country_Name], [Country_Code]) VALUES (3, N'Australia', N'AUS       ')
INSERT [dbo].[tb_country] ([Country_Id], [Country_Name], [Country_Code]) VALUES (4, N'America', N'USA       ')
INSERT [dbo].[tb_country] ([Country_Id], [Country_Name], [Country_Code]) VALUES (5, N'France', N'FRA       ')
INSERT [dbo].[tb_country] ([Country_Id], [Country_Name], [Country_Code]) VALUES (6, N'Spain', N'ESP       ')
INSERT [dbo].[tb_country] ([Country_Id], [Country_Name], [Country_Code]) VALUES (7, N'Portugal', N'POR       ')
INSERT [dbo].[tb_country] ([Country_Id], [Country_Name], [Country_Code]) VALUES (8, N'Germany', N'GER       ')
INSERT [dbo].[tb_country] ([Country_Id], [Country_Name], [Country_Code]) VALUES (9, N'Italy', N'ITA       ')
SET IDENTITY_INSERT [dbo].[tb_country] OFF
SET IDENTITY_INSERT [dbo].[tb_event] ON 

INSERT [dbo].[tb_event] ([Event_Id], [Event_Name]) VALUES (1, N'Uttoxeter')
SET IDENTITY_INSERT [dbo].[tb_event] OFF
SET IDENTITY_INSERT [dbo].[tb_horse] ON 

INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (1, N'Cougar Kid', CAST(N'2011-02-18T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (2, N'Kodiline', CAST(N'2014-04-19T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (3, N'Agent Of Fortune', CAST(N'2015-03-15T00:00:00.000' AS DateTime), 0, NULL, 2)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (4, N'Vincenzo Coccotti', CAST(N'2012-02-19T00:00:00.000' AS DateTime), 0, NULL, 4)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (5, N'Maazel', CAST(N'2014-04-16T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (8, N'Brigand', CAST(N'2015-02-03T00:00:00.000' AS DateTime), 0, NULL, 2)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (9, N'Is It Off', CAST(N'2015-03-07T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (10, N'Billingsley', CAST(N'2012-05-02T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (11, N'Divine Gift', CAST(N'2016-03-23T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (12, N'Eleanor Bob', CAST(N'2015-04-21T00:00:00.000' AS DateTime), 0, NULL, 2)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (13, N'Chloe''s Court', CAST(N'2013-04-09T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (14, N'Massini''s Dream', CAST(N'2011-03-30T00:00:00.000' AS DateTime), 0, NULL, 2)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (15, N'Catchmeifyoucan', CAST(N'2014-05-16T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (16, N'Emmas Dilemma', CAST(N'2012-04-30T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (17, N'Getawaytonewbay', CAST(N'2013-05-14T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (18, N'Special Princess', CAST(N'2010-03-15T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (19, N'Incertaine', CAST(N'2013-05-16T00:00:00.000' AS DateTime), 0, NULL, 2)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (20, N'Truckers Cailin', CAST(N'2013-05-13T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (21, N'Truckers Cailn', CAST(N'2019-11-27T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (22, N'Bean Liath', CAST(N'2011-05-05T00:00:00.000' AS DateTime), 0, NULL, 1)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (23, N'Mole Trap', CAST(N'2011-04-30T00:00:00.000' AS DateTime), 0, NULL, 2)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (24, N'Seymour Sox', CAST(N'2014-06-05T00:00:00.000' AS DateTime), 0, NULL, 2)
INSERT [dbo].[tb_horse] ([Horse_Id], [Horse_Name], [Date_Of_Birth], [IsRetired], [Notes], [Country]) VALUES (25, N'Cosheston', CAST(N'2013-05-12T00:00:00.000' AS DateTime), 0, NULL, 2)
SET IDENTITY_INSERT [dbo].[tb_horse] OFF
SET IDENTITY_INSERT [dbo].[tb_jockey] ON 

INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (1, N'Graham Lee')
INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (2, N'Ben Curtis')
INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (3, N'Daniel Tudhope')
INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (4, N'Jack Garritty')
INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (5, N'David Allan')
INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (6, N'James Sulivan')
INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (7, N'Test')
INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (8, N'Test')
INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (9, N'Test1')
INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (12, N'Test')
INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (13, N'Test')
INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (14, N'Boob')
INSERT [dbo].[tb_jockey] ([Jockey_Id], [Jockey_Name]) VALUES (1010, N'Joe Bloggs')
SET IDENTITY_INSERT [dbo].[tb_jockey] OFF
SET IDENTITY_INSERT [dbo].[tb_race] ON 

INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (6, 1, 1, 1, 1, 1, 6, 0, CAST(N'2020-01-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (7, 1, 1, 1, 1, 1, 5, 0, CAST(N'2020-01-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (8, 1, 1, 1, 1, 1, 2, 0, CAST(N'2020-01-16T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (9, 1, 1, 1, 1, 1, 3, 0, CAST(N'2020-01-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (10, 1, 1, 1, 1, 1, 3, 0, CAST(N'2020-01-17T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1002, 1, 1, 1, 1, 2, 11, 0, CAST(N'2020-07-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1003, 1, 1, 1, 1, 2, 11, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1004, 1, 1, 1, 1, 1, 5, 0, CAST(N'2020-08-06T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1005, 1, 1, 1, 1, 1, 12, 0, CAST(N'2020-08-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1006, 1, 1, 1, 1, 2, 11, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1007, 1, 1, 1, 1, 1, 11, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1008, 1, 1, 1, 1, 3, 5, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1009, 1, 1, 1, 1, 1, 3, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1010, 1, 1, 1, 1, 1, 2, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1011, 1, 1, 1, 1, 2, 3, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1012, 1, 1, 1, 1, 2, 2, 0, CAST(N'2020-08-14T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1013, 1, 1, 1, 1, 1, 7, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1014, 1, 1, 1, 1, 1, 4, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1015, 1, 1, 1, 1, 3, 3, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1016, 1, 1, 1, 1, 1, 2, 0, CAST(N'2020-08-24T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1017, 1, 1, 1, 1, 1, 2, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1018, 1, 1, 1, 1, 1, 2, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1019, 1, 1, 1, 1, 1, 2, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1020, 1, 1, 1, 1, 2, 11, 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1021, 1, 1, 1, 1, 2, 2, 0, CAST(N'2020-08-08T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1022, 1, 1, 1, 1, 1, 2, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1023, 1, 1, 1, 1, 1, 2, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1024, 1, 1, 1, 1, 1, 2, 0, CAST(N'2020-08-07T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1025, 1, 1, 1, 1, 1, 4, 0, CAST(N'2020-08-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1026, 1, 1, 1, 1, 1, 4, 0, CAST(N'2020-08-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1027, 1, 1, 1, 1, 1, 4, 0, CAST(N'2020-08-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1028, 1, 1, 1, 1, 1, 4, 0, CAST(N'2020-08-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1029, 1, 1, 1, 1, 1, 3, 0, CAST(N'2020-08-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1030, 1, 1, 1, 1, 1, 2, 0, CAST(N'2020-08-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1031, 1, 1, 1, 1, 1, 2, 0, CAST(N'2020-08-31T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (1032, 1, 1, 1, 1, 1, 4, 0, CAST(N'2020-09-01T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (2025, 1, 1, 1, 1, 1, 2, 0, CAST(N'2020-09-02T00:00:00.000' AS DateTime))
INSERT [dbo].[tb_race] ([Race_Id], [Event_Id], [Weather_Id], [Surface_Id], [Race_Type_Id], [Class_Id], [Number_Of_Horses], [IsComplete], [Date]) VALUES (3025, 1, 1, 1, 1, 3, 2, 0, CAST(N'2020-09-17T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[tb_race] OFF
INSERT [dbo].[tb_race_horse] ([Race_Id], [Horse_Id], [Weight], [Age], [Trainer_Id], [Jockey_Id], [Position], [DNF], [Clean_Race]) VALUES (1027, 1, 12, 9, 2, 1, NULL, NULL, NULL)
INSERT [dbo].[tb_race_horse] ([Race_Id], [Horse_Id], [Weight], [Age], [Trainer_Id], [Jockey_Id], [Position], [DNF], [Clean_Race]) VALUES (1028, 1, 1, 9, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[tb_race_horse] ([Race_Id], [Horse_Id], [Weight], [Age], [Trainer_Id], [Jockey_Id], [Position], [DNF], [Clean_Race]) VALUES (1029, 1, 4, 9, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[tb_race_horse] ([Race_Id], [Horse_Id], [Weight], [Age], [Trainer_Id], [Jockey_Id], [Position], [DNF], [Clean_Race]) VALUES (1030, 1, 2, 9, 2, 2, NULL, NULL, NULL)
INSERT [dbo].[tb_race_horse] ([Race_Id], [Horse_Id], [Weight], [Age], [Trainer_Id], [Jockey_Id], [Position], [DNF], [Clean_Race]) VALUES (1031, 1, 2, 9, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[tb_race_horse] ([Race_Id], [Horse_Id], [Weight], [Age], [Trainer_Id], [Jockey_Id], [Position], [DNF], [Clean_Race]) VALUES (1031, 2, 3, 6, 2, 2, NULL, NULL, NULL)
INSERT [dbo].[tb_race_horse] ([Race_Id], [Horse_Id], [Weight], [Age], [Trainer_Id], [Jockey_Id], [Position], [DNF], [Clean_Race]) VALUES (1032, 1, 11, 9, 1, 1, NULL, NULL, NULL)
INSERT [dbo].[tb_race_horse] ([Race_Id], [Horse_Id], [Weight], [Age], [Trainer_Id], [Jockey_Id], [Position], [DNF], [Clean_Race]) VALUES (1032, 2, 11, 6, 2, 2, NULL, NULL, NULL)
INSERT [dbo].[tb_race_horse] ([Race_Id], [Horse_Id], [Weight], [Age], [Trainer_Id], [Jockey_Id], [Position], [DNF], [Clean_Race]) VALUES (1032, 4, 12, 8, 4, 4, NULL, NULL, NULL)
INSERT [dbo].[tb_race_horse] ([Race_Id], [Horse_Id], [Weight], [Age], [Trainer_Id], [Jockey_Id], [Position], [DNF], [Clean_Race]) VALUES (1032, 5, 10, 6, 6, 5, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tb_racetype] ON 

INSERT [dbo].[tb_racetype] ([Race_Type_Id], [Race_Type_Description], [Hurdle], [No_Of_Hurdles], [Meters], [furlongs]) VALUES (1, N'2m3f207y', 1, NULL, 2, 3)
SET IDENTITY_INSERT [dbo].[tb_racetype] OFF
SET IDENTITY_INSERT [dbo].[tb_sql_error_log] ON 

INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (1, N'pr_add_horse', N'A horse with that name already exists', CAST(N'2020-08-07T14:01:33.327' AS DateTime))
INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (2, N'pr_add_horse', N'A horse with that name already exists', CAST(N'2020-08-07T14:04:24.143' AS DateTime))
INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (3, N'pr_add_horse', N'A horse with that name already exists', CAST(N'2020-08-07T14:06:00.747' AS DateTime))
INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (1001, N'pr_add_horse', N'A horse with that name already exists', CAST(N'2020-09-01T15:55:35.220' AS DateTime))
INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (1002, N'pr_add_horse', N'A horse with that name already exists', CAST(N'2020-09-01T15:55:39.697' AS DateTime))
INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (1003, N'pr_add_jockey', N'A jockey with that name already exists', CAST(N'2020-09-01T15:59:35.513' AS DateTime))
INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (1004, N'pr_add_jockey', N'A jockey with that name already exists', CAST(N'2020-09-01T15:59:36.970' AS DateTime))
INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (1005, N'pr_add_jockey', N'A jockey with that name already exists', CAST(N'2020-09-01T15:59:37.563' AS DateTime))
INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (1006, N'pr_add_jockey', N'A jockey with that name already exists', CAST(N'2020-09-01T16:00:28.770' AS DateTime))
INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (1007, N'pr_add_horse', N'A horse with that name already exists', CAST(N'2020-09-01T16:02:05.763' AS DateTime))
INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (1008, N'pr_add_jockey', N'A jockey with that name already exists', CAST(N'2020-09-01T16:02:48.067' AS DateTime))
INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (1009, N'pr_add_trainer', N'A trainer with that name already exists', CAST(N'2020-09-01T16:04:51.870' AS DateTime))
INSERT [dbo].[tb_sql_error_log] ([Sql_Error_Id], [Object_Name], [Error], [Date]) VALUES (2001, N'pr_add_weather', N'Weather condition that name already exists', CAST(N'2020-09-17T12:34:29.243' AS DateTime))
SET IDENTITY_INSERT [dbo].[tb_sql_error_log] OFF
SET IDENTITY_INSERT [dbo].[tb_surface] ON 

INSERT [dbo].[tb_surface] ([Surface_Id], [Surface_Description]) VALUES (1, N'Damp in places')
INSERT [dbo].[tb_surface] ([Surface_Id], [Surface_Description]) VALUES (2, N'Tes')
INSERT [dbo].[tb_surface] ([Surface_Id], [Surface_Description]) VALUES (3, N'Testtt')
SET IDENTITY_INSERT [dbo].[tb_surface] OFF
SET IDENTITY_INSERT [dbo].[tb_trainer] ON 

INSERT [dbo].[tb_trainer] ([Trainer_Id], [Trainer_Name]) VALUES (1, N'Micky Hammond')
INSERT [dbo].[tb_trainer] ([Trainer_Id], [Trainer_Name]) VALUES (2, N'Mark Johnston')
INSERT [dbo].[tb_trainer] ([Trainer_Id], [Trainer_Name]) VALUES (3, N'Saeed bin Suroor')
INSERT [dbo].[tb_trainer] ([Trainer_Id], [Trainer_Name]) VALUES (4, N'Jed O''Keefe')
INSERT [dbo].[tb_trainer] ([Trainer_Id], [Trainer_Name]) VALUES (5, N'Tim Easterby')
INSERT [dbo].[tb_trainer] ([Trainer_Id], [Trainer_Name]) VALUES (6, N'Ruth Carr')
INSERT [dbo].[tb_trainer] ([Trainer_Id], [Trainer_Name]) VALUES (7, N'Test')
INSERT [dbo].[tb_trainer] ([Trainer_Id], [Trainer_Name]) VALUES (8, N'Joe Bloggs')
SET IDENTITY_INSERT [dbo].[tb_trainer] OFF
SET IDENTITY_INSERT [dbo].[tb_weather] ON 

INSERT [dbo].[tb_weather] ([Weather_Id], [Weather_Description]) VALUES (1, N'Heavy')
SET IDENTITY_INSERT [dbo].[tb_weather] OFF
ALTER TABLE [dbo].[tb_horse] ADD  CONSTRAINT [DF_tb_horse_IsRetired]  DEFAULT ((0)) FOR [IsRetired]
GO
ALTER TABLE [dbo].[tb_race] ADD  CONSTRAINT [DF_tb_race_IsComplete]  DEFAULT ((0)) FOR [IsComplete]
GO
ALTER TABLE [dbo].[tb_horse]  WITH CHECK ADD  CONSTRAINT [FK_tb_horse_tb_country] FOREIGN KEY([Country])
REFERENCES [dbo].[tb_country] ([Country_Id])
GO
ALTER TABLE [dbo].[tb_horse] CHECK CONSTRAINT [FK_tb_horse_tb_country]
GO
ALTER TABLE [dbo].[tb_race]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_tb_class] FOREIGN KEY([Class_Id])
REFERENCES [dbo].[tb_class] ([Class_Id])
GO
ALTER TABLE [dbo].[tb_race] CHECK CONSTRAINT [FK_tb_race_tb_class]
GO
ALTER TABLE [dbo].[tb_race]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_tb_event] FOREIGN KEY([Event_Id])
REFERENCES [dbo].[tb_event] ([Event_Id])
GO
ALTER TABLE [dbo].[tb_race] CHECK CONSTRAINT [FK_tb_race_tb_event]
GO
ALTER TABLE [dbo].[tb_race]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_tb_racetype] FOREIGN KEY([Race_Type_Id])
REFERENCES [dbo].[tb_racetype] ([Race_Type_Id])
GO
ALTER TABLE [dbo].[tb_race] CHECK CONSTRAINT [FK_tb_race_tb_racetype]
GO
ALTER TABLE [dbo].[tb_race]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_tb_surface] FOREIGN KEY([Surface_Id])
REFERENCES [dbo].[tb_surface] ([Surface_Id])
GO
ALTER TABLE [dbo].[tb_race] CHECK CONSTRAINT [FK_tb_race_tb_surface]
GO
ALTER TABLE [dbo].[tb_race]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_tb_weather] FOREIGN KEY([Weather_Id])
REFERENCES [dbo].[tb_weather] ([Weather_Id])
GO
ALTER TABLE [dbo].[tb_race] CHECK CONSTRAINT [FK_tb_race_tb_weather]
GO
ALTER TABLE [dbo].[tb_race_horse]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_horse_tb_horse] FOREIGN KEY([Horse_Id])
REFERENCES [dbo].[tb_horse] ([Horse_Id])
GO
ALTER TABLE [dbo].[tb_race_horse] CHECK CONSTRAINT [FK_tb_race_horse_tb_horse]
GO
ALTER TABLE [dbo].[tb_race_horse]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_horse_tb_jockey] FOREIGN KEY([Jockey_Id])
REFERENCES [dbo].[tb_jockey] ([Jockey_Id])
GO
ALTER TABLE [dbo].[tb_race_horse] CHECK CONSTRAINT [FK_tb_race_horse_tb_jockey]
GO
ALTER TABLE [dbo].[tb_race_horse]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_horse_tb_race] FOREIGN KEY([Race_Id])
REFERENCES [dbo].[tb_race] ([Race_Id])
GO
ALTER TABLE [dbo].[tb_race_horse] CHECK CONSTRAINT [FK_tb_race_horse_tb_race]
GO
ALTER TABLE [dbo].[tb_race_horse]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_horse_tb_trainer] FOREIGN KEY([Trainer_Id])
REFERENCES [dbo].[tb_trainer] ([Trainer_Id])
GO
ALTER TABLE [dbo].[tb_race_horse] CHECK CONSTRAINT [FK_tb_race_horse_tb_trainer]
GO
/****** Object:  StoredProcedure [dbo].[pr_add_event]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_add_event]        
@EventName varchar(150)      
AS        
SET NOCOUNT ON;        
BEGIN        
IF ((SELECT TOP (1) 1 FROM [dbo].[tb_event] WHERE Event_Name = @EventName) = 1)        
 BEGIN        
  EXEC [dbo].[pr_log_sql_error] '[pr_add_event]','Event with that name already exists' END        
ELSE        
 BEGIN        
 INSERT INTO [tb_event] VALUES        
 (@EventName)        
 END        
END 
GO
/****** Object:  StoredProcedure [dbo].[pr_add_horse]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_add_horse]    
@Horse_Name varchar(50),    
@Date_Of_Birth datetime,    
@Country int    
AS    
SET NOCOUNT ON;    
BEGIN    
IF ((SELECT TOP(1) 1 FROM [dbo].[tb_horse] WHERE Horse_Name = @Horse_Name) = 1)    
 BEGIN    
  EXEC [dbo].[pr_log_sql_error] 'pr_add_horse','A horse with that name already exists'  
 END    
ELSE    
 BEGIN    
 INSERT INTO tb_horse VALUES    
 (@Horse_Name, @Date_Of_Birth, 0, null, @Country)    
 END    
END
GO
/****** Object:  StoredProcedure [dbo].[pr_add_jockey]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_add_jockey]      
@Jockey_Name varchar(150)    
AS      
SET NOCOUNT ON;      
BEGIN      
IF ((SELECT TOP (1) 1 FROM [dbo].[tb_jockey] WHERE Jockey_Name = @Jockey_Name) = 1)      
 BEGIN      
  EXEC [dbo].[pr_log_sql_error] 'pr_add_jockey','A jockey with that name already exists' END      
ELSE      
 BEGIN      
 INSERT INTO tb_jockey VALUES      
 (@Jockey_Name)      
 END      
END  
GO
/****** Object:  StoredProcedure [dbo].[pr_add_race]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_add_race]
@Event_Id int,
@Weather_Id int,
@Surface_Id int,
@Race_Type_Id int,
@Class_Id int,
@Number_Of_Horses int,
@Date DateTime
AS 

SET NOCOUNT ON;

BEGIN

INSERT INTO tb_race VALUES
(@Event_Id, @Weather_Id, @Surface_Id, @Race_Type_Id, @Class_Id, @Number_Of_Horses, 0, @Date)

SELECT Race_Id AS result FROM tb_race WHERE Race_Id = @@Identity;

END
GO
/****** Object:  StoredProcedure [dbo].[pr_add_race_horse]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_add_race_horse]      
@Race_Id int,      
@Horse_Id int,      
@Weight int,
@Age int,
@Trainer_Id int,
@Jockey_Id int
AS      
SET NOCOUNT ON;           
BEGIN      
	INSERT INTO tb_race_horse VALUES      
	(@Race_Id, @Horse_Id, @Weight, @Age, @Trainer_Id, @Jockey_Id, null, null, null)      
END      
GO
/****** Object:  StoredProcedure [dbo].[pr_add_racetype]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_add_racetype]        
@RaceTypeDescription varchar(150),
@IsHurdle bit,
@Meters int,
@Furlongs int,
@No_Of_Hurdles int = null
AS        
SET NOCOUNT ON;        
BEGIN        
IF ((SELECT TOP (1) 1 FROM [dbo].[tb_racetype] WHERE Race_Type_Description = @RaceTypeDescription) = 1)        
 BEGIN        
  EXEC [dbo].[pr_log_sql_error] '[pr_add_racetype]','Race Type Description with that name already exists' END        
ELSE        
 BEGIN        
 INSERT INTO tb_racetype VALUES        
 (@RaceTypeDescription, @IsHurdle, @No_Of_Hurdles, @Meters, @Furlongs)        
 END        
END 
GO
/****** Object:  StoredProcedure [dbo].[pr_add_surface]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_add_surface]        
@Surface varchar(150)      
AS        
SET NOCOUNT ON;        
BEGIN        
IF ((SELECT TOP (1) 1 FROM [dbo].[tb_surface] WHERE Surface_Description = @Surface) = 1)        
 BEGIN        
  EXEC [dbo].[pr_log_sql_error] '[pr_add_surface]','Surface Condition with that name already exists' END        
ELSE        
 BEGIN        
 INSERT INTO [tb_surface] VALUES        
 (@Surface)        
 END        
END
GO
/****** Object:  StoredProcedure [dbo].[pr_add_trainer]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_add_trainer]      
@Trainer_Name varchar(150)    
AS      
SET NOCOUNT ON;      
BEGIN      
IF ((SELECT TOP(1) 1 FROM [dbo].[tb_trainer] WHERE Trainer_Name = @Trainer_Name) = 1)      
 BEGIN      
  EXEC [dbo].[pr_log_sql_error] 'pr_add_trainer','A trainer with that name already exists'   
 END      
ELSE      
 BEGIN      
 INSERT INTO tb_trainer VALUES      
 (@Trainer_Name)      
 END      
END
GO
/****** Object:  StoredProcedure [dbo].[pr_add_weather]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_add_weather]        
@Weather varchar(150)      
AS        
SET NOCOUNT ON;        
BEGIN        
IF ((SELECT TOP (1) 1 FROM [dbo].[tb_weather] WHERE Weather_Description = @Weather) = 1)        
 BEGIN        
  EXEC [dbo].[pr_log_sql_error] 'pr_add_weather','Weather condition that name already exists' END        
ELSE        
 BEGIN        
 INSERT INTO tb_weather VALUES        
 (@Weather)        
 END        
END 
GO
/****** Object:  StoredProcedure [dbo].[pr_get_class]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_class]  
AS  
SET NOCOUNT ON;  
BEGIN  
SELECT Class_Id, Class_Number, Class_Description FROM tb_class  
END
GO
/****** Object:  StoredProcedure [dbo].[pr_get_countries]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_countries]
AS
SET NOCOUNT ON;
BEGIN
SELECT Country_Id, Country_Name, Country_Code FROM tb_country
END
GO
/****** Object:  StoredProcedure [dbo].[pr_get_events]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_get_events]  
AS  
SET NOCOUNT ON;  
BEGIN  
SELECT Event_Id, Event_Name FROM tb_event  
END
GO
/****** Object:  StoredProcedure [dbo].[pr_get_horses]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_horses]  
AS  
SET NOCOUNT ON;  
BEGIN  
SELECT Horse_Id, Horse_Name, Date_Of_Birth, IsRetired, Notes, Age, Country FROM tb_horse
END
GO
/****** Object:  StoredProcedure [dbo].[pr_get_jockeys]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_jockeys]  
AS  
SET NOCOUNT ON;  
BEGIN  
SELECT Jockey_Id, Jockey_Name FROM tb_jockey
END
GO
/****** Object:  StoredProcedure [dbo].[pr_get_race]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_race]
@Race_Id int
AS 

SET NOCOUNT ON;

BEGIN

SELECT 
Race_Id,
Event_Id,
Weather_Id,
Surface_Id,
Race_Type_Id,
Class_Id,
Number_Of_Horses,
IsComplete,
[Date]
 FROM tb_race (NOLOCK) WHERE Race_Id = @Race_Id

END
GO
/****** Object:  StoredProcedure [dbo].[pr_get_race_list]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
 CREATE PROCEDURE [dbo].[pr_get_race_list]
 AS
 SET NOCOUNT ON;
 BEGIN

	SELECT r.Race_Id, r.Date, e.Event_Name, c.Class_Number, rt.Race_Type_Description, r.Number_Of_Horses, r.isComplete
	FROM [dbo].[tb_race] r (NOLOCK)
	RIGHT JOIN [dbo].[tb_race_horse] rh (NOLOCK) ON r.Race_Id = rh.Race_Id
	INNER JOIN [dbo].[tb_event] e (NOLOCK) ON r.Event_Id = e.Event_Id
	INNER JOIN [dbo].[tb_class] c (NOLOCK) ON r.Class_Id = c.Class_Id
	INNER JOIN [dbo].[tb_racetype] rt (NOLOCK) ON r.Race_Type_Id = rt.Race_Type_Id
	GROUP BY r.Race_Id, r.Date ,e.Event_Name, c.Class_Number, rt.Race_Type_Description, r.Number_Of_Horses, r.isComplete
	ORDER BY r.Date DESC
 END
GO
/****** Object:  StoredProcedure [dbo].[pr_get_race_types]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_race_types]  
AS  
SET NOCOUNT ON;  
BEGIN  
SELECT Race_Type_Id, Race_Type_Description, Hurdle,No_Of_Hurdles,Meters,furlongs FROM tb_racetype
END
GO
/****** Object:  StoredProcedure [dbo].[pr_get_surface]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_surface]  
AS  
SET NOCOUNT ON;  
BEGIN  
SELECT Surface_Id, Surface_Description FROM tb_surface  
END
GO
/****** Object:  StoredProcedure [dbo].[pr_get_trainers]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_trainers]  
AS  
SET NOCOUNT ON;  
BEGIN  
SELECT Trainer_Id, Trainer_Name FROM tb_trainer
END
GO
/****** Object:  StoredProcedure [dbo].[pr_get_weather]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_weather]  
AS  
SET NOCOUNT ON;  
BEGIN  
SELECT Weather_Id, Weather_Description FROM tb_weather  
END
GO
/****** Object:  StoredProcedure [dbo].[pr_log_error]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_log_error]  
@Log varchar(MAX),
@Inner varchar(MAX),
@Stack varchar(MAX)
AS  
SET NOCOUNT ON;  
 BEGIN  
 INSERT INTO tb_error_log VALUES  
 (@Log, @Inner, @Stack, GETDATE())  
 END  
GO
/****** Object:  StoredProcedure [dbo].[pr_log_sql_error]    Script Date: 17/09/2020 12:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 CREATE PROCEDURE [dbo].[pr_log_sql_error]  
@Object varchar(MAX),
@Log varchar(MAX)
AS  
SET NOCOUNT ON;  
 BEGIN  
 INSERT INTO tb_sql_error_log VALUES  
 (@Object, @Log, GETDATE())  
 END  
GO
USE [master]
GO
ALTER DATABASE [ProjectPunter] SET  READ_WRITE 
GO
