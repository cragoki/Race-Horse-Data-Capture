USE [RHDC]
GO
/****** Object:  Table [dbo].[tb_job]    Script Date: 9/20/2021 3:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_job](
	[job_id] [int] IDENTITY(1,1) NOT NULL,
	[job_name] [varchar](50) NOT NULL,
	[last_execution] [datetime] NULL,
	[next_execution] [datetime] NOT NULL,
	[interval_check_minutes] [int] NULL,
 CONSTRAINT [PK_tb_job \] PRIMARY KEY CLUSTERED 
(
	[job_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tb_job] ON 
GO
INSERT [dbo].[tb_job] ([job_id], [job_name], [last_execution], [next_execution], [interval_check_minutes]) VALUES (2, N'rhdc-automation', CAST(N'2021-09-20T15:01:52.737' AS DateTime), CAST(N'2021-09-21T15:01:52.737' AS DateTime), 30)
GO
INSERT [dbo].[tb_job] ([job_id], [job_name], [last_execution], [next_execution], [interval_check_minutes]) VALUES (3, N'rhdc-backlog', NULL, CAST(N'2021-09-20T12:00:00.000' AS DateTime), 30)
GO
INSERT [dbo].[tb_job] ([job_id], [job_name], [last_execution], [next_execution], [interval_check_minutes]) VALUES (4, N'rhdc-result-retriever', NULL, CAST(N'2021-09-20T23:00:00.000' AS DateTime), 30)
GO
SET IDENTITY_INSERT [dbo].[tb_job] OFF
GO
