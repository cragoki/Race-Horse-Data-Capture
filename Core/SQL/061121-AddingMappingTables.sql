USE [RHDC]
GO
/****** Object:  Table [dbo].[tb_age_type]    Script Date: 06/11/2021 19:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_age_type](
	[age_type_id] [int] IDENTITY(1,1) NOT NULL,
	[age_type] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tb_age_type] PRIMARY KEY CLUSTERED 
(
	[age_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_distance_type]    Script Date: 06/11/2021 19:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_distance_type](
	[distance_type_id] [int] IDENTITY(1,1) NOT NULL,
	[distance_type] [varchar](150) NOT NULL,
 CONSTRAINT [PK_tb_distance_type] PRIMARY KEY CLUSTERED 
(
	[distance_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_going_type]    Script Date: 06/11/2021 19:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_going_type](
	[going_type_id] [int] IDENTITY(1,1) NOT NULL,
	[going_type] [varchar](150) NOT NULL,
 CONSTRAINT [PK_tb_going_type] PRIMARY KEY CLUSTERED 
(
	[going_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_meeting_type]    Script Date: 06/11/2021 19:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_meeting_type](
	[meeting_type_id] [int] IDENTITY(1,1) NOT NULL,
	[meeting_type] [varchar](150) NOT NULL,
 CONSTRAINT [PK_tb_meeting_type] PRIMARY KEY CLUSTERED 
(
	[meeting_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_stalls_type]    Script Date: 06/11/2021 19:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_stalls_type](
	[stalls_type_id] [int] IDENTITY(1,1) NOT NULL,
	[stalls_type] [varchar](150) NOT NULL,
 CONSTRAINT [PK_tb_stalls_type] PRIMARY KEY CLUSTERED 
(
	[stalls_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_surface_type]    Script Date: 06/11/2021 19:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_surface_type](
	[surface_type_id] [int] IDENTITY(1,1) NOT NULL,
	[surface_type] [varchar](150) NULL,
 CONSTRAINT [PK_tb_surface_type] PRIMARY KEY CLUSTERED 
(
	[surface_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_weather_type]    Script Date: 06/11/2021 19:42:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_weather_type](
	[weather_type_id] [int] IDENTITY(1,1) NOT NULL,
	[weather_type] [nchar](10) NULL,
 CONSTRAINT [PK_tb_weather_type] PRIMARY KEY CLUSTERED 
(
	[weather_type_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
