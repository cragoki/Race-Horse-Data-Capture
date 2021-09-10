USE [master]
GO
/****** Object:  Database [RHDC]    Script Date: 9/10/2021 12:14:17 PM ******/
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
/****** Object:  Table [dbo].[tb_archive_horse]    Script Date: 9/10/2021 12:14:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_archive_horse](
	[archive_id] [int] IDENTITY(1,1) NOT NULL,
	[horse_id] [int] NOT NULL,
	[field_changed] [varchar](50) NOT NULL,
	[old_value] [varchar](50) NOT NULL,
	[new_value] [varchar](50) NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_tb_archive_horse] PRIMARY KEY CLUSTERED 
(
	[archive_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_batch]    Script Date: 9/10/2021 12:14:18 PM ******/
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
/****** Object:  Table [dbo].[tb_course]    Script Date: 9/10/2021 12:14:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_course](
	[course_id] [int] NOT NULL,
	[name] [varchar](150) NOT NULL,
	[country_code] [varchar](6) NOT NULL,
	[all_weather] [bit] NULL,
	[course_url] [varchar](250) NULL,
 CONSTRAINT [PK_tb_course] PRIMARY KEY CLUSTERED 
(
	[course_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_event]    Script Date: 9/10/2021 12:14:18 PM ******/
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
/****** Object:  Table [dbo].[tb_horse]    Script Date: 9/10/2021 12:14:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_horse](
	[horse_id] [int] IDENTITY(1,1) NOT NULL,
	[rp_horse_id] [int] NULL,
	[horse_name] [varchar](150) NOT NULL,
	[dob] [datetime] NULL,
	[horse_url] [varchar](150) NOT NULL,
	[top_speed] [int] NULL,
	[rpr] [int] NULL,
 CONSTRAINT [PK_tb_horse] PRIMARY KEY CLUSTERED 
(
	[horse_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_jockey]    Script Date: 9/10/2021 12:14:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_jockey](
	[jockey_id] [int] NOT NULL,
	[jockey_name] [varchar](150) NOT NULL,
	[jockey_url] [varchar](150) NULL,
 CONSTRAINT [PK_tb_jockey] PRIMARY KEY CLUSTERED 
(
	[jockey_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_race]    Script Date: 9/10/2021 12:14:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_race](
	[race_id] [int] IDENTITY(1,1) NOT NULL,
	[event_id] [int] NOT NULL,
	[race_time] [varchar](20) NOT NULL,
	[rp_race_id] [int] NOT NULL,
	[weather] [varchar](50) NULL,
	[no_of_horses] [int] NULL,
	[going] [varchar](150) NULL,
	[stalls] [varchar](150) NULL,
	[distance] [varchar](12) NULL,
	[race_class] [int] NULL,
	[ages] [varchar](15) NULL,
	[description] [varchar](550) NULL,
	[race_url] [varchar](150) NULL,
 CONSTRAINT [PK_tb_race] PRIMARY KEY CLUSTERED 
(
	[race_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_race_horse]    Script Date: 9/10/2021 12:14:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_race_horse](
	[race_id] [int] NOT NULL,
	[horse_id] [int] NOT NULL,
	[weight] [varchar](15) NULL,
	[age] [int] NULL,
	[trainer_id] [int] NULL,
	[jockey_id] [int] NULL,
	[jockey_weight] [varchar](15) NOT NULL,
	[finished] [bit] NULL,
	[position] [int] NULL,
	[description] [varchar](250) NULL,
	[rp_notes] [varchar](500) NULL,
 CONSTRAINT [PK_tb_race_horse] PRIMARY KEY CLUSTERED 
(
	[race_id] ASC,
	[horse_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_trainer]    Script Date: 9/10/2021 12:14:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_trainer](
	[trainer_id] [int] IDENTITY(1,1) NOT NULL,
	[trainer_name] [varchar](150) NOT NULL,
	[trainer_url] [varchar](150) NULL,
 CONSTRAINT [PK_tb_trainer] PRIMARY KEY CLUSTERED 
(
	[trainer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tb_batch] ([batch_id], [diagnostics], [date]) VALUES (N'7269d755-b82f-45a9-9d3f-54ef66ed2915', N'{"Automator":0,"TimeInitialized":"2021-08-25T14:40:10.9330285+02:00","TimeCompleted":"2021-08-25T14:41:11.7325394+02:00","EventsFiltered":8,"ErrorsEncountered":0,"EllapsedTime":60.7995109}', CAST(N'2021-08-25T14:41:11.773' AS DateTime))
GO
INSERT [dbo].[tb_batch] ([batch_id], [diagnostics], [date]) VALUES (N'e55b28ad-c8cf-4ce6-8796-8a0e5b9464c4', N'{"Automator":0,"TimeInitialized":"2021-08-25T10:26:19.5345263+02:00","TimeCompleted":"2021-08-25T10:26:21.389136+02:00","EventsFiltered":8,"ErrorsEncountered":0,"EllapsedTime":1.8546097}', CAST(N'2021-08-25T10:26:21.523' AS DateTime))
GO
INSERT [dbo].[tb_batch] ([batch_id], [diagnostics], [date]) VALUES (N'927ab4bc-c077-4314-b44e-b3f2bbb3e93b', N'{"Automator":0,"TimeInitialized":"2021-08-13T14:55:47.3434273+02:00","TimeCompleted":"2021-08-13T14:55:49.1296615+02:00","EventsFiltered":7,"ErrorsEncountered":0,"EllapsedTime":1.7862342}', CAST(N'2021-08-13T14:55:49.170' AS DateTime))
GO
INSERT [dbo].[tb_batch] ([batch_id], [diagnostics], [date]) VALUES (N'62eff31b-5d50-4277-876f-b718e6459e1b', N'{"Automator":0,"TimeInitialized":"2021-08-17T09:22:12.6409592+02:00","TimeCompleted":"2021-08-17T09:22:42.6868533+02:00","EventsFiltered":5,"ErrorsEncountered":0,"EllapsedTime":30.0458941}', CAST(N'2021-08-17T09:22:42.737' AS DateTime))
GO
INSERT [dbo].[tb_batch] ([batch_id], [diagnostics], [date]) VALUES (N'24475ba7-95bb-40b8-9a2f-cb9715470db3', N'{"Automator":0,"TimeInitialized":"2021-08-25T13:40:47.1326745+02:00","TimeCompleted":"2021-08-25T13:41:35.6407559+02:00","EventsFiltered":8,"ErrorsEncountered":0,"EllapsedTime":48.5080814}', CAST(N'2021-08-25T13:41:35.680' AS DateTime))
GO
INSERT [dbo].[tb_batch] ([batch_id], [diagnostics], [date]) VALUES (N'8e14a680-4656-4000-adaa-e8c06059be8f', N'{"Automator":0,"TimeInitialized":"2021-09-10T09:01:56.6629209+02:00","TimeCompleted":"2021-09-10T09:02:33.3428836+02:00","EventsFiltered":6,"ErrorsEncountered":0,"EllapsedTime":36.6799627}', CAST(N'2021-09-10T09:02:33.390' AS DateTime))
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (4, N'Bangor-on-Dee', N'GB', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (8, N'Carlisle', N'GB', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (10, N'Catterick', N'GB', 0, N'/profile/course/10/catterick')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (13, N'Chester', N'GB', 0, N'/profile/course/13/chester')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (15, N'Doncaster', N'GB', 0, N'/profile/course/15/doncaster')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (16, N'Musselburgh', N'GB', 0, N'/profile/course/16/musselburgh')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (20, N'Fontwell', N'GB', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (22, N'Hamilton', N'GB', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (30, N'Leicester', N'GB', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (36, N'Newbury', N'GB', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (40, N'Nottingham', N'GB', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (52, N'Salisbury', N'GB', 0, N'/profile/course/52/salisbury')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (54, N'Sandown', N'GB', 0, N'/profile/course/54/sandown')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (80, N'Thirsk', N'GB', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (104, N'Yarmouth', N'GB', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (174, N'Newmarket (July)', N'GB', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (175, N'Ballinrobe', N'IRE', 0, N'/profile/course/175/ballinrobe')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (176, N'Bellewstown', N'IRE', 0, N'/profile/course/176/bellewstown')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (178, N'Curragh', N'IRE', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (197, N'Sligo', N'IRE', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (200, N'Tramore', N'IRE', 0, NULL)
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (219, N'Saint-Cloud', N'FR', 0, N'/profile/course/219/saint-cloud')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (393, N'Lingfield', N'GB', 1, N'/profile/course/393/lingfield-aw')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (513, N'Wolverhampton', N'GB', 1, N'/profile/course/513/wolverhampton-aw')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (1079, N'Kempton', N'GB', 1, N'/profile/course/1079/kempton-aw')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (1321, N'Lingfield', N'ARO', 1, N'/profile/course/1321/lingfield-aw-gb')
GO
SET IDENTITY_INSERT [dbo].[tb_event] ON 
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (266, 16, 0, N'', N'Musselburgh_8/25/2021', N'/racecards/16/musselburgh/2021-08-25', N'musselburgh', N'Flat', 7, N'9c65aebd-7270-4b31-9a14-694bfd918764', CAST(N'2021-08-25T12:50:33.960' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (267, 393, 0, N'Polytrack', N'Lingfield_8/25/2021', N'/racecards/393/lingfield-aw/2021-08-25', N'lingfield-aw', N'Flat', 8, N'9c65aebd-7270-4b31-9a14-694bfd918764', CAST(N'2021-08-25T12:50:33.990' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (268, 176, 0, N'', N'Bellewstown_8/25/2021', N'/racecards/176/bellewstown/2021-08-25', N'bellewstown', N'Jumps', 8, N'9c65aebd-7270-4b31-9a14-694bfd918764', CAST(N'2021-08-25T12:50:34.010' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (269, 513, 0, N'Tapeta', N'Wolverhampton_8/25/2021', N'/racecards/513/wolverhampton-aw/2021-08-25', N'wolverhampton-aw', N'Flat', 8, N'9c65aebd-7270-4b31-9a14-694bfd918764', CAST(N'2021-08-25T12:50:34.023' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (270, 1079, 0, N'Polytrack', N'Kempton_8/25/2021', N'/racecards/1079/kempton-aw/2021-08-25', N'kempton-aw', N'Flat', 7, N'9c65aebd-7270-4b31-9a14-694bfd918764', CAST(N'2021-08-25T12:50:34.047' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (271, 20, 1, N'', N'Fontwell_8/25/2021', N'/racecards/20/fontwell/2021-08-25', N'fontwell', N'Jumps', 7, N'9c65aebd-7270-4b31-9a14-694bfd918764', CAST(N'2021-08-25T12:50:34.070' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (272, 1321, 0, N'Dirt', N'Lingfield_8/25/2021', N'/racecards/1321/lingfield-aw-gb/2021-08-25', N'lingfield-aw-gb', N'Flat', 1, N'9c65aebd-7270-4b31-9a14-694bfd918764', CAST(N'2021-08-25T12:50:34.083' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (273, 10, 0, N'', N'Catterick_8/25/2021', N'/racecards/10/catterick/2021-08-25', N'catterick', N'Flat', 8, N'2d95b5dc-8013-4ac9-bc7e-e7d5ec0198e8', CAST(N'2021-08-25T12:57:21.000' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (274, 16, 0, N'', N'Musselburgh_8/25/2021', N'/racecards/16/musselburgh/2021-08-25', N'musselburgh', N'Flat', 7, N'2d95b5dc-8013-4ac9-bc7e-e7d5ec0198e8', CAST(N'2021-08-25T12:57:21.240' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (275, 393, 0, N'Polytrack', N'Lingfield_8/25/2021', N'/racecards/393/lingfield-aw/2021-08-25', N'lingfield-aw', N'Flat', 8, N'2d95b5dc-8013-4ac9-bc7e-e7d5ec0198e8', CAST(N'2021-08-25T12:57:21.277' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (276, 176, 0, N'', N'Bellewstown_8/25/2021', N'/racecards/176/bellewstown/2021-08-25', N'bellewstown', N'Jumps', 8, N'2d95b5dc-8013-4ac9-bc7e-e7d5ec0198e8', CAST(N'2021-08-25T12:57:21.290' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (277, 513, 0, N'Tapeta', N'Wolverhampton_8/25/2021', N'/racecards/513/wolverhampton-aw/2021-08-25', N'wolverhampton-aw', N'Flat', 8, N'2d95b5dc-8013-4ac9-bc7e-e7d5ec0198e8', CAST(N'2021-08-25T12:57:21.320' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (278, 1079, 0, N'Polytrack', N'Kempton_8/25/2021', N'/racecards/1079/kempton-aw/2021-08-25', N'kempton-aw', N'Flat', 7, N'2d95b5dc-8013-4ac9-bc7e-e7d5ec0198e8', CAST(N'2021-08-25T12:57:21.337' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (279, 20, 1, N'', N'Fontwell_8/25/2021', N'/racecards/20/fontwell/2021-08-25', N'fontwell', N'Jumps', 7, N'2d95b5dc-8013-4ac9-bc7e-e7d5ec0198e8', CAST(N'2021-08-25T12:57:21.357' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (280, 1321, 0, N'Dirt', N'Lingfield_8/25/2021', N'/racecards/1321/lingfield-aw-gb/2021-08-25', N'lingfield-aw-gb', N'Flat', 1, N'2d95b5dc-8013-4ac9-bc7e-e7d5ec0198e8', CAST(N'2021-08-25T12:57:21.377' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (281, 10, 0, N'', N'Catterick_8/25/2021', N'/racecards/10/catterick/2021-08-25', N'catterick', N'Flat', 8, N'688fdf45-1676-4206-9184-de6d6f4f2ef6', CAST(N'2021-08-25T13:29:49.830' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (282, 16, 0, N'', N'Musselburgh_8/25/2021', N'/racecards/16/musselburgh/2021-08-25', N'musselburgh', N'Flat', 7, N'688fdf45-1676-4206-9184-de6d6f4f2ef6', CAST(N'2021-08-25T13:29:50.087' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (283, 393, 0, N'Polytrack', N'Lingfield_8/25/2021', N'/racecards/393/lingfield-aw/2021-08-25', N'lingfield-aw', N'Flat', 8, N'688fdf45-1676-4206-9184-de6d6f4f2ef6', CAST(N'2021-08-25T13:29:50.107' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (284, 176, 0, N'', N'Bellewstown_8/25/2021', N'/racecards/176/bellewstown/2021-08-25', N'bellewstown', N'Jumps', 8, N'688fdf45-1676-4206-9184-de6d6f4f2ef6', CAST(N'2021-08-25T13:29:50.130' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (285, 513, 0, N'Tapeta', N'Wolverhampton_8/25/2021', N'/racecards/513/wolverhampton-aw/2021-08-25', N'wolverhampton-aw', N'Flat', 8, N'688fdf45-1676-4206-9184-de6d6f4f2ef6', CAST(N'2021-08-25T13:29:50.150' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (286, 1079, 0, N'Polytrack', N'Kempton_8/25/2021', N'/racecards/1079/kempton-aw/2021-08-25', N'kempton-aw', N'Flat', 7, N'688fdf45-1676-4206-9184-de6d6f4f2ef6', CAST(N'2021-08-25T13:29:50.163' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (287, 20, 1, N'', N'Fontwell_8/25/2021', N'/racecards/20/fontwell/2021-08-25', N'fontwell', N'Jumps', 7, N'688fdf45-1676-4206-9184-de6d6f4f2ef6', CAST(N'2021-08-25T13:29:50.187' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (288, 1321, 0, N'Dirt', N'Lingfield_8/25/2021', N'/racecards/1321/lingfield-aw-gb/2021-08-25', N'lingfield-aw-gb', N'Flat', 1, N'688fdf45-1676-4206-9184-de6d6f4f2ef6', CAST(N'2021-08-25T13:29:50.213' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (289, 10, 0, N'', N'Catterick_8/25/2021', N'/racecards/10/catterick/2021-08-25', N'catterick', N'Flat', 8, N'34ab3d8a-ff3c-4a99-889b-8939549aa1ec', CAST(N'2021-08-25T13:36:26.430' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (290, 16, 0, N'', N'Musselburgh_8/25/2021', N'/racecards/16/musselburgh/2021-08-25', N'musselburgh', N'Flat', 7, N'34ab3d8a-ff3c-4a99-889b-8939549aa1ec', CAST(N'2021-08-25T13:36:26.690' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (291, 393, 0, N'Polytrack', N'Lingfield_8/25/2021', N'/racecards/393/lingfield-aw/2021-08-25', N'lingfield-aw', N'Flat', 8, N'34ab3d8a-ff3c-4a99-889b-8939549aa1ec', CAST(N'2021-08-25T13:36:26.723' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (292, 176, 0, N'', N'Bellewstown_8/25/2021', N'/racecards/176/bellewstown/2021-08-25', N'bellewstown', N'Jumps', 8, N'34ab3d8a-ff3c-4a99-889b-8939549aa1ec', CAST(N'2021-08-25T13:36:26.747' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (293, 513, 0, N'Tapeta', N'Wolverhampton_8/25/2021', N'/racecards/513/wolverhampton-aw/2021-08-25', N'wolverhampton-aw', N'Flat', 8, N'34ab3d8a-ff3c-4a99-889b-8939549aa1ec', CAST(N'2021-08-25T13:36:26.760' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (294, 1079, 0, N'Polytrack', N'Kempton_8/25/2021', N'/racecards/1079/kempton-aw/2021-08-25', N'kempton-aw', N'Flat', 7, N'34ab3d8a-ff3c-4a99-889b-8939549aa1ec', CAST(N'2021-08-25T13:36:26.780' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (295, 20, 1, N'', N'Fontwell_8/25/2021', N'/racecards/20/fontwell/2021-08-25', N'fontwell', N'Jumps', 7, N'34ab3d8a-ff3c-4a99-889b-8939549aa1ec', CAST(N'2021-08-25T13:36:26.803' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (296, 1321, 0, N'Dirt', N'Lingfield_8/25/2021', N'/racecards/1321/lingfield-aw-gb/2021-08-25', N'lingfield-aw-gb', N'Flat', 1, N'34ab3d8a-ff3c-4a99-889b-8939549aa1ec', CAST(N'2021-08-25T13:36:26.823' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (297, 10, 0, N'', N'Catterick_8/25/2021', N'/racecards/10/catterick/2021-08-25', N'catterick', N'Flat', 8, N'24475ba7-95bb-40b8-9a2f-cb9715470db3', CAST(N'2021-08-25T13:40:48.677' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (298, 16, 0, N'', N'Musselburgh_8/25/2021', N'/racecards/16/musselburgh/2021-08-25', N'musselburgh', N'Flat', 7, N'24475ba7-95bb-40b8-9a2f-cb9715470db3', CAST(N'2021-08-25T13:40:48.900' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (299, 393, 0, N'Polytrack', N'Lingfield_8/25/2021', N'/racecards/393/lingfield-aw/2021-08-25', N'lingfield-aw', N'Flat', 8, N'24475ba7-95bb-40b8-9a2f-cb9715470db3', CAST(N'2021-08-25T13:40:48.943' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (300, 176, 0, N'', N'Bellewstown_8/25/2021', N'/racecards/176/bellewstown/2021-08-25', N'bellewstown', N'Jumps', 8, N'24475ba7-95bb-40b8-9a2f-cb9715470db3', CAST(N'2021-08-25T13:40:48.963' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (301, 513, 0, N'Tapeta', N'Wolverhampton_8/25/2021', N'/racecards/513/wolverhampton-aw/2021-08-25', N'wolverhampton-aw', N'Flat', 8, N'24475ba7-95bb-40b8-9a2f-cb9715470db3', CAST(N'2021-08-25T13:40:48.980' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (302, 1079, 0, N'Polytrack', N'Kempton_8/25/2021', N'/racecards/1079/kempton-aw/2021-08-25', N'kempton-aw', N'Flat', 7, N'24475ba7-95bb-40b8-9a2f-cb9715470db3', CAST(N'2021-08-25T13:40:49.007' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (303, 20, 1, N'', N'Fontwell_8/25/2021', N'/racecards/20/fontwell/2021-08-25', N'fontwell', N'Jumps', 7, N'24475ba7-95bb-40b8-9a2f-cb9715470db3', CAST(N'2021-08-25T13:40:49.027' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (304, 1321, 0, N'Dirt', N'Lingfield_8/25/2021', N'/racecards/1321/lingfield-aw-gb/2021-08-25', N'lingfield-aw-gb', N'Flat', 1, N'24475ba7-95bb-40b8-9a2f-cb9715470db3', CAST(N'2021-08-25T13:40:49.043' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (305, 10, 0, N'', N'Catterick_8/25/2021', N'/racecards/10/catterick/2021-08-25', N'catterick', N'Flat', 8, N'd85d00ff-f927-4ef3-a099-b9513de5fb5a', CAST(N'2021-08-25T14:01:49.213' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (306, 16, 0, N'', N'Musselburgh_8/25/2021', N'/racecards/16/musselburgh/2021-08-25', N'musselburgh', N'Flat', 7, N'd85d00ff-f927-4ef3-a099-b9513de5fb5a', CAST(N'2021-08-25T14:01:49.450' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (307, 393, 0, N'Polytrack', N'Lingfield_8/25/2021', N'/racecards/393/lingfield-aw/2021-08-25', N'lingfield-aw', N'Flat', 8, N'd85d00ff-f927-4ef3-a099-b9513de5fb5a', CAST(N'2021-08-25T14:01:49.487' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (308, 176, 0, N'', N'Bellewstown_8/25/2021', N'/racecards/176/bellewstown/2021-08-25', N'bellewstown', N'Jumps', 8, N'd85d00ff-f927-4ef3-a099-b9513de5fb5a', CAST(N'2021-08-25T14:01:49.520' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (309, 513, 0, N'Tapeta', N'Wolverhampton_8/25/2021', N'/racecards/513/wolverhampton-aw/2021-08-25', N'wolverhampton-aw', N'Flat', 8, N'd85d00ff-f927-4ef3-a099-b9513de5fb5a', CAST(N'2021-08-25T14:01:49.543' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (310, 1079, 0, N'Polytrack', N'Kempton_8/25/2021', N'/racecards/1079/kempton-aw/2021-08-25', N'kempton-aw', N'Flat', 7, N'd85d00ff-f927-4ef3-a099-b9513de5fb5a', CAST(N'2021-08-25T14:01:49.560' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (311, 20, 1, N'', N'Fontwell_8/25/2021', N'/racecards/20/fontwell/2021-08-25', N'fontwell', N'Jumps', 7, N'd85d00ff-f927-4ef3-a099-b9513de5fb5a', CAST(N'2021-08-25T14:01:49.590' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (312, 1321, 0, N'Dirt', N'Lingfield_8/25/2021', N'/racecards/1321/lingfield-aw-gb/2021-08-25', N'lingfield-aw-gb', N'Flat', 1, N'd85d00ff-f927-4ef3-a099-b9513de5fb5a', CAST(N'2021-08-25T14:01:49.617' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (313, 10, 0, N'', N'Catterick_8/25/2021', N'/racecards/10/catterick/2021-08-25', N'catterick', N'Flat', 8, N'f96b89c1-dafb-4dd0-8e2f-78734356d715', CAST(N'2021-08-25T14:05:39.843' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (314, 16, 0, N'', N'Musselburgh_8/25/2021', N'/racecards/16/musselburgh/2021-08-25', N'musselburgh', N'Flat', 7, N'f96b89c1-dafb-4dd0-8e2f-78734356d715', CAST(N'2021-08-25T14:05:40.073' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (315, 393, 0, N'Polytrack', N'Lingfield_8/25/2021', N'/racecards/393/lingfield-aw/2021-08-25', N'lingfield-aw', N'Flat', 8, N'f96b89c1-dafb-4dd0-8e2f-78734356d715', CAST(N'2021-08-25T14:05:40.107' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (316, 176, 0, N'', N'Bellewstown_8/25/2021', N'/racecards/176/bellewstown/2021-08-25', N'bellewstown', N'Jumps', 8, N'f96b89c1-dafb-4dd0-8e2f-78734356d715', CAST(N'2021-08-25T14:05:40.130' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (317, 513, 0, N'Tapeta', N'Wolverhampton_8/25/2021', N'/racecards/513/wolverhampton-aw/2021-08-25', N'wolverhampton-aw', N'Flat', 8, N'f96b89c1-dafb-4dd0-8e2f-78734356d715', CAST(N'2021-08-25T14:05:40.143' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (318, 1079, 0, N'Polytrack', N'Kempton_8/25/2021', N'/racecards/1079/kempton-aw/2021-08-25', N'kempton-aw', N'Flat', 7, N'f96b89c1-dafb-4dd0-8e2f-78734356d715', CAST(N'2021-08-25T14:05:40.167' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (319, 20, 1, N'', N'Fontwell_8/25/2021', N'/racecards/20/fontwell/2021-08-25', N'fontwell', N'Jumps', 7, N'f96b89c1-dafb-4dd0-8e2f-78734356d715', CAST(N'2021-08-25T14:05:40.187' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (320, 1321, 0, N'Dirt', N'Lingfield_8/25/2021', N'/racecards/1321/lingfield-aw-gb/2021-08-25', N'lingfield-aw-gb', N'Flat', 1, N'f96b89c1-dafb-4dd0-8e2f-78734356d715', CAST(N'2021-08-25T14:05:40.203' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (321, 10, 0, N'', N'Catterick_8/25/2021', N'/racecards/10/catterick/2021-08-25', N'catterick', N'Flat', 8, N'9b98cd5f-68c9-4aa1-b9c3-0852c8f07744', CAST(N'2021-08-25T14:06:19.797' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (322, 16, 0, N'', N'Musselburgh_8/25/2021', N'/racecards/16/musselburgh/2021-08-25', N'musselburgh', N'Flat', 7, N'9b98cd5f-68c9-4aa1-b9c3-0852c8f07744', CAST(N'2021-08-25T14:06:20.033' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (323, 393, 0, N'Polytrack', N'Lingfield_8/25/2021', N'/racecards/393/lingfield-aw/2021-08-25', N'lingfield-aw', N'Flat', 8, N'9b98cd5f-68c9-4aa1-b9c3-0852c8f07744', CAST(N'2021-08-25T14:06:20.060' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (324, 176, 0, N'', N'Bellewstown_8/25/2021', N'/racecards/176/bellewstown/2021-08-25', N'bellewstown', N'Jumps', 8, N'9b98cd5f-68c9-4aa1-b9c3-0852c8f07744', CAST(N'2021-08-25T14:06:20.083' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (325, 513, 0, N'Tapeta', N'Wolverhampton_8/25/2021', N'/racecards/513/wolverhampton-aw/2021-08-25', N'wolverhampton-aw', N'Flat', 8, N'9b98cd5f-68c9-4aa1-b9c3-0852c8f07744', CAST(N'2021-08-25T14:06:20.100' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (326, 1079, 0, N'Polytrack', N'Kempton_8/25/2021', N'/racecards/1079/kempton-aw/2021-08-25', N'kempton-aw', N'Flat', 7, N'9b98cd5f-68c9-4aa1-b9c3-0852c8f07744', CAST(N'2021-08-25T14:06:20.133' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (327, 20, 1, N'', N'Fontwell_8/25/2021', N'/racecards/20/fontwell/2021-08-25', N'fontwell', N'Jumps', 7, N'9b98cd5f-68c9-4aa1-b9c3-0852c8f07744', CAST(N'2021-08-25T14:06:20.163' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (328, 1321, 0, N'Dirt', N'Lingfield_8/25/2021', N'/racecards/1321/lingfield-aw-gb/2021-08-25', N'lingfield-aw-gb', N'Flat', 1, N'9b98cd5f-68c9-4aa1-b9c3-0852c8f07744', CAST(N'2021-08-25T14:06:20.177' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (329, 10, 0, N'', N'Catterick_8/25/2021', N'/racecards/10/catterick/2021-08-25', N'catterick', N'Flat', 8, N'a210c27a-dd83-460f-b94d-86355c65a36e', CAST(N'2021-08-25T14:22:50.157' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (330, 16, 0, N'', N'Musselburgh_8/25/2021', N'/racecards/16/musselburgh/2021-08-25', N'musselburgh', N'Flat', 7, N'a210c27a-dd83-460f-b94d-86355c65a36e', CAST(N'2021-08-25T14:22:50.400' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (331, 393, 0, N'Polytrack', N'Lingfield_8/25/2021', N'/racecards/393/lingfield-aw/2021-08-25', N'lingfield-aw', N'Flat', 8, N'a210c27a-dd83-460f-b94d-86355c65a36e', CAST(N'2021-08-25T14:22:50.450' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (332, 176, 0, N'', N'Bellewstown_8/25/2021', N'/racecards/176/bellewstown/2021-08-25', N'bellewstown', N'Jumps', 8, N'a210c27a-dd83-460f-b94d-86355c65a36e', CAST(N'2021-08-25T14:22:50.477' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (333, 513, 0, N'Tapeta', N'Wolverhampton_8/25/2021', N'/racecards/513/wolverhampton-aw/2021-08-25', N'wolverhampton-aw', N'Flat', 8, N'a210c27a-dd83-460f-b94d-86355c65a36e', CAST(N'2021-08-25T14:22:50.493' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (334, 1079, 0, N'Polytrack', N'Kempton_8/25/2021', N'/racecards/1079/kempton-aw/2021-08-25', N'kempton-aw', N'Flat', 7, N'a210c27a-dd83-460f-b94d-86355c65a36e', CAST(N'2021-08-25T14:22:50.513' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (335, 20, 1, N'', N'Fontwell_8/25/2021', N'/racecards/20/fontwell/2021-08-25', N'fontwell', N'Jumps', 7, N'a210c27a-dd83-460f-b94d-86355c65a36e', CAST(N'2021-08-25T14:22:50.530' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (336, 1321, 0, N'Dirt', N'Lingfield_8/25/2021', N'/racecards/1321/lingfield-aw-gb/2021-08-25', N'lingfield-aw-gb', N'Flat', 1, N'a210c27a-dd83-460f-b94d-86355c65a36e', CAST(N'2021-08-25T14:22:50.557' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (337, 10, 0, N'', N'Catterick_8/25/2021', N'/racecards/10/catterick/2021-08-25', N'catterick', N'Flat', 8, N'346e3a57-2e74-4faf-875a-d2b6a6426b10', CAST(N'2021-08-25T14:26:33.953' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (338, 16, 0, N'', N'Musselburgh_8/25/2021', N'/racecards/16/musselburgh/2021-08-25', N'musselburgh', N'Flat', 7, N'346e3a57-2e74-4faf-875a-d2b6a6426b10', CAST(N'2021-08-25T14:26:34.190' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (339, 393, 0, N'Polytrack', N'Lingfield_8/25/2021', N'/racecards/393/lingfield-aw/2021-08-25', N'lingfield-aw', N'Flat', 8, N'346e3a57-2e74-4faf-875a-d2b6a6426b10', CAST(N'2021-08-25T14:26:34.273' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (340, 176, 0, N'', N'Bellewstown_8/25/2021', N'/racecards/176/bellewstown/2021-08-25', N'bellewstown', N'Jumps', 8, N'346e3a57-2e74-4faf-875a-d2b6a6426b10', CAST(N'2021-08-25T14:26:34.313' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (341, 513, 0, N'Tapeta', N'Wolverhampton_8/25/2021', N'/racecards/513/wolverhampton-aw/2021-08-25', N'wolverhampton-aw', N'Flat', 8, N'346e3a57-2e74-4faf-875a-d2b6a6426b10', CAST(N'2021-08-25T14:26:34.340' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (342, 1079, 0, N'Polytrack', N'Kempton_8/25/2021', N'/racecards/1079/kempton-aw/2021-08-25', N'kempton-aw', N'Flat', 7, N'346e3a57-2e74-4faf-875a-d2b6a6426b10', CAST(N'2021-08-25T14:26:34.363' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (343, 20, 1, N'', N'Fontwell_8/25/2021', N'/racecards/20/fontwell/2021-08-25', N'fontwell', N'Jumps', 7, N'346e3a57-2e74-4faf-875a-d2b6a6426b10', CAST(N'2021-08-25T14:26:34.380' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (344, 1321, 0, N'Dirt', N'Lingfield_8/25/2021', N'/racecards/1321/lingfield-aw-gb/2021-08-25', N'lingfield-aw-gb', N'Flat', 1, N'346e3a57-2e74-4faf-875a-d2b6a6426b10', CAST(N'2021-08-25T14:26:34.403' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (345, 10, 0, N'', N'Catterick_8/25/2021', N'/racecards/10/catterick/2021-08-25', N'catterick', N'Flat', 8, N'7269d755-b82f-45a9-9d3f-54ef66ed2915', CAST(N'2021-08-25T14:40:12.570' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (346, 16, 0, N'', N'Musselburgh_8/25/2021', N'/racecards/16/musselburgh/2021-08-25', N'musselburgh', N'Flat', 7, N'7269d755-b82f-45a9-9d3f-54ef66ed2915', CAST(N'2021-08-25T14:40:12.813' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (347, 393, 0, N'Polytrack', N'Lingfield_8/25/2021', N'/racecards/393/lingfield-aw/2021-08-25', N'lingfield-aw', N'Flat', 8, N'7269d755-b82f-45a9-9d3f-54ef66ed2915', CAST(N'2021-08-25T14:40:12.843' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (348, 176, 0, N'', N'Bellewstown_8/25/2021', N'/racecards/176/bellewstown/2021-08-25', N'bellewstown', N'Jumps', 8, N'7269d755-b82f-45a9-9d3f-54ef66ed2915', CAST(N'2021-08-25T14:40:12.867' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (349, 513, 0, N'Tapeta', N'Wolverhampton_8/25/2021', N'/racecards/513/wolverhampton-aw/2021-08-25', N'wolverhampton-aw', N'Flat', 8, N'7269d755-b82f-45a9-9d3f-54ef66ed2915', CAST(N'2021-08-25T14:40:12.880' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (350, 1079, 0, N'Polytrack', N'Kempton_8/25/2021', N'/racecards/1079/kempton-aw/2021-08-25', N'kempton-aw', N'Flat', 7, N'7269d755-b82f-45a9-9d3f-54ef66ed2915', CAST(N'2021-08-25T14:40:12.903' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (351, 20, 1, N'', N'Fontwell_8/25/2021', N'/racecards/20/fontwell/2021-08-25', N'fontwell', N'Jumps', 7, N'7269d755-b82f-45a9-9d3f-54ef66ed2915', CAST(N'2021-08-25T14:40:12.923' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (352, 1321, 0, N'Dirt', N'Lingfield_8/25/2021', N'/racecards/1321/lingfield-aw-gb/2021-08-25', N'lingfield-aw-gb', N'Flat', 1, N'7269d755-b82f-45a9-9d3f-54ef66ed2915', CAST(N'2021-08-25T14:40:12.940' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (353, 54, 0, N'', N'Sandown_9/10/2021', N'/racecards/54/sandown/2021-09-10', N'sandown', N'Flat', 7, N'8e14a680-4656-4000-adaa-e8c06059be8f', CAST(N'2021-09-10T09:01:58.623' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (354, 15, 0, N'', N'Doncaster_9/10/2021', N'/racecards/15/doncaster/2021-09-10', N'doncaster', N'Flat', 7, N'8e14a680-4656-4000-adaa-e8c06059be8f', CAST(N'2021-09-10T09:01:58.770' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (355, 13, 0, N'', N'Chester_9/10/2021', N'/racecards/13/chester/2021-09-10', N'chester', N'Flat', 7, N'8e14a680-4656-4000-adaa-e8c06059be8f', CAST(N'2021-09-10T09:01:58.793' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (356, 175, 0, N'', N'Ballinrobe_9/10/2021', N'/racecards/175/ballinrobe/2021-09-10', N'ballinrobe', N'Jumps', 8, N'8e14a680-4656-4000-adaa-e8c06059be8f', CAST(N'2021-09-10T09:01:58.823' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (357, 52, 0, N'', N'Salisbury_9/10/2021', N'/racecards/52/salisbury/2021-09-10', N'salisbury', N'Flat', 6, N'8e14a680-4656-4000-adaa-e8c06059be8f', CAST(N'2021-09-10T09:01:58.850' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (358, 219, 0, N'', N'Saint-Cloud_9/10/2021', N'/racecards/219/saint-cloud/2021-09-10', N'saint-cloud', N'Flat', 1, N'8e14a680-4656-4000-adaa-e8c06059be8f', CAST(N'2021-09-10T09:01:58.873' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (365, 54, 0, N'', N'Sandown_9/10/2021', N'/racecards/54/sandown/2021-09-10', N'sandown', N'Flat', 7, N'21bfad9e-2b69-4f72-938e-abfe106e4c8d', CAST(N'2021-09-10T10:48:26.277' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (366, 15, 0, N'', N'Doncaster_9/10/2021', N'/racecards/15/doncaster/2021-09-10', N'doncaster', N'Flat', 7, N'21bfad9e-2b69-4f72-938e-abfe106e4c8d', CAST(N'2021-09-10T10:48:26.543' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (367, 13, 0, N'', N'Chester_9/10/2021', N'/racecards/13/chester/2021-09-10', N'chester', N'Flat', 7, N'21bfad9e-2b69-4f72-938e-abfe106e4c8d', CAST(N'2021-09-10T10:48:26.577' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (368, 175, 0, N'', N'Ballinrobe_9/10/2021', N'/racecards/175/ballinrobe/2021-09-10', N'ballinrobe', N'Jumps', 8, N'21bfad9e-2b69-4f72-938e-abfe106e4c8d', CAST(N'2021-09-10T10:48:26.603' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (369, 52, 0, N'', N'Salisbury_9/10/2021', N'/racecards/52/salisbury/2021-09-10', N'salisbury', N'Flat', 6, N'21bfad9e-2b69-4f72-938e-abfe106e4c8d', CAST(N'2021-09-10T10:48:26.617' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (370, 219, 0, N'', N'Saint-Cloud_9/10/2021', N'/racecards/219/saint-cloud/2021-09-10', N'saint-cloud', N'Flat', 1, N'21bfad9e-2b69-4f72-938e-abfe106e4c8d', CAST(N'2021-09-10T10:48:26.637' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (371, 54, 0, N'', N'Sandown_9/10/2021', N'/racecards/54/sandown/2021-09-10', N'sandown', N'Flat', 7, N'132a1f06-f5c6-4097-9bb0-73dcbcb3d7cc', CAST(N'2021-09-10T12:09:40.273' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (372, 15, 0, N'', N'Doncaster_9/10/2021', N'/racecards/15/doncaster/2021-09-10', N'doncaster', N'Flat', 7, N'132a1f06-f5c6-4097-9bb0-73dcbcb3d7cc', CAST(N'2021-09-10T12:09:40.513' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (373, 13, 0, N'', N'Chester_9/10/2021', N'/racecards/13/chester/2021-09-10', N'chester', N'Flat', 7, N'132a1f06-f5c6-4097-9bb0-73dcbcb3d7cc', CAST(N'2021-09-10T12:09:40.540' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (374, 175, 0, N'', N'Ballinrobe_9/10/2021', N'/racecards/175/ballinrobe/2021-09-10', N'ballinrobe', N'Jumps', 8, N'132a1f06-f5c6-4097-9bb0-73dcbcb3d7cc', CAST(N'2021-09-10T12:09:40.563' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (375, 52, 0, N'', N'Salisbury_9/10/2021', N'/racecards/52/salisbury/2021-09-10', N'salisbury', N'Flat', 6, N'132a1f06-f5c6-4097-9bb0-73dcbcb3d7cc', CAST(N'2021-09-10T12:09:40.587' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (376, 219, 0, N'', N'Saint-Cloud_9/10/2021', N'/racecards/219/saint-cloud/2021-09-10', N'saint-cloud', N'Flat', 1, N'132a1f06-f5c6-4097-9bb0-73dcbcb3d7cc', CAST(N'2021-09-10T12:09:40.607' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tb_event] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_race] ON 
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (95, 345, N'13:00', 789997, N'(Partly cloudy)', 13, N'Good', N'(STALLS Inside)', N'5f', 5, N'

(2yo)
', N'
                                    Oops A Daisy Florists EBF Restricted Maiden Stakes (For horses in Bands C and D) (GBB Race)                                ', N'/racecards/10/catterick/2021-08-25/789997')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (96, 345, N'13:30', 790000, N'(Partly cloudy)', 11, N'Good', N'(STALLS Inside)', N'1m5f192y', 6, N'

(3yo+0-65)
', N'
                                    Terry Johnson Handicap                                ', N'/racecards/10/catterick/2021-08-25/790000')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (97, 345, N'14:00', 790002, N'(Partly cloudy)', 8, N'Good', N'(STALLS Inside)', N'5f212y', 6, N'

(2yo0-60)
', N'
                                    holidayathome.co.uk Nursery Handicap                                ', N'/racecards/10/catterick/2021-08-25/790002')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (98, 345, N'14:30', 789996, N'(Partly cloudy)', 8, N'Good', N'(STALLS Inside)', N'5f212y', 4, N'

(3yo+0-85)
', N'
                                    Fascination By Cherished Handicap                                ', N'/racecards/10/catterick/2021-08-25/789996')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (99, 345, N'15:00', 790001, N'(Partly cloudy)', 7, N'Good', N'(STALLS Inside)', N'5f212y', 6, N'

(3yo+0-60)
', N'
                                    racingtv.com Handicap (Div I)                                ', N'/racecards/10/catterick/2021-08-25/790001')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (100, 345, N'15:30', 792156, N'(Partly cloudy)', 9, N'Good', N'(STALLS Inside)', N'5f212y', 6, N'

(3yo+0-60)
', N'
                                    racingtv.com Handicap (Div II)                                ', N'/racecards/10/catterick/2021-08-25/792156')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (101, 345, N'16:00', 789998, N'(Partly cloudy)', 9, N'Good', N'(STALLS Inside)', N'7f6y', 5, N'

(3yo+)
', N'
                                    Millbry Hill Country Store Fillies'' Novice Stakes                                ', N'/racecards/10/catterick/2021-08-25/789998')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (102, 345, N'16:35', 789999, N'(Partly cloudy)', 12, N'Good', N'(STALLS Inside)', N'7f6y', 5, N'

(3yo+0-70)
', N'
                                    Racing Again 7th September Handicap                                ', N'/racecards/10/catterick/2021-08-25/789999')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (103, 346, N'13:15', 790003, N'(Partly cloudy)', 5, N'Good', N'(STALLS Inside)', N'7f33y', 4, N'

(2yo)
', N'
                                    British Stallion Studs EBF Maiden Stakes (GBB Race) (IRE Incentive Race)                                ', N'/racecards/16/musselburgh/2021-08-25/790003')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (104, 346, N'13:45', 790007, N'(Partly cloudy)', 5, N'Good', N'(STALLS Inside)', N'1m4f104y', 6, N'

(3-5yo0-65)
', N'
                                    100% Racing TV Profits Back To Racing Classified Selling Stakes                                ', N'/racecards/16/musselburgh/2021-08-25/790007')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (105, 346, N'14:20', 790008, N'(Partly cloudy)', 8, N'Good', N'(STALLS Inside)', N'1m208y', 6, N'

(3yo0-55)
', N'
                                    Racing TV Profits Returned To Racing Handicap                                ', N'/racecards/16/musselburgh/2021-08-25/790008')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (106, 346, N'14:50', 790004, N'(Partly cloudy)', 6, N'Good', N'(STALLS Stands'' side)', N'5f1y', 4, N'

(4yo+0-85)
', N'
                                    Visit racingtv.com Handicap                                ', N'/racecards/16/musselburgh/2021-08-25/790004')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (107, 346, N'15:20', 790005, N'(Partly cloudy)', 7, N'Good', N'(STALLS Inside)', N'2m2f28y', 6, N'

(4yo+0-65)
', N'
                                    Every Race Live On Racing TV Handicap                                ', N'/racecards/16/musselburgh/2021-08-25/790005')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (108, 346, N'15:50', 790006, N'(Partly cloudy)', 7, N'Good', N'(STALLS Inside)', N'7f33y', 5, N'

(3yo+0-75)
', N'
                                    Bet At racingtv.com Handicap                                ', N'/racecards/16/musselburgh/2021-08-25/790006')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (109, 346, N'16:20', 790009, N'(Partly cloudy)', 8, N'Good', N'(STALLS Stands'' side)', N'5f1y', 6, N'

(3yo0-65)
', N'
                                    Watch Racing TV Now Handicap                                ', N'/racecards/16/musselburgh/2021-08-25/790009')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (110, 347, N'14:10', 790180, N'(Partly cloudy)', 11, N'Standard', N'(STALLS Outside)', N'1m1y', 6, N'

(3yo0-60)
', N'
                                    Cazoo Handicap                                ', N'/racecards/393/lingfield-aw/2021-08-25/790180')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (111, 347, N'14:40', 790175, N'(Partly cloudy)', 9, N'Standard', N'(STALLS Inside)', N'7f1y', 5, N'

(2yo)
', N'
                                    Sky Sports Racing Sky 415/EBF Restricted Maiden Fillies'' Stakes (Bands C and D) (GBB Race) (Div I)                                ', N'/racecards/393/lingfield-aw/2021-08-25/790175')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (112, 347, N'15:10', 792157, N'(Partly cloudy)', 9, N'Standard', N'(STALLS Inside)', N'7f1y', 5, N'

(2yo)
', N'
                                    Sky Sports Racing Sky 415/EBF Restricted Maiden Fillies'' Stakes (Bands C and D) (GBB Race) (Div II)                                ', N'/racecards/393/lingfield-aw/2021-08-25/792157')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (113, 347, N'15:40', 790177, N'(Partly cloudy)', 6, N'Standard', N'(STALLS Inside)', N'7f1y', 5, N'

(3yo+0-75)
', N'
                                    Pinpoint Resourcing Handicap                                ', N'/racecards/393/lingfield-aw/2021-08-25/790177')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (114, 347, N'16:15', 790178, N'(Partly cloudy)', 10, N'Standard', N'(STALLS Inside)', N'1m7f169y', 6, N'

(4yo+0-60)
', N'
                                    Follow @AtTheRaces On Twitter Handicap                                ', N'/racecards/393/lingfield-aw/2021-08-25/790178')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (115, 347, N'16:50', 790181, N'(Partly cloudy)', 11, N'Standard', N'(STALLS Inside)', N'6f1y', 6, N'

(3yo+0-52)
', N'
                                    Racing League On Sky Sports Racing Handicap                                ', N'/racecards/393/lingfield-aw/2021-08-25/790181')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (116, 347, N'17:25', 790176, N'(Partly cloudy)', 9, N'Standard', N'(STALLS Outside)', N'5f6y', 5, N'

(3yo+)
', N'
                                    Cazoo Maiden Stakes                                ', N'/racecards/393/lingfield-aw/2021-08-25/790176')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (117, 347, N'18:00', 790179, N'(Partly cloudy)', 12, N'Standard', N'(STALLS Inside)', N'1m4f', 6, N'

(3yo+0-52)
', N'
                                    Read Hollie Doyle''s Column On attheraces.com Handicap                                ', N'/racecards/393/lingfield-aw/2021-08-25/790179')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (118, 348, N'16:10', 792026, N'(Mostly sunny)', 15, N'Good', N'', N'2m4f57y', 0, N'

(4yo+)
', N'
                                    Whiteriver Wood Flooring Claiming Hurdle                                ', N'/racecards/176/bellewstown/2021-08-25/792026')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (119, 348, N'16:40', 792027, N'(Mostly sunny)', 10, N'Good', N'', N'2m4f57y', 0, N'

(4yo+)
', N'
                                    Connolly''s RED MILLS Irish EBF Auction Maiden Hurdle (IRE Incentive Race)                                ', N'/racecards/176/bellewstown/2021-08-25/792027')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (120, 348, N'17:15', 792028, N'(Mostly sunny)', 7, N'Good', N'', N'2m4f57y', 0, N'

(4yo+)
', N'
                                    John H Kierans Memorial Mares Hurdle                                ', N'/racecards/176/bellewstown/2021-08-25/792028')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (121, 348, N'17:45', 792029, N'(Mostly sunny)', 15, N'Good', N'', N'2m4f57y', 0, N'

(4yo+80-102)
', N'
                                    Donacarney Village Square Handicap Hurdle                                ', N'/racecards/176/bellewstown/2021-08-25/792029')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (122, 348, N'18:15', 792030, N'(Mostly sunny)', 15, N'Good', N'', N'2m4f57y', 0, N'

(4yo+)
', N'
                                    Bective Stud Mullacurry Cup Handicap Hurdle                                ', N'/racecards/176/bellewstown/2021-08-25/792030')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (123, 348, N'18:45', 792031, N'(Mostly sunny)', 13, N'Good', N'', N'2m212y', 0, N'

(4yo+80-95)
', N'
                                    John Purfield Memorial Handicap Hurdle (Div I)                                ', N'/racecards/176/bellewstown/2021-08-25/792031')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (124, 348, N'19:15', 792155, N'(Mostly sunny)', 15, N'Good', N'', N'2m212y', 0, N'

(4yo+80-95)
', N'
                                    John Purfield Memorial Handicap Hurdle (Div II)                                ', N'/racecards/176/bellewstown/2021-08-25/792155')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (125, 348, N'19:50', 792032, N'(Mostly sunny)', 13, N'Good', N'', N'2m212y', 0, N'

(4-7yo)
', N'
                                    Avison Young (Ladies Pro/Am) Flat Race                                ', N'/racecards/176/bellewstown/2021-08-25/792032')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (126, 349, N'16:30', 792103, N'(Partly cloudy)', 10, N'Standard', N'(STALLS Inside)', N'5f21y', 6, N'

(2yo0-60)
', N'
                                    Sky Sports Racing Sky 415 Nursery Handicap                                ', N'/racecards/513/wolverhampton-aw/2021-08-25/792103')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (127, 349, N'17:00', 792105, N'(Partly cloudy)', 7, N'Standard', N'(STALLS Inside)', N'6f20y', 4, N'

(2yo)
', N'
                                    St Leger On Sky Sports Racing Restricted Novice Stakes (Bands B, C and D) (GBB Race)                                ', N'/racecards/513/wolverhampton-aw/2021-08-25/792105')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (128, 349, N'17:35', 792104, N'(Partly cloudy)', 12, N'Standard', N'(STALLS Inside)', N'6f20y', 5, N'

(2yo0-70)
', N'
                                    Download The At The Races App Nursery Handicap                                ', N'/racecards/513/wolverhampton-aw/2021-08-25/792104')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (129, 349, N'18:05', 792107, N'(Partly cloudy)', 11, N'Standard', N'(STALLS Inside)', N'6f20y', 6, N'

(3yo+0-50)
', N'
                                    Read Hollie Doyle''s Column On attheraces.com Classified Stakes (Div I)                                ', N'/racecards/513/wolverhampton-aw/2021-08-25/792107')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (130, 349, N'18:35', 792159, N'(Partly cloudy)', 10, N'Standard', N'(STALLS Inside)', N'6f20y', 6, N'

(3yo+0-50)
', N'
                                    Read Hollie Doyle''s Column On attheraces.com Classified Stakes (Div II)                                ', N'/racecards/513/wolverhampton-aw/2021-08-25/792159')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (131, 349, N'19:05', 792106, N'(Partly cloudy)', 12, N'Standard', N'(STALLS Inside)', N'1m142y', 6, N'

(4yo+0-65)
', N'
                                    Follow @AtTheRaces On Twitter Handicap                                ', N'/racecards/513/wolverhampton-aw/2021-08-25/792106')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (132, 349, N'19:35', 792108, N'(Partly cloudy)', 8, N'Standard', N'(STALLS Inside)', N'1m142y', 6, N'

(3yo+0-50)
', N'
                                    Read Kevin Blake On attheraces.com Classified Stakes                                ', N'/racecards/513/wolverhampton-aw/2021-08-25/792108')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (133, 349, N'20:10', 792109, N'(Partly cloudy)', 8, N'Standard', N'(STALLS Inside)', N'1m1f104y', 5, N'

(3yo+0-70)
', N'
                                    Free Tips On attheraces.com Handicap                                ', N'/racecards/513/wolverhampton-aw/2021-08-25/792109')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (134, 350, N'17:55', 790170, N'(Partly cloudy)', 12, N'Standard To Slow', N'(STALLS Inside)', N'1m', 5, N'

(2yo)
', N'
                                    British Stallion Studs EBF Restricted Novice Stakes (For horses in Bands C and D) (GBB Race)                                ', N'/racecards/1079/kempton-aw/2021-08-25/790170')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (135, 350, N'18:25', 790173, N'(Partly cloudy)', 9, N'Standard To Slow', N'(STALLS Inside)', N'1m', 5, N'

(3yo0-70)
', N'
                                    Unibet Extra Place Offers Every Day Handicap (London Mile Series Qualifier) (Div I)                                ', N'/racecards/1079/kempton-aw/2021-08-25/790173')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (136, 350, N'18:55', 792158, N'(Partly cloudy)', 10, N'Standard To Slow', N'(STALLS Inside)', N'1m', 5, N'

(3yo0-70)
', N'
                                    Unibet Extra Place Offers Every Day Handicap (London Mile Series Qualifier) (Div II)                                ', N'/racecards/1079/kempton-aw/2021-08-25/792158')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (137, 350, N'19:25', 790171, N'(Partly cloudy)', 13, N'Standard To Slow', N'(STALLS Inside)', N'7f', 5, N'

(3yo+)
', N'
                                    Unibet Casino Deposit £10 Get £40 Bonus Novice Stakes                                ', N'/racecards/1079/kempton-aw/2021-08-25/790171')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (138, 350, N'20:00', 790172, N'(Partly cloudy)', 11, N'Standard To Slow', N'(STALLS Inside)', N'1m3f219y', 5, N'

(3yo+0-70)
', N'
                                    Unibet New Instant Roulette Fillies'' Handicap                                ', N'/racecards/1079/kempton-aw/2021-08-25/790172')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (139, 350, N'20:30', 790169, N'(Partly cloudy)', 7, N'Standard To Slow', N'(STALLS Inside)', N'1m2f219y', 3, N'

(3yo0-90)
', N'
                                    Unibet 3 Uniboosts A Day (London Middle Distance Series Qualifier) Handicap                                ', N'/racecards/1079/kempton-aw/2021-08-25/790169')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (140, 350, N'21:00', 790174, N'(Partly cloudy)', 12, N'Standard To Slow', N'(STALLS Inside)', N'6f', 6, N'

(3yo0-65)
', N'
                                    Try Our New Price Boosts At Unibet Handicap                                ', N'/racecards/1079/kempton-aw/2021-08-25/790174')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (141, 352, N'13:40', 792185, N'(Partly cloudy)', 8, N'Standard', N'', N'1m2f', 0, N'

(3yo+)
', N'
                                    Rossdales Veterinary Surgeons Maiden Stakes                                ', N'/racecards/1321/lingfield-aw-gb/2021-08-25/792185')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (142, 353, N'13:00', 790906, N'(Showers)', 10, N'Good', N'(STALLS Far side)', N'5f10y', 4, N'

(2yo)
', N'
                                    Paul Ferguson Memorial EBF Maiden Stakes (GBB Race)                                ', N'/racecards/54/sandown/2021-09-10/790906')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (143, 353, N'13:30', 790909, N'(Showers)', 6, N'Good', N'(STALLS Far side)', N'5f10y', 5, N'

(3yo+0-75)
', N'
                                    1Account ID Handicap                                ', N'/racecards/54/sandown/2021-09-10/790909')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (144, 353, N'14:00', 790905, N'(Showers)', 6, N'Good', N'(STALLS Inside)', N'1m', 4, N'

(2yo)
', N'
                                    IRE Incentive Scheme EBF Fillies'' Novice Stakes (GBB Race) (IRE Incentive Race)                                ', N'/racecards/54/sandown/2021-09-10/790905')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (145, 353, N'14:30', 790904, N'(Showers)', 9, N'Good', N'(STALLS Inside)', N'1m', 3, N'

(3yo+0-95)
', N'
                                    1Account KYC Handicap                                ', N'/racecards/54/sandown/2021-09-10/790904')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (146, 353, N'15:00', 790903, N'(Showers)', 9, N'Good', N'(STALLS Inside)', N'7f', 2, N'

(4yo+0-100)
', N'
                                    Follow #Raceday On Instagram Handicap                                ', N'/racecards/54/sandown/2021-09-10/790903')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (147, 353, N'15:35', 790908, N'(Showers)', 8, N'Good', N'(STALLS Inside)', N'7f', 4, N'

(3yo+0-80)
', N'
                                    Every Race Live On Racing TV Fillies'' Handicap                                ', N'/racecards/54/sandown/2021-09-10/790908')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (148, 353, N'16:05', 790907, N'(Showers)', 9, N'Good', N'(STALLS Inside)', N'1m1f209y', 4, N'

(3yo+0-85)
', N'
                                    1Account Handicap                                ', N'/racecards/54/sandown/2021-09-10/790907')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (149, 354, N'13:10', 790878, N'(Showers)', 16, N'Good To Firm', N'(STALLS Stands'' side)', N'7f6y', 3, N'

(2yo)
', N'
                                    Coopers Marquees Maiden Stakes (GBB Race)                                ', N'/racecards/15/doncaster/2021-09-10/790878')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (150, 354, N'13:40', 790876, N'(Showers)', 10, N'Good To Firm', N'(STALLS Stands'' side)', N'7f6y', 1, N'

(2yo)
', N'
                                    Cazoo Flying Scotsman Stakes (Listed Race)                                ', N'/racecards/15/doncaster/2021-09-10/790876')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (151, 354, N'14:10', 790872, N'(Showers)', 11, N'Good To Firm', N'(STALLS Stands'' side)', N'5f3y', 1, N'

(2yo)
', N'
                                    Wainwright Flying Childers Stakes (Group 2)                                ', N'/racecards/15/doncaster/2021-09-10/790872')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (152, 354, N'14:40', 790873, N'(Showers)', 7, N'Good To Firm', N'(STALLS Inside)', N'2m1f197y', 1, N'

(3yo+)
', N'
                                    Doncaster Cup Stakes (Group 2) (British Champions Series)                                ', N'/racecards/15/doncaster/2021-09-10/790873')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (153, 354, N'15:15', 790874, N'(Showers)', 12, N'Good To Firm', N'(STALLS Inside)', N'1m6f115y', 2, N'

(3yo+0-105)
', N'
                                    racehorselotto.com Mallard Handicap                                ', N'/racecards/15/doncaster/2021-09-10/790874')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (154, 354, N'15:45', 790875, N'(Showers)', 13, N'Good To Firm', N'(STALLS Stands'' side)', N'6f111y', 2, N'

(3yo+0-105)
', N'
                                    Cazoo Handicap                                ', N'/racecards/15/doncaster/2021-09-10/790875')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (155, 354, N'16:20', 790877, N'(Showers)', 7, N'Good To Firm', N'(STALLS Inside)', N'1m3f197y', 2, N'

(3yo+0-100)
', N'
                                    British EBF Premier Fillies'' Handicap                                ', N'/racecards/15/doncaster/2021-09-10/790877')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (156, 355, N'13:50', 790913, N'(Showers)', 5, N'Good', N'(STALLS Inside)', N'7f127y', 4, N'

(2yo)
', N'
                                    Envirosips EBF Novice Stakes (Colts & Geldings) (IRE Incentive Race)                                ', N'/racecards/13/chester/2021-09-10/790913')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (157, 355, N'14:20', 790916, N'(Showers)', 7, N'Good', N'(STALLS Inside)', N'7f1y', 4, N'

(2yo0-85)
', N'
                                    Deepbridge Estate Planning Service Nursery Handicap (IRE Incentive Race)                                ', N'/racecards/13/chester/2021-09-10/790916')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (158, 355, N'14:50', 790914, N'(Showers)', 8, N'Good', N'(STALLS Outside)', N'1m2f70y', 4, N'

(3yo0-85)
', N'
                                    Homeserve Handicap                                ', N'/racecards/13/chester/2021-09-10/790914')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (159, 355, N'15:25', 790911, N'(Showers)', 9, N'Good', N'(STALLS Inside)', N'7f1y', 3, N'

(3yo+0-95)
', N'
                                    TMT Group Handicap                                ', N'/racecards/13/chester/2021-09-10/790911')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (160, 355, N'16:00', 790912, N'(Showers)', 7, N'Good', N'(STALLS Outside)', N'1m2f70y', 4, N'

(2yo)
', N'
                                    Motorola Solutions Maiden Stakes (GBB Race) (IRE Incentive Race)                                ', N'/racecards/13/chester/2021-09-10/790912')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (161, 355, N'16:30', 790910, N'(Showers)', 6, N'Good', N'(STALLS Inside)', N'1m4f63y', 3, N'

(3yo+0-95)
', N'
                                    White Oak Handicap                                ', N'/racecards/13/chester/2021-09-10/790910')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (162, 355, N'17:05', 790915, N'(Showers)', 12, N'Good', N'(STALLS Inside)', N'7f127y', 4, N'

(3yo+0-80)
', N'
                                    Mental Health UK Handicap (For Gentleman Amateur Jockeys)                                ', N'/racecards/13/chester/2021-09-10/790915')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (163, 356, N'15:55', 793134, N'(Cloudy)', 12, N'Soft', N'', N'2m2f', 0, N'

(3yo)
', N'
                                    Ballinrobe ''A History In The Making'' 3-Y-O Maiden Hurdle                                ', N'/racecards/175/ballinrobe/2021-09-10/793134')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (164, 356, N'16:25', 793135, N'(Cloudy)', 15, N'Soft', N'', N'2m2f', 0, N'

(5yo+)
', N'
                                    Irish Stallion Farms EBF Mares Maiden Hurdle                                ', N'/racecards/175/ballinrobe/2021-09-10/793135')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (165, 356, N'17:00', 793136, N'(Cloudy)', 16, N'Soft', N'', N'2m5f181y', 0, N'

(4yo+80-116)
', N'
                                    Burkes Clonbur Handicap Hurdle                                ', N'/racecards/175/ballinrobe/2021-09-10/793136')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (166, 356, N'17:30', 793137, N'(Cloudy)', 12, N'Soft', N'', N'2m5f181y', 0, N'

(4yo+)
', N'
                                    Irish Stallion Farms EBF Mares Handicap Hurdle                                ', N'/racecards/175/ballinrobe/2021-09-10/793137')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (167, 356, N'18:00', 793138, N'(Cloudy)', 9, N'Soft', N'', N'2m7f', 0, N'

(4yo+)
', N'
                                    P&D Lydon Beginners Chase                                ', N'/racecards/175/ballinrobe/2021-09-10/793138')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (168, 356, N'18:30', 793164, N'(Cloudy)', 16, N'Soft', N'', N'2m7f', 0, N'

(4yo+0-95)
', N'
                                    Good Luck To Mayo Handicap Chase                                ', N'/racecards/175/ballinrobe/2021-09-10/793164')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (169, 356, N'19:00', 793139, N'(Cloudy)', 11, N'Soft', N'', N'2m1f', 0, N'

(5yo+0-109)
', N'
                                    Adare Manor Opportunity Handicap Chase                                ', N'/racecards/175/ballinrobe/2021-09-10/793139')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (170, 356, N'19:30', 793140, N'(Cloudy)', 10, N'Soft', N'', N'2m', 0, N'

(4yo)
', N'
                                    Eamon Sheridan Groundworks INH Flat Race                                ', N'/racecards/175/ballinrobe/2021-09-10/793140')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (171, 357, N'16:13', 790900, N'(Showers)', 9, N'Good To Firm', N'(STALLS Far side)', N'1m', 5, N'

(2yo)
', N'
                                    Byerley Stud Restricted Novice Stakes (For horses In Bands B, C And D) (GBB Race)                                ', N'/racecards/52/salisbury/2021-09-10/790900')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (172, 357, N'16:45', 790901, N'(Showers)', 10, N'Good To Firm', N'(STALLS Far side)', N'6f213y', 5, N'

(2yo0-75)
', N'
                                    Andy Hardwick Memorial Nursery Handicap                                ', N'/racecards/52/salisbury/2021-09-10/790901')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (173, 357, N'17:15', 790898, N'(Showers)', 5, N'Good To Firm', N'(STALLS Far side)', N'6f', 3, N'

(2yo0-90)
', N'
                                    Irish Stallion Farms EBF Fillies'' Nursery Handicap                                ', N'/racecards/52/salisbury/2021-09-10/790898')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (174, 357, N'17:45', 790902, N'(Showers)', 13, N'Good To Firm', N'(STALLS Far side)', N'6f', 6, N'

(3yo0-65)
', N'
                                    Cognatum Howarth Park Handicap                                ', N'/racecards/52/salisbury/2021-09-10/790902')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (175, 357, N'18:15', 790897, N'(Showers)', 5, N'Good To Firm', N'(STALLS Flag start)', N'1m6f44y', 2, N'

(3yo+)
', N'
                                    Weatherbys Bloodstock Pro Persian Punch Conditions Stakes                                ', N'/racecards/52/salisbury/2021-09-10/790897')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (176, 357, N'18:45', 790899, N'(Showers)', 7, N'Good To Firm', N'(STALLS Inside)', N'1m1f201y', 4, N'

(3yo+)
', N'
                                    D & N Construction British EBF Novice Stakes                                ', N'/racecards/52/salisbury/2021-09-10/790899')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (177, 358, N'15:40', 793206, N'', 6, N'Good To Soft', N'', N'1m4f', 0, N'

(3yo)
', N'
                                    Prix Turenne (Listed Race) (3yo) (Turf)                                ', N'/racecards/219/saint-cloud/2021-09-10/793206')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (179, 365, N'13:00', 790906, N'(Showers)', 10, N'Good', N'(STALLS Far side)', N'5f10y', 4, N'

(2yo)
', N'
                                    Paul Ferguson Memorial EBF Maiden Stakes (GBB Race)                                ', N'/racecards/54/sandown/2021-09-10/790906')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (180, 371, N'13:00', 790906, N'(Showers)', 10, N'Good', N'(STALLS Far side)', N'5f10y', 4, N'

(2yo)
', N'
                                    Paul Ferguson Memorial EBF Maiden Stakes (GBB Race)                                ', N'/racecards/54/sandown/2021-09-10/790906')
GO
SET IDENTITY_INSERT [dbo].[tb_race] OFF
GO
ALTER TABLE [dbo].[tb_event]  WITH CHECK ADD  CONSTRAINT [FK_tb_event_tb_course] FOREIGN KEY([course_id])
REFERENCES [dbo].[tb_course] ([course_id])
GO
ALTER TABLE [dbo].[tb_event] CHECK CONSTRAINT [FK_tb_event_tb_course]
GO
ALTER TABLE [dbo].[tb_race]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_tb_event] FOREIGN KEY([event_id])
REFERENCES [dbo].[tb_event] ([event_id])
GO
ALTER TABLE [dbo].[tb_race] CHECK CONSTRAINT [FK_tb_race_tb_event]
GO
ALTER TABLE [dbo].[tb_race_horse]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_horse_tb_horse] FOREIGN KEY([horse_id])
REFERENCES [dbo].[tb_horse] ([horse_id])
GO
ALTER TABLE [dbo].[tb_race_horse] CHECK CONSTRAINT [FK_tb_race_horse_tb_horse]
GO
ALTER TABLE [dbo].[tb_race_horse]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_horse_tb_jockey] FOREIGN KEY([jockey_id])
REFERENCES [dbo].[tb_jockey] ([jockey_id])
GO
ALTER TABLE [dbo].[tb_race_horse] CHECK CONSTRAINT [FK_tb_race_horse_tb_jockey]
GO
ALTER TABLE [dbo].[tb_race_horse]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_horse_tb_race] FOREIGN KEY([race_id])
REFERENCES [dbo].[tb_race] ([race_id])
GO
ALTER TABLE [dbo].[tb_race_horse] CHECK CONSTRAINT [FK_tb_race_horse_tb_race]
GO
ALTER TABLE [dbo].[tb_race_horse]  WITH CHECK ADD  CONSTRAINT [FK_tb_race_horse_tb_trainer] FOREIGN KEY([trainer_id])
REFERENCES [dbo].[tb_trainer] ([trainer_id])
GO
ALTER TABLE [dbo].[tb_race_horse] CHECK CONSTRAINT [FK_tb_race_horse_tb_trainer]
GO
USE [master]
GO
ALTER DATABASE [RHDC] SET  READ_WRITE 
GO
