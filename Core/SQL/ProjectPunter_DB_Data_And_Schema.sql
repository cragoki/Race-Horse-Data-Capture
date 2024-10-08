USE [RHDC]
GO
/****** Object:  Table [dbo].[tb_archive_horse]    Script Date: 9/13/2021 2:56:37 PM ******/
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
/****** Object:  Table [dbo].[tb_batch]    Script Date: 9/13/2021 2:56:37 PM ******/
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
/****** Object:  Table [dbo].[tb_course]    Script Date: 9/13/2021 2:56:37 PM ******/
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
/****** Object:  Table [dbo].[tb_event]    Script Date: 9/13/2021 2:56:37 PM ******/
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
/****** Object:  Table [dbo].[tb_horse]    Script Date: 9/13/2021 2:56:37 PM ******/
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
	[top_speed] [varchar](5) NULL,
	[rpr] [varchar](5) NULL,
 CONSTRAINT [PK_tb_horse] PRIMARY KEY CLUSTERED 
(
	[horse_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_jockey]    Script Date: 9/13/2021 2:56:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_jockey](
	[jockey_id] [int] IDENTITY(1,1) NOT NULL,
	[jockey_name] [varchar](150) NOT NULL,
	[jockey_url] [varchar](150) NULL,
 CONSTRAINT [PK_tb_jockey] PRIMARY KEY CLUSTERED 
(
	[jockey_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_race]    Script Date: 9/13/2021 2:56:37 PM ******/
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
	[completed] bit NULL,
 CONSTRAINT [PK_tb_race] PRIMARY KEY CLUSTERED 
(
	[race_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_race_horse]    Script Date: 9/13/2021 2:56:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_race_horse](
	[race_horse_id] [int] IDENTITY(1,1) NOT NULL,
	[race_id] [int] NOT NULL,
	[horse_id] [int] NOT NULL,
	[weight] [varchar](15) NULL,
	[age] [int] NULL,
	[trainer_id] [int] NULL,
	[jockey_id] [int] NULL,
	[jockey_weight] [varchar](15) NULL,
	[finished] [bit] NULL,
	[position] [int] NULL,
	[description] [varchar](250) NULL,
	[rp_notes] [varchar](500) NULL,
 CONSTRAINT [PK_tb_race_horse_1] PRIMARY KEY CLUSTERED 
(
	[race_horse_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tb_trainer]    Script Date: 9/13/2021 2:56:37 PM ******/
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
INSERT [dbo].[tb_batch] ([batch_id], [diagnostics], [date]) VALUES (N'16d1e29d-36e9-4ed9-a1fa-278677022c54', N'{"Automator":0,"TimeInitialized":"2021-09-13T14:49:10.8451797+02:00","TimeCompleted":"2021-09-13T14:50:21.9034439+02:00","EventsFiltered":4,"ErrorsEncountered":0,"EllapsedTime":71.0582642}', CAST(N'2021-09-13T14:50:21.947' AS DateTime))
GO
INSERT [dbo].[tb_batch] ([batch_id], [diagnostics], [date]) VALUES (N'7269d755-b82f-45a9-9d3f-54ef66ed2915', N'{"Automator":0,"TimeInitialized":"2021-08-25T14:40:10.9330285+02:00","TimeCompleted":"2021-08-25T14:41:11.7325394+02:00","EventsFiltered":8,"ErrorsEncountered":0,"EllapsedTime":60.7995109}', CAST(N'2021-08-25T14:41:11.773' AS DateTime))
GO
INSERT [dbo].[tb_batch] ([batch_id], [diagnostics], [date]) VALUES (N'7dc2fbee-6ccf-4bec-b6c1-557010007d40', N'{"Automator":0,"TimeInitialized":"2021-09-13T14:23:53.7968654+02:00","TimeCompleted":"2021-09-13T14:24:23.3209041+02:00","EventsFiltered":4,"ErrorsEncountered":0,"EllapsedTime":29.5240387}', CAST(N'2021-09-13T14:24:23.360' AS DateTime))
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
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (7, N'Brighton', N'GB', 0, N'/profile/course/7/brighton')
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
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (80, N'Thirsk', N'GB', 0, N'/profile/course/80/thirsk')
GO
INSERT [dbo].[tb_course] ([course_id], [name], [country_code], [all_weather], [course_url]) VALUES (101, N'Worcester', N'GB', 0, N'/profile/course/101/worcester')
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
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (461, 80, 0, N'', N'Thirsk_9/13/2021', N'/racecards/80/thirsk/2021-09-13', N'thirsk', N'Flat', 8, N'16d1e29d-36e9-4ed9-a1fa-278677022c54', CAST(N'2021-09-13T14:49:12.600' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (462, 101, 0, N'', N'Worcester_9/13/2021', N'/racecards/101/worcester/2021-09-13', N'worcester', N'Jumps', 7, N'16d1e29d-36e9-4ed9-a1fa-278677022c54', CAST(N'2021-09-13T14:49:12.853' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (463, 7, 0, N'', N'Brighton_9/13/2021', N'/racecards/7/brighton/2021-09-13', N'brighton', N'Flat', 7, N'16d1e29d-36e9-4ed9-a1fa-278677022c54', CAST(N'2021-09-13T14:49:12.873' AS DateTime))
GO
INSERT [dbo].[tb_event] ([event_id], [course_id], [abandoned], [surface_type], [name], [meeting_url], [hash_name], [meeting_type], [races], [batch_id], [created]) VALUES (464, 1079, 0, N'Polytrack', N'Kempton_9/13/2021', N'/racecards/1079/kempton-aw/2021-09-13', N'kempton-aw', N'Flat', 8, N'16d1e29d-36e9-4ed9-a1fa-278677022c54', CAST(N'2021-09-13T14:49:12.903' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tb_event] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_horse] ON 
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (1, 3807120, N'
ClochNua


', NULL, N'/profile/horse/3807120/cloch-nua', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (2, 3612354, N'
GoldenProsperity


', NULL, N'/profile/horse/3612354/golden-prosperity', N'
42', N'
69')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (3, 3583175, N'
SnakePass


', NULL, N'/profile/horse/3583175/snake-pass', N'
-', N'
29')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (4, 3770754, N'
WakeyWakey


', NULL, N'/profile/horse/3770754/wakey-wakey', N'
57', N'
76')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (5, 3689304, N'
YellowBear


', NULL, N'/profile/horse/3689304/yellow-bear', N'
52', N'
85')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (6, 3772677, N'
Alethiometer


', NULL, N'/profile/horse/3772677/alethiometer', N'
53', N'
73')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (7, 3807123, N'
EmilyPost


', NULL, N'/profile/horse/3807123/emily-post', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (8, 3603991, N'
Qweldryk


', NULL, N'/profile/horse/3603991/qweldryk', N'
44', N'
76')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (9, 3749593, N'
WeePoppy


', NULL, N'/profile/horse/3749593/wee-poppy', N'
72', N'
78')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (10, 3366803, N'
WhealKitty


', NULL, N'/profile/horse/3366803/wheal-kitty', N'
56', N'
76')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (11, 3179933, N'
TerrafirmaLady


', NULL, N'/profile/horse/3179933/terrafirma-lady', N'
45', N'
110')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (12, 3479740, N'
GroveAsh


', NULL, N'/profile/horse/3479740/grove-ash', N'
53', N'
99')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (13, 2549647, N'
CloudFormation


', NULL, N'/profile/horse/2549647/cloud-formation', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (14, 2926744, N'
DubaiGuest


', NULL, N'/profile/horse/2926744/dubai-guest', N'
40', N'
98')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (15, 3335260, N'
HeadOn


', NULL, N'/profile/horse/3335260/head-on', N'
44', N'
112')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (16, 2523485, N'
BigTree


', NULL, N'/profile/horse/2523485/big-tree', N'
42', N'
106')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (17, 1857695, N'
MarilynMonroe


', NULL, N'/profile/horse/1857695/marilyn-monroe', N'
65', N'
105')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (18, 861664, N'
MrJim


', NULL, N'/profile/horse/861664/mr-jim', N'
79', N'
111')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (19, 1253845, N'
TheDawnMan


', NULL, N'/profile/horse/1253845/the-dawn-man', N'
100', N'
109')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (20, 2426463, N'
FrillyFrock


', NULL, N'/profile/horse/2426463/frilly-frock', N'
35', N'
101')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (21, 3502544, N'
GreatNews


', NULL, N'/profile/horse/3502544/great-news', N'
79', N'
94')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (22, 895400, N'
Mamillius


', NULL, N'/profile/horse/895400/mamillius', N'
74', N'
90')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (23, 2643521, N'
RecallTheShow


', NULL, N'/profile/horse/2643521/recall-the-show', N'
75', N'
91')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (24, 2714417, N'
BatchelorBoy


', NULL, N'/profile/horse/2714417/batchelor-boy', N'
65', N'
92')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (25, 838388, N'
BoomTheGroom


', NULL, N'/profile/horse/838388/boom-the-groom', N'
68', N'
92')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (26, 2142258, N'
GiltEdge


', NULL, N'/profile/horse/2142258/gilt-edge', N'
81', N'
92')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (27, 3552563, N'
Gangway


', NULL, N'/profile/horse/3552563/gangway', N'
-', N'
73')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (28, 3658030, N'
BuxtedReel


', NULL, N'/profile/horse/3658030/buxted-reel', N'
-', N'
69')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (29, 3685059, N'
GreatnessAwaits


', NULL, N'/profile/horse/3685059/greatness-awaits', N'
-', N'
78')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (30, 3437720, N'
BeLuckyMySon


', NULL, N'/profile/horse/3437720/be-lucky-my-son', N'
54', N'
71')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (31, 3668367, N'
ComeToPass


', NULL, N'/profile/horse/3668367/come-to-pass', N'
-', N'
67')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (32, 3662558, N'
Cephalus


', NULL, N'/profile/horse/3662558/cephalus', N'
55', N'
71')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (33, 3366620, N'
Adaayinourlife


', NULL, N'/profile/horse/3366620/adaayinourlife', N'
42', N'
76')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (34, 3586885, N'
CheeseTheOne


', NULL, N'/profile/horse/3586885/cheese-the-one', N'
24', N'
71')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (35, 3654392, N'
EgypsyanCrackelli


', NULL, N'/profile/horse/3654392/egypsyan-crackelli', N'
-', N'
69')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (36, 3607418, N'
FourNotes


', NULL, N'/profile/horse/3607418/four-notes', N'
41', N'
71')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (37, 3564060, N'
Divining


', NULL, N'/profile/horse/3564060/divining', N'
-', N'
67')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (38, 3668353, N'
GowerGlen


', NULL, N'/profile/horse/3668353/gower-glen', N'
-', N'
67')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (39, 3588895, N'
Pneumatic


', NULL, N'/profile/horse/3588895/pneumatic', N'
43', N'
72')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (40, 3533563, N'
Benzema


', NULL, N'/profile/horse/3533563/benzema', N'
71', N'
76')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (41, 3481869, N'
NobleAlbert


', NULL, N'/profile/horse/3481869/noble-albert', N'
56', N'
74')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (42, 3535319, N'
MakeAProphet


', NULL, N'/profile/horse/3535319/make-a-prophet', N'
51', N'
72')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (43, 3549483, N'
WoodlandsCharm


', NULL, N'/profile/horse/3549483/woodlands-charm', N'
50', N'
71')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (44, 3646551, N'
Contactless


', NULL, N'/profile/horse/3646551/contactless', N'
58', N'
75')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (45, 3597229, N'
Nemorum


', NULL, N'/profile/horse/3597229/nemorum', N'
48', N'
72')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (46, 3701276, N'
Kissing


', NULL, N'/profile/horse/3701276/kissing', N'
55', N'
69')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (47, 3693790, N'
AngelsTale


', NULL, N'/profile/horse/3693790/angels-tale', N'
32', N'
72')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (48, 3607408, N'
CotaiGrey


', NULL, N'/profile/horse/3607408/cotai-grey', N'
62', N'
75')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (49, 3516764, N'
NaughtyNadine


', NULL, N'/profile/horse/3516764/naughty-nadine', N'
52', N'
74')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (50, 3693789, N'
BobbyDalton


', NULL, N'/profile/horse/3693789/bobby-dalton', N'
39', N'
69')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (51, 3366977, N'
Enraged


', NULL, N'/profile/horse/3366977/enraged', N'
62', N'
75')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (52, 3550668, N'
Mymateshez


', NULL, N'/profile/horse/3550668/mymateshez', N'
54', N'
72')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (53, 3612356, N'
TheGreyLass


', NULL, N'/profile/horse/3612356/the-grey-lass', N'
55', N'
74')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (54, 3689354, N'
Talladora


', NULL, N'/profile/horse/3689354/talladora', N'
58', N'
70')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (55, 3577229, N'
MissWombleton


', NULL, N'/profile/horse/3577229/miss-wombleton', N'
33', N'
68')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (56, 3438011, N'
Sonella


', NULL, N'/profile/horse/3438011/sonella', N'
67', N'
75')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (57, 3509850, N'
Hilary''sBoy


', NULL, N'/profile/horse/3509850/hilarys-boy', N'
41', N'
65')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (58, 3600657, N'
GoldenGal


', NULL, N'/profile/horse/3600657/golden-gal', N'
43', N'
72')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (59, 2537451, N'
Oakenshield


', NULL, N'/profile/horse/2537451/oakenshield', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (60, 1860070, N'
RedBravo


', NULL, N'/profile/horse/1860070/red-bravo', N'
59', N'
71')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (61, 1894513, N'
JungleInthebungle


', NULL, N'/profile/horse/1894513/jungle-inthebungle', N'
59', N'
60')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (62, 3095503, N'
Manumission


', NULL, N'/profile/horse/3095503/manumission', N'
62', N'
73')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (63, 3125449, N'
NaughtyAna


', NULL, N'/profile/horse/3125449/naughty-ana', N'
55', N'
66')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (64, 1505802, N'
Bibbidibobbidiboo


', NULL, N'/profile/horse/1505802/bibbidibobbidiboo', N'
64', N'
73')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (65, 2847961, N'
AlexGracie


', NULL, N'/profile/horse/2847961/alex-gracie', N'
59', N'
70')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (66, 1860064, N'
Mutawaffer


', NULL, N'/profile/horse/1860064/mutawaffer', N'
52', N'
66')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (67, 3096277, N'
Twentysharesofgrey


', NULL, N'/profile/horse/3096277/twentysharesofgrey', N'
30', N'
68')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (68, 3679950, N'
NoseyRosiePosey


', NULL, N'/profile/horse/3679950/nosey-rosie-posey', N'
33', N'
64')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (69, 2645434, N'
CobraEye


', NULL, N'/profile/horse/2645434/cobra-eye', N'
64', N'
69')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (70, 3531568, N'
Sleight


', NULL, N'/profile/horse/3531568/sleight', N'
47', N'
67')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (71, 2819110, N'
TwelveDiamonds


', NULL, N'/profile/horse/2819110/twelve-diamonds', N'
63', N'
72')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (72, 3152147, N'
RacyStacey


', NULL, N'/profile/horse/3152147/racy-stacey', N'
48', N'
69')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (73, 2654598, N'
SparklingDiamond


', NULL, N'/profile/horse/2654598/sparkling-diamond', N'
43', N'
65')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (74, 3115047, N'
Shesaheart


', NULL, N'/profile/horse/3115047/shesaheart', N'
64', N'
71')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (75, 3092729, N'
FrenchRed


', NULL, N'/profile/horse/3092729/french-red', N'
58', N'
70')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (76, 3097267, N'
LittleGem


', NULL, N'/profile/horse/3097267/little-gem', N'
39', N'
66')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (77, 3048899, N'
PorthDiana


', NULL, N'/profile/horse/3048899/porth-diana', N'
28', N'
66')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (78, 3149641, N'
Topflightsuperfly


', NULL, N'/profile/horse/3149641/topflightsuperfly', N'
-', N'
43')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (79, 3284492, N'
Bartzella


', NULL, N'/profile/horse/3284492/bartzella', N'
65', N'
89')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (80, 3687262, N'
AliceKazeem


', NULL, N'/profile/horse/3687262/alice-kazeem', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (81, 3779993, N'
Cilka


', NULL, N'/profile/horse/3779993/cilka', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (82, 3229635, N'
Quilted


', NULL, N'/profile/horse/3229635/quilted', N'
58', N'
83')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (83, 3713244, N'
TitaniumMoon


', NULL, N'/profile/horse/3713244/titanium-moon', N'
-', N'
79')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (84, 906444, N'
Daawy


', NULL, N'/profile/horse/906444/daawy', N'
66', N'
76')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (85, 2876581, N'
GoldenHind


', NULL, N'/profile/horse/2876581/golden-hind', N'
50', N'
75')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (86, 3226219, N'
GoldenDove


', NULL, N'/profile/horse/3226219/golden-dove', N'
61', N'
74')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (87, 3336308, N'
Hesperis


', NULL, N'/profile/horse/3336308/hesperis', N'
54', N'
77')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (88, 1796196, N'
Hooflepuff


', NULL, N'/profile/horse/1796196/hooflepuff', N'
9', N'
73')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (89, 2681610, N'
InternationalLion


', NULL, N'/profile/horse/2681610/international-lion', N'
47', N'
67')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (90, 3244636, N'
LadyRockstar


', NULL, N'/profile/horse/3244636/lady-rockstar', N'
79', N'
86')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (91, 2550991, N'
ElegantErin


', NULL, N'/profile/horse/2550991/elegant-erin', N'
74', N'
80')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (92, 1476932, N'
HarvestDay


', NULL, N'/profile/horse/1476932/harvest-day', N'
74', N'
85')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (93, 3370958, N'
Lilikoi


', NULL, N'/profile/horse/3370958/lilikoi', N'
72', N'
79')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (94, 2739028, N'
RhymeScheme


', NULL, N'/profile/horse/2739028/rhyme-scheme', N'
67', N'
82')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (95, 3269862, N'
Lochnaver


', NULL, N'/profile/horse/3269862/lochnaver', N'
27', N'
79')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (96, 1491794, N'
KylieRules


', NULL, N'/profile/horse/1491794/kylie-rules', N'
83', N'
89')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (97, 891151, N'
Florenza


', NULL, N'/profile/horse/891151/florenza', N'
74', N'
83')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (98, 3010331, N'
KeyLook


', NULL, N'/profile/horse/3010331/key-look', N'
70', N'
76')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (99, 1452920, N'
Zumurud


', NULL, N'/profile/horse/1452920/zumurud', N'
74', N'
86')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (100, 2707522, N'
GoldZabeel


', NULL, N'/profile/horse/2707522/gold-zabeel', N'
74', N'
87')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (101, 2201781, N'
Irreverent


', NULL, N'/profile/horse/2201781/irreverent', N'
64', N'
91')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (102, 1547597, N'
SaisonsD''Or


', NULL, N'/profile/horse/1547597/saisons-dor', N'
72', N'
89')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (103, 866324, N'
GetKnotted


', NULL, N'/profile/horse/866324/get-knotted', N'
74', N'
88')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (104, 1467593, N'
DeltaRiver


', NULL, N'/profile/horse/1467593/delta-river', N'
79', N'
84')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (105, 1371643, N'
FlaviusTitus


', NULL, N'/profile/horse/1371643/flavius-titus', N'
73', N'
86')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (106, 2741064, N'
DickDatchery


', NULL, N'/profile/horse/2741064/dick-datchery', N'
67', N'
83')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (107, 3405222, N'
Vellco''sBoy


', NULL, N'/profile/horse/3405222/vellcos-boy', N'
63', N'
81')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (108, 1536530, N'
RumRunner


', NULL, N'/profile/horse/1536530/rum-runner', N'
72', N'
86')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (109, 833567, N'
ClubWexford


', NULL, N'/profile/horse/833567/club-wexford', N'
74', N'
87')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (110, 853110, N'
MagicalEffect


', NULL, N'/profile/horse/853110/magical-effect', N'
78', N'
93')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (111, 1655035, N'
WillieJohn


', NULL, N'/profile/horse/1655035/willie-john', N'
69', N'
81')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (112, 2641748, N'
TrinityLake


', NULL, N'/profile/horse/2641748/trinity-lake', N'
83', N'
92')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (113, 1448205, N'
OurLittlePony


', NULL, N'/profile/horse/1448205/our-little-pony', N'
87', N'
89')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (114, 2691574, N'
StoneCircle


', NULL, N'/profile/horse/2691574/stone-circle', N'
74', N'
91')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (115, 2645480, N'
HotSummer


', NULL, N'/profile/horse/2645480/hot-summer', N'
80', N'
88')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (116, 2869407, N'
TheWeedMachine


', NULL, N'/profile/horse/2869407/the-weed-machine', N'
51', N'
89')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (117, 1035974, N'
ProudArchi


', NULL, N'/profile/horse/1035974/proud-archi', N'
80', N'
87')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (118, 1726708, N'
SwissKnight


', NULL, N'/profile/horse/1726708/swiss-knight', N'
74', N'
89')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (119, 2917903, N'
BrazenBolt


', NULL, N'/profile/horse/2917903/brazen-bolt', N'
67', N'
87')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (120, 2527746, N'
Marvel


', NULL, N'/profile/horse/2527746/marvel', N'
81', N'
87')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (121, 2708937, N'
SingeAnglais


', NULL, N'/profile/horse/2708937/singe-anglais', N'
76', N'
88')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (122, 1624720, N'
ForeseeableFuture


', NULL, N'/profile/horse/1624720/foreseeable-future', N'
73', N'
91')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (123, 2902406, N'
BrotherPat


', NULL, N'/profile/horse/2902406/brother-pat', N'
60', N'
107')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (124, 2296155, N'
BleueAway


', NULL, N'/profile/horse/2296155/bleue-away', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (125, 885109, N'
DoubleCourt


', NULL, N'/profile/horse/885109/double-court', N'
69', N'
105')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (126, 1154891, N'
Orchestrated


', NULL, N'/profile/horse/1154891/orchestrated', N'
83', N'
102')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (127, 2510117, N'
GoingMobile


', NULL, N'/profile/horse/2510117/going-mobile', N'
89', N'
111')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (128, 2949603, N'
Regaby


', NULL, N'/profile/horse/2949603/regaby', N'
80', N'
103')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (129, 2378890, N'
WriteItDown


', NULL, N'/profile/horse/2378890/write-it-down', N'
93', N'
115')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (130, 1452964, N'
LapfordLad


', NULL, N'/profile/horse/1452964/lapford-lad', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (131, 2536720, N'
TomO''Roughley


', NULL, N'/profile/horse/2536720/tom-oroughley', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (132, 2283824, N'
JemimaP


', NULL, N'/profile/horse/2283824/jemima-p', N'
110', N'
143')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (133, 2536703, N'
TheGoldenRebel


', NULL, N'/profile/horse/2536703/the-golden-rebel', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (134, 906777, N'
NoComment


', NULL, N'/profile/horse/906777/no-comment', N'
114', N'
143')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (135, 2378946, N'
FiniskRiver


', NULL, N'/profile/horse/2378946/finisk-river', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (136, 1726700, N'
ShiroccanRoll


', NULL, N'/profile/horse/1726700/shiroccan-roll', N'
114', N'
141')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (137, 2702905, N'
PisgahPike


', NULL, N'/profile/horse/2702905/pisgah-pike', N'
95', N'
145')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (138, 1939907, N'
Bathiva


', NULL, N'/profile/horse/1939907/bathiva', N'
101', N'
145')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (139, 1697378, N'
Leapaway


', NULL, N'/profile/horse/1697378/leapaway', N'
113', N'
149')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (140, 899839, N'
MercianPrince


', NULL, N'/profile/horse/899839/mercian-prince', N'
116', N'
147')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (141, 898821, N'
Admiral''sSunset


', NULL, N'/profile/horse/898821/admirals-sunset', N'
116', N'
139')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (142, 2944723, N'
Fifrelet


', NULL, N'/profile/horse/2944723/fifrelet', N'
102', N'
112')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (143, 3257912, N'
AnnualReview


', NULL, N'/profile/horse/3257912/annual-review', N'
-', N'
105')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (144, 3807130, N'
BrodForceOne


', NULL, N'/profile/horse/3807130/brod-force-one', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (145, 3267432, N'
TurpinGold


', NULL, N'/profile/horse/3267432/turpin-gold', N'
99', N'
110')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (146, 3753478, N'
DeeDayLanding


', NULL, N'/profile/horse/3753478/dee-day-landing', N'
-', N'
86')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (147, 3807129, N'
FineByMe


', NULL, N'/profile/horse/3807129/fine-by-me', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (148, 3807128, N'
SteveBackshall


', NULL, N'/profile/horse/3807128/steve-backshall', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (149, 3154609, N'
BabyMoonbeam


', NULL, N'/profile/horse/3154609/baby-moonbeam', N'
70', N'
117')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (150, 1407532, N'
PushTheTempo


', NULL, N'/profile/horse/1407532/push-the-tempo', N'
112', N'
128')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (151, 1980745, N'
DrumleeWatar


', NULL, N'/profile/horse/1980745/drumlee-watar', N'
96', N'
130')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (152, 1888998, N'
StoryOfFriends


', NULL, N'/profile/horse/1888998/story-of-friends', N'
99', N'
128')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (153, 1825613, N'
SomeDetail


', NULL, N'/profile/horse/1825613/some-detail', N'
109', N'
129')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (154, 1939951, N'
Thunderstruck


', NULL, N'/profile/horse/1939951/thunderstruck', N'
112', N'
131')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (155, 2750185, N'
DruimSamhraidh


', NULL, N'/profile/horse/2750185/druim-samhraidh', N'
79', N'
125')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (156, 1954854, N'
LiffeydaleDreamer


', NULL, N'/profile/horse/1954854/liffeydale-dreamer', N'
87', N'
127')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (157, 1954948, N'
KilkeaskinMolly


', NULL, N'/profile/horse/1954948/kilkeaskin-molly', N'
117', N'
127')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (158, 1156114, N'
ArabMoon


', NULL, N'/profile/horse/1156114/arab-moon', N'
-', N'
55')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (159, 3248376, N'
BilboaRiver


', NULL, N'/profile/horse/3248376/bilboa-river', N'
101', N'
108')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (160, 2949604, N'
Duneomeno


', NULL, N'/profile/horse/2949604/duneomeno', N'
-', N'
82')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (161, 2984467, N'
Foillan


', NULL, N'/profile/horse/2984467/foillan', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (162, 3072064, N'
Killane


', NULL, N'/profile/horse/3072064/killane', N'
12', N'
72')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (163, 3204172, N'
LuttrellLad


', NULL, N'/profile/horse/3204172/luttrell-lad', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (164, 2932986, N'
MidnightWave


', NULL, N'/profile/horse/2932986/midnight-wave', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (165, 1465239, N'
ParaMio


', NULL, N'/profile/horse/1465239/para-mio', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (166, 3189678, N'
Pittsburg


', NULL, N'/profile/horse/3189678/pittsburg', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (167, 2949546, N'
PrioryWood


', NULL, N'/profile/horse/2949546/priory-wood', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (168, 3265209, N'
Whistleinthedark


', NULL, N'/profile/horse/3265209/whistleinthedark', N'
55', N'
104')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (169, 2821421, N'
Balzac


', NULL, N'/profile/horse/2821421/balzac', N'
56', N'
91')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (170, 3461269, N'
StormingCarlos


', NULL, N'/profile/horse/3461269/storming-carlos', N'
20', N'
102')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (171, 3689307, N'
Iroseaboveitall


', NULL, N'/profile/horse/3689307/iroseaboveitall', N'
27', N'
56')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (172, 3713237, N'
PaddyK


', NULL, N'/profile/horse/3713237/paddy-k', N'
32', N'
79')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (173, 3751660, N'
SurreyTerritories


', NULL, N'/profile/horse/3751660/surrey-territories', N'
-', N'
31')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (174, 3550626, N'
ZainNibras


', NULL, N'/profile/horse/3550626/zain-nibras', N'
76', N'
80')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (175, 3366777, N'
Arclight


', NULL, N'/profile/horse/3366777/arclight', N'
50', N'
64')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (176, 3765253, N'
Astrobrio


', NULL, N'/profile/horse/3765253/astrobrio', N'
-', N'
72')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (177, 3776071, N'
JustAnInkling


', NULL, N'/profile/horse/3776071/just-an-inkling', N'
44', N'
74')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (178, 3717672, N'
LaDuchesse


', NULL, N'/profile/horse/3717672/la-duchesse', N'
35', N'
83')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (179, 3625168, N'
Rubbeldiekatz


', NULL, N'/profile/horse/3625168/rubbeldiekatz', N'
73', N'
80')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (180, 3723428, N'
UnderOath


', NULL, N'/profile/horse/3723428/under-oath', N'
44', N'
63')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (181, 2810446, N'
SummerValley


', NULL, N'/profile/horse/2810446/summer-valley', N'
63', N'
77')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (182, 3294856, N'
Shielded


', NULL, N'/profile/horse/3294856/shielded', N'
2', N'
73')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (183, 3048867, N'
CubanCigar


', NULL, N'/profile/horse/3048867/cuban-cigar', N'
63', N'
71')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (184, 3161477, N'
CoupeDeChampagne


', NULL, N'/profile/horse/3161477/coupe-de-champagne', N'
49', N'
73')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (185, 2219486, N'
CafeSydney


', NULL, N'/profile/horse/2219486/cafe-sydney', N'
48', N'
74')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (186, 2280308, N'
Thunderoad


', NULL, N'/profile/horse/2280308/thunderoad', N'
67', N'
74')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (187, 3152149, N'
TurnOfPhrase


', NULL, N'/profile/horse/3152149/turn-of-phrase', N'
57', N'
63')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (188, 2669623, N'
CapricornPrince


', NULL, N'/profile/horse/2669623/capricorn-prince', N'
-', N'
55')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (189, 1325375, N'
Voi


', NULL, N'/profile/horse/1325375/voi', N'
50', N'
61')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (190, 3137835, N'
HoovesLikeJagger


', NULL, N'/profile/horse/3137835/hooves-like-jagger', N'
54', N'
60')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (191, 3177348, N'
MissTiki


', NULL, N'/profile/horse/3177348/miss-tiki', N'
43', N'
61')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (192, 3315591, N'
DayTrader


', NULL, N'/profile/horse/3315591/day-trader', N'
54', N'
63')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (193, 3536944, N'
SenseOfHumour


', NULL, N'/profile/horse/3536944/sense-of-humour', N'
62', N'
85')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (194, 3240251, N'
Yanifer


', NULL, N'/profile/horse/3240251/yanifer', N'
68', N'
86')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (195, 3308255, N'
Tellmeyourstory


', NULL, N'/profile/horse/3308255/tellmeyourstory', N'
79', N'
86')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (196, 3123852, N'
RiverWharfe


', NULL, N'/profile/horse/3123852/river-wharfe', N'
64', N'
86')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (197, 3134083, N'
Marselan


', NULL, N'/profile/horse/3134083/marselan', N'
56', N'
87')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (198, 3220075, N'
Discomatic


', NULL, N'/profile/horse/3220075/discomatic', N'
81', N'
88')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (199, 3246099, N'
ToronadoGrey


', NULL, N'/profile/horse/3246099/toronado-grey', N'
55', N'
78')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (200, 3243611, N'
TurquoiseKingdom


', NULL, N'/profile/horse/3243611/turquoise-kingdom', N'
68', N'
74')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (201, 3092755, N'
RitaTheCheetah


', NULL, N'/profile/horse/3092755/rita-the-cheetah', N'
13', N'
68')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (202, 3326372, N'
Valentinka


', NULL, N'/profile/horse/3326372/valentinka', N'
48', N'
75')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (203, 2782553, N'
Ivadream


', NULL, N'/profile/horse/2782553/ivadream', N'
66', N'
74')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (204, 3153457, N'
OliviaMary


', NULL, N'/profile/horse/3153457/olivia-mary', N'
45', N'
75')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (205, 2695140, N'
Luscifer


', NULL, N'/profile/horse/2695140/luscifer', N'
32', N'
76')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (206, 3098929, N'
Incorrigible


', NULL, N'/profile/horse/3098929/incorrigible', N'
65', N'
75')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (207, 2177945, N'
Atty''sEdge


', NULL, N'/profile/horse/2177945/attys-edge', N'
72', N'
78')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (208, 2734127, N'
We''reReunited


', NULL, N'/profile/horse/2734127/were-reunited', N'
62', N'
77')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (209, 2687070, N'
SirRodneyredblood


', NULL, N'/profile/horse/2687070/sir-rodneyredblood', N'
57', N'
79')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (210, 858149, N'
KingCrimson


', NULL, N'/profile/horse/858149/king-crimson', N'
67', N'
80')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (211, 929347, N'
CappanantyCon


', NULL, N'/profile/horse/929347/cappananty-con', N'
68', N'
77')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (212, 863810, N'
Essaka


', NULL, N'/profile/horse/863810/essaka', N'
62', N'
79')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (213, 3241323, N'
OceanWilde


', NULL, N'/profile/horse/3241323/ocean-wilde', N'
70', N'
77')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (214, 3513378, N'
Teasyweasy


', NULL, N'/profile/horse/3513378/teasyweasy', N'
-', N'
71')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (215, 3505916, N'
LadyDollars


', NULL, N'/profile/horse/3505916/lady-dollars', N'
17', N'
69')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (216, 3536942, N'
GlobalMirage


', NULL, N'/profile/horse/3536942/global-mirage', N'
45', N'
67')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (217, 3366783, N'
Chicanery


', NULL, N'/profile/horse/3366783/chicanery', N'
49', N'
69')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (218, 3526640, N'
WinterSiege


', NULL, N'/profile/horse/3526640/winter-siege', N'
-', N'
73')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (219, 3636548, N'
RunRabbitRun


', NULL, N'/profile/horse/3636548/run-rabbit-run', N'
-', N'
71')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (220, 3366894, N'
MythicalStar


', NULL, N'/profile/horse/3366894/mythical-star', N'
41', N'
67')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (221, 3538682, N'
Adaaydreambeliever


', NULL, N'/profile/horse/3538682/adaaydreambeliever', N'
49', N'
74')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (222, 3652160, N'
Thatsthefinest


', NULL, N'/profile/horse/3652160/thatsthefinest', N'
43', N'
74')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (223, 3437872, N'
MissRosaBella


', NULL, N'/profile/horse/3437872/miss-rosa-bella', N'
36', N'
62')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (224, 3477858, N'
DarkTerms


', NULL, N'/profile/horse/3477858/dark-terms', N'
31', N'
71')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (225, 3660580, N'
Cozican


', NULL, N'/profile/horse/3660580/cozican', N'
-', N'
70')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (226, 3547603, N'
CharmingWally


', NULL, N'/profile/horse/3547603/charming-wally', N'
49', N'
70')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (227, 3577242, N'
AilishT


', NULL, N'/profile/horse/3577242/ailish-t', N'
15', N'
69')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (228, 3577241, N'
Offering


', NULL, N'/profile/horse/3577241/offering', N'
33', N'
64')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (229, 3544309, N'
PrettyGreen


', NULL, N'/profile/horse/3544309/pretty-green', N'
50', N'
68')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (230, 3366979, N'
IndianGuru


', NULL, N'/profile/horse/3366979/indian-guru', N'
51', N'
57')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (231, 3438023, N'
Ardita


', NULL, N'/profile/horse/3438023/ardita', N'
-', N'
68')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (232, 3616095, N'
LangleyLady


', NULL, N'/profile/horse/3616095/langley-lady', N'
38', N'
67')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (233, 3678085, N'
StrawberryLola


', NULL, N'/profile/horse/3678085/strawberry-lola', N'
-', N'
49')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (234, 3807118, N'
AlTilal


', NULL, N'/profile/horse/3807118/al-tilal', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (235, 3759239, N'
BearToDream


', NULL, N'/profile/horse/3759239/bear-to-dream', N'
46', N'
64')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (236, 3789371, N'
EeshaMeesh


', NULL, N'/profile/horse/3789371/eesha-meesh', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (237, 3779995, N'
IvyRosie


', NULL, N'/profile/horse/3779995/ivy-rosie', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (238, 3612338, N'
LilGuff


', NULL, N'/profile/horse/3612338/lil-guff', N'
-', N'
85')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (239, 3538679, N'
ModernArtist


', NULL, N'/profile/horse/3538679/modern-artist', N'
47', N'
69')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (240, 3666414, N'
PsychicChange


', NULL, N'/profile/horse/3666414/psychic-change', N'
-', N'
75')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (241, 3366791, N'
PureCharmer


', NULL, N'/profile/horse/3366791/pure-charmer', N'
69', N'
84')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (242, 3550645, N'
RenegadeRose


', NULL, N'/profile/horse/3550645/renegade-rose', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (243, 3785503, N'
RoseberryTopping


', NULL, N'/profile/horse/3785503/roseberry-topping', N'
-', N'
64')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (244, 3717680, N'
SaltaResta


', NULL, N'/profile/horse/3717680/salta-resta', N'
28', N'
59')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (245, 3678084, N'
YouAreMyWorld


', NULL, N'/profile/horse/3678084/you-are-my-world', N'
-', N'
67')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (246, 3550636, N'
AureliaGold


', NULL, N'/profile/horse/3550636/aurelia-gold', N'
-', N'
65')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (247, 3793540, N'
BrownMouse


', NULL, N'/profile/horse/3793540/brown-mouse', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (248, 3703480, N'
CapeCornwallRose


', NULL, N'/profile/horse/3703480/cape-cornwall-rose', N'
19', N'
68')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (249, 3741019, N'
Commandment


', NULL, N'/profile/horse/3741019/commandment', N'
-', N'
90')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (250, 3679952, N'
EternalLove


', NULL, N'/profile/horse/3679952/eternal-love', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (251, 3807116, N'
Fiorina


', NULL, N'/profile/horse/3807116/fiorina', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (252, 3781895, N'
FreshFancy


', NULL, N'/profile/horse/3781895/fresh-fancy', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (253, 3785509, N'
MoreDiamonds


', NULL, N'/profile/horse/3785509/more-diamonds', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (254, 3741022, N'
PenguinIsland


', NULL, N'/profile/horse/3741022/penguin-island', N'
-', N'
71')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (255, 3807114, N'
Rakurai


', NULL, N'/profile/horse/3807114/rakurai', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (256, 3789372, N'
Ramadhaan


', NULL, N'/profile/horse/3789372/ramadhaan', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (257, 3668359, N'
Shoutout


', NULL, N'/profile/horse/3668359/shoutout', N'
-', N'
56')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (258, 3749521, N'
Thakrah


', NULL, N'/profile/horse/3749521/thakrah', N'
-', N'
90')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (259, 3721432, N'
TokenTrader


', NULL, N'/profile/horse/3721432/token-trader', N'
45', N'
80')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (260, 2654579, N'
CryHavoc


', NULL, N'/profile/horse/2654579/cry-havoc', N'
-', N'
99')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (261, 3257934, N'
DivineMagic


', NULL, N'/profile/horse/3257934/divine-magic', N'
-', N'
97')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (262, 3116613, N'
GreatVibes


', NULL, N'/profile/horse/3116613/great-vibes', N'
-', N'
92')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (263, 3172537, N'
Beheld


', NULL, N'/profile/horse/3172537/beheld', N'
59', N'
100')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (264, 3057765, N'
FlameOfFreedom


', NULL, N'/profile/horse/3057765/flame-of-freedom', N'
61', N'
95')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (265, 3217630, N'
SealedOffer


', NULL, N'/profile/horse/3217630/sealed-offer', N'
77', N'
95')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (266, 2192346, N'
CubanHope


', NULL, N'/profile/horse/2192346/cuban-hope', N'
67', N'
89')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (267, 1208903, N'
Mutarabby


', NULL, N'/profile/horse/1208903/mutarabby', N'
72', N'
82')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (268, 2617020, N'
LexingtonForce


', NULL, N'/profile/horse/2617020/lexington-force', N'
66', N'
82')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (269, 2445599, N'
Where''sTom


', NULL, N'/profile/horse/2445599/wheres-tom', N'
78', N'
84')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (270, 2313719, N'
Proton


', NULL, N'/profile/horse/2313719/proton', N'
-', N'
-')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (271, 2537440, N'
FrozenWaters


', NULL, N'/profile/horse/2537440/frozen-waters', N'
-', N'
79')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (272, 3552555, N'
LibertyWarrior


', NULL, N'/profile/horse/3552555/liberty-warrior', N'
74', N'
80')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (273, 3102117, N'
SpringGlow


', NULL, N'/profile/horse/3102117/spring-glow', N'
68', N'
81')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (274, 3250955, N'
BuxtedToo


', NULL, N'/profile/horse/3250955/buxted-too', N'
-', N'
81')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (275, 3420533, N'
MakethBelieveth


', NULL, N'/profile/horse/3420533/maketh-believeth', N'
70', N'
80')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (276, 3222734, N'
AlphaKing


', NULL, N'/profile/horse/3222734/alpha-king', N'
72', N'
76')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (277, 2996571, N'
DiamondBay


', NULL, N'/profile/horse/2996571/diamond-bay', N'
51', N'
81')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (278, 3515045, N'
LionFace


', NULL, N'/profile/horse/3515045/lion-face', N'
75', N'
80')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (279, 3616036, N'
ForLoveOfLouise


', NULL, N'/profile/horse/3616036/for-love-of-louise', N'
-', N'
63')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (280, 3233458, N'
Sundayinmay


', NULL, N'/profile/horse/3233458/sundayinmay', N'
56', N'
66')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (281, 3156660, N'
SaulireStar


', NULL, N'/profile/horse/3156660/saulire-star', N'
51', N'
74')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (282, 3261271, N'
Soyuz


', NULL, N'/profile/horse/3261271/soyuz', N'
48', N'
73')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (283, 3249769, N'
MustangKodi


', NULL, N'/profile/horse/3249769/mustang-kodi', N'
46', N'
73')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (284, 3287653, N'
UrbanForest


', NULL, N'/profile/horse/3287653/urban-forest', N'
-', N'
68')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (285, 3217838, N'
Williewinamillion


', NULL, N'/profile/horse/3217838/williewinamillion', N'
-', N'
67')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (286, 3244628, N'
AuthoraDream


', NULL, N'/profile/horse/3244628/authora-dream', N'
13', N'
64')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (287, 3493739, N'
Padraig


', NULL, N'/profile/horse/3493739/padraig', N'
19', N'
61')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (288, 3493740, N'
Intoxication


', NULL, N'/profile/horse/3493740/intoxication', N'
30', N'
62')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (289, 3162620, N'
ViveLaDanse


', NULL, N'/profile/horse/3162620/vive-la-danse', N'
58', N'
68')
GO
INSERT [dbo].[tb_horse] ([horse_id], [rp_horse_id], [horse_name], [dob], [horse_url], [top_speed], [rpr]) VALUES (290, 3662409, N'
OwenLittle


', NULL, N'/profile/horse/3662409/owen-little', N'
18', N'
51')
GO
SET IDENTITY_INSERT [dbo].[tb_horse] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_jockey] ON 
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (1, N'
CliffordLee


', N'/profile/jockey/93480/clifford-lee')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (2, N'
BenRobinson


', N'/profile/jockey/95279/ben-robinson')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (3, N'
DuranFentiman


', N'/profile/jockey/82122/duran-fentiman')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (4, N'
DavidAllan


', N'/profile/jockey/78223/david-allan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (5, N'
HarrisonShaw


', N'/profile/jockey/96211/harrison-shaw')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (6, N'
ConnorBeasley


', N'/profile/jockey/90868/connor-beasley')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (7, N'
DanielTudhope


', N'/profile/jockey/82231/daniel-tudhope')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (8, N'
PaulMulrennan


', N'/profile/jockey/79371/paul-mulrennan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (9, N'
JasonHart


', N'/profile/jockey/90243/jason-hart')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (10, N'
GrahamLee


', N'/profile/jockey/10590/graham-lee')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (11, N'
AlexanderThorne


', N'/profile/jockey/94334/alexander-thorne')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (12, N'
TomBuckley


', N'/profile/jockey/97944/tom-buckley')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (13, N'
PatrickCowley


', N'/profile/jockey/93110/patrick-cowley')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (14, N'
LewisStones


', N'/profile/jockey/93122/lewis-stones')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (15, N'
LiamHarrison


', N'/profile/jockey/98445/liam-harrison')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (16, N'
KevinJones


', N'/profile/jockey/92841/kevin-jones')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (17, N'
BenGodfrey


', N'/profile/jockey/96166/ben-godfrey')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (18, N'
KevinBrogan


', N'/profile/jockey/96895/kevin-brogan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (19, N'
JackTudor


', N'/profile/jockey/98673/jack-tudor')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (20, N'
TabithaWorsley


', N'/profile/jockey/91902/tabitha-worsley')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (21, N'
HayleyTurner


', N'/profile/jockey/78920/hayley-turner')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (22, N'
PatCosgrave


', N'/profile/jockey/14629/pat-cosgrave')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (23, N'
FrannyNorton


', N'/profile/jockey/7458/franny-norton')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (24, N'
CallumHutchinson


', N'/profile/jockey/100360/callum-hutchinson')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (25, N'
RayDawson


', N'/profile/jockey/88798/ray-dawson')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (26, N'
WilliamCox


', N'/profile/jockey/95208/william-cox')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (27, N'
JoeFanning


', N'/profile/jockey/2572/joe-fanning')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (28, N'
WilliamBuick


', N'/profile/jockey/85793/william-buick')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (29, N'
JamesDoyle


', N'/profile/jockey/6901/james-doyle')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (30, N'
RobHornby


', N'/profile/jockey/92774/rob-hornby')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (31, N'
JasonWatson


', N'/profile/jockey/96222/jason-watson')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (32, N'
KieranShoemark


', N'/profile/jockey/93237/kieran-shoemark')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (33, N'
EllieMacKenzie


', N'/profile/jockey/95072/ellie-mackenzie')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (34, N'
HarryBurns


', N'/profile/jockey/92486/harry-burns')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (35, N'
CallumShepherd


', N'/profile/jockey/93343/callum-shepherd')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (36, N'
DavidProbert


', N'/profile/jockey/86013/david-probert')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (37, N'
DavidEgan


', N'/profile/jockey/95388/david-egan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (38, N'
PaddyMathers


', N'/profile/jockey/79115/paddy-mathers')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (39, N'
TomMarquand


', N'/profile/jockey/93947/tom-marquand')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (40, N'
FayeMcManoman


', N'/profile/jockey/96409/faye-mcmanoman')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (41, N'
CallumRodriguez


', N'/profile/jockey/94225/callum-rodriguez')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (42, N'
JosephineGordon


', N'/profile/jockey/92801/josephine-gordon')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (43, N'
JackGarritty


', N'/profile/jockey/92735/jack-garritty')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (44, N'
TonyHamilton


', N'/profile/jockey/79004/tony-hamilton')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (45, N'
TomEaves


', N'/profile/jockey/76831/tom-eaves')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (46, N'
JoannaMason


', N'/profile/jockey/85782/joanna-mason')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (47, N'
JamesSullivan


', N'/profile/jockey/84541/james-sullivan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (48, N'
JaneElliott


', N'/profile/jockey/92778/jane-elliott')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (49, N'
CamHardie


', N'/profile/jockey/92732/cam-hardie')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (50, N'
KieranSchofield


', N'/profile/jockey/92224/kieran-schofield')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (51, N'
NON-RUNNER


', N'/profile/jockey/74971/non-runner')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (52, N'
ElishaWhittington


', N'/profile/jockey/96720/elisha-whittington')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (53, N'
ShaneGray


', N'/profile/jockey/90103/shane-gray')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (54, N'
JonathanFisher


', N'/profile/jockey/94853/jonathan-fisher')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (55, N'
TheodoreLadd


', N'/profile/jockey/95741/theodore-ladd')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (56, N'
JamieGormley


', N'/profile/jockey/92961/jamie-gormley')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (57, N'
BarryMcHugh


', N'/profile/jockey/78398/barry-mchugh')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (58, N'
SamJames


', N'/profile/jockey/87290/sam-james')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (59, N'
AndrewElliott


', N'/profile/jockey/82642/andrew-elliott')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (60, N'
BillyGarritty


', N'/profile/jockey/95152/billy-garritty')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (61, N'
ZakWheatley


', N'/profile/jockey/96669/zak-wheatley')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (62, N'
TheoGillard


', N'/profile/jockey/93874/theo-gillard')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (63, N'
JackSavage


', N'/profile/jockey/92085/jack-savage')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (64, N'
TomMidgley


', N'/profile/jockey/97204/tom-midgley')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (65, N'
SeanHoulihan


', N'/profile/jockey/94129/sean-houlihan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (66, N'
TomBellamy


', N'/profile/jockey/89945/tom-bellamy')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (67, N'
MrJackAndrews


', N'/profile/jockey/98137/mr-jack-andrews')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (68, N'
TomO''Brien


', N'/profile/jockey/82134/tom-obrien')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (69, N'
BrianHughes


', N'/profile/jockey/81409/brian-hughes')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (70, N'
AdamWedge


', N'/profile/jockey/86912/adam-wedge')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (71, N'
GavinSheehan


', N'/profile/jockey/88907/gavin-sheehan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (72, N'
PaddyBrennan


', N'/profile/jockey/76635/paddy-brennan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (73, N'
JackQuinlan


', N'/profile/jockey/87528/jack-quinlan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (74, N'
PageFuller


', N'/profile/jockey/92510/page-fuller')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (75, N'
KielanWoods


', N'/profile/jockey/88884/kielan-woods')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (76, N'
MrJohnReddington


', N'/profile/jockey/100528/mr-john-reddington')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (77, N'
RossChapman


', N'/profile/jockey/92580/ross-chapman')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (78, N'
SamTwiston-Davies


', N'/profile/jockey/88023/sam-twiston-davies')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (79, N'
BenPoste


', N'/profile/jockey/88090/ben-poste')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (80, N'
DavidNoonan


', N'/profile/jockey/91533/david-noonan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (81, N'
CaoilinQuinn


', N'/profile/jockey/99249/caoilin-quinn')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (82, N'
FergusGillard


', N'/profile/jockey/98636/fergus-gillard')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (83, N'
BrendanPowell


', N'/profile/jockey/90406/brendan-powell')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (84, N'
JonathanEngland


', N'/profile/jockey/87958/jonathan-england')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (85, N'
WilliamKennedy


', N'/profile/jockey/76669/william-kennedy')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (86, N'
NickScholfield


', N'/profile/jockey/85957/nick-scholfield')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (87, N'
HarryKimber


', N'/profile/jockey/98637/harry-kimber')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (88, N'
PaulO''Brien


', N'/profile/jockey/92112/paul-obrien')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (89, N'
RichieMcLernon


', N'/profile/jockey/83698/richie-mclernon')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (90, N'
AlainCawley


', N'/profile/jockey/83595/alain-cawley')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (91, N'
BenJones


', N'/profile/jockey/96449/ben-jones')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (92, N'
GeorgeBuckell


', N'/profile/jockey/90903/george-buckell')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (93, N'
RyanTate


', N'/profile/jockey/90428/ryan-tate')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (94, N'
GeorgeWood


', N'/profile/jockey/94575/george-wood')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (95, N'
MarcoGhiani


', N'/profile/jockey/98301/marco-ghiani')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (96, N'
SaffieOsborne


', N'/profile/jockey/100350/saffie-osborne')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (97, N'
WilliamCarver


', N'/profile/jockey/97319/william-carver')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (98, N'
AidanKeeley


', N'/profile/jockey/100531/aidan-keeley')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (99, N'
LukeCatton


', N'/profile/jockey/95522/luke-catton')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (100, N'
JonnyPeate


', N'/profile/jockey/100783/jonny-peate')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (101, N'
MolliePhillips


', N'/profile/jockey/99317/mollie-phillips')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (102, N'
EmmaTaff


', N'/profile/jockey/95755/emma-taff')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (103, N'
JFEgan


', N'/profile/jockey/2456/j-f-egan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (104, N'
HectorCrouch


', N'/profile/jockey/92806/hector-crouch')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (105, N'
LauraPearson


', N'/profile/jockey/98602/laura-pearson')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (106, N'
SamHitchcott


', N'/profile/jockey/79244/sam-hitchcott')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (107, N'
RossCoakley


', N'/profile/jockey/89973/ross-coakley')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (108, N'
CharlesBishop


', N'/profile/jockey/89627/charles-bishop')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (109, N'
RhysClutterbuck


', N'/profile/jockey/98254/rhys-clutterbuck')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (110, N'
HowardCheng


', N'/profile/jockey/99856/howard-cheng')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (111, N'
SebastianWoods


', N'/profile/jockey/96233/sebastian-woods')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (112, N'
ChristianHowarth


', N'/profile/jockey/100380/christian-howarth')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (113, N'
TrevorWhelan


', N'/profile/jockey/86570/trevor-whelan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (114, N'
RossaRyan


', N'/profile/jockey/95744/rossa-ryan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (115, N'
GraceMcEntee


', N'/profile/jockey/98171/grace-mcentee')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (116, N'
WilliamCarson


', N'/profile/jockey/84882/william-carson')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (117, N'
DarraghKeenan


', N'/profile/jockey/95815/darragh-keenan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (118, N'
EoinWalsh


', N'/profile/jockey/91088/eoin-walsh')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (119, N'
HollieDoyle


', N'/profile/jockey/92695/hollie-doyle')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (120, N'
GeorgiaDobie


', N'/profile/jockey/95492/georgia-dobie')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (121, N'
LukeMorris


', N'/profile/jockey/84857/luke-morris')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (122, N'
RowanScott


', N'/profile/jockey/92565/rowan-scott')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (123, N'
StefanoCherchi


', N'/profile/jockey/98476/stefano-cherchi')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (124, N'
RichardKingscote


', N'/profile/jockey/83554/richard-kingscote')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (125, N'
MollyPresland


', N'/profile/jockey/99804/molly-presland')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (126, N'
DylanHogan


', N'/profile/jockey/92620/dylan-hogan')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (127, N'
RhiainIngram


', N'/profile/jockey/93076/rhiain-ingram')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (128, N'
CierenFallon


', N'/profile/jockey/98519/cieren-fallon')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (129, N'
MartinDwyer


', N'/profile/jockey/11255/martin-dwyer')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (130, N'
PatDobbs


', N'/profile/jockey/75506/pat-dobbs')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (131, N'
DaneO''Neill


', N'/profile/jockey/13317/dane-oneill')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (132, N'
JimCrowley


', N'/profile/jockey/14234/jim-crowley')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (133, N'
PJMcDonald


', N'/profile/jockey/76872/p-j-mcdonald')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (134, N'
JackMitchell


', N'/profile/jockey/84416/jack-mitchell')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (135, N'
PhilipPrince


', N'/profile/jockey/88895/philip-prince')
GO
INSERT [dbo].[tb_jockey] ([jockey_id], [jockey_name], [jockey_url]) VALUES (136, N'
GeorgeRooke


', N'/profile/jockey/98260/george-rooke')
GO
SET IDENTITY_INSERT [dbo].[tb_jockey] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_race] ON 
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (209, 461, N'13:00', 791651, N'(Rain)', 10, N'Good To Firm', N'(STALLS Inside)', N'7f', 6, N'

(2yo)
', N'
                                    JW 4x4 Northallerton Restricted Novice Stakes (For Horses In Band D)                                ', N'/racecards/80/thirsk/2021-09-13/791651')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (210, 461, N'13:30', 791652, N'(Rain)', 20, N'Good To Firm', N'(STALLS Centre)', N'6f', 6, N'

(2yo0-65)
', N'
                                    Ideal Conference & Events Venue @ThirskRaces Nursery Handicap                                ', N'/racecards/80/thirsk/2021-09-13/791652')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (211, 461, N'14:00', 791650, N'(Rain)', 18, N'Good To Firm', N'(STALLS Centre)', N'6f', 6, N'

(3-6yo0-55)
', N'
                                    Every Race Live On RacingTV Selling Handicap                                ', N'/racecards/80/thirsk/2021-09-13/791650')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (212, 461, N'14:30', 791646, N'(Rain)', 5, N'Good To Firm', N'(STALLS Outside)', N'1m4f8y', 4, N'

(3yo+)
', N'
                                    British EBF Fillies'' Novice Stakes                                ', N'/racecards/80/thirsk/2021-09-13/791646')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (213, 461, N'15:05', 791649, N'(Rain)', 6, N'Good To Firm', N'(STALLS Outside)', N'1m4f8y', 5, N'

(3yo+0-70)
', N'
                                    Patrick Hibbert-Foy Memorial Handicap                                ', N'/racecards/80/thirsk/2021-09-13/791649')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (214, 461, N'15:40', 791648, N'(Rain)', 9, N'Good To Firm', N'(STALLS Inside)', N'7f218y', 5, N'

(3yo+0-75)
', N'
                                    Steve Kelsey 40-Years And He''s Still Smiling Fillies'' Handicap                                ', N'/racecards/80/thirsk/2021-09-13/791648')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (215, 461, N'16:15', 791647, N'(Rain)', 12, N'Good To Firm', N'(STALLS Inside)', N'7f', 5, N'

(4yo+0-75)
', N'
                                    Follow @ThirskRaces For 2022 Ticketing News Handicap (Div I)                                ', N'/racecards/80/thirsk/2021-09-13/791647')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (216, 461, N'16:50', 793325, N'(Rain)', 12, N'Good To Firm', N'(STALLS Inside)', N'7f', 5, N'

(4yo+0-75)
', N'
                                    Follow @ThirskRaces For 2022 Ticketing News Handicap (Div II)                                ', N'/racecards/80/thirsk/2021-09-13/793325')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (217, 462, N'13:15', 791570, N'(Rain)', 10, N'Good', N'', N'2m7f', 5, N'

(4yo+0-100)
', N'
                                    MansionBet Bet £10 Get £20 Conditional Jockeys'' Handicap Chase (Div I)                                ', N'/racecards/101/worcester/2021-09-13/791570')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (218, 462, N'13:45', 793326, N'(Rain)', 8, N'Good', N'', N'2m7f', 5, N'

(4yo+0-100)
', N'
                                    MansionBet Bet £10 Get £20 Conditional Jockeys'' Handicap Chase (Div II)                                ', N'/racecards/101/worcester/2021-09-13/793326')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (219, 462, N'14:20', 791567, N'(Rain)', 5, N'Good', N'', N'2m4f', 3, N'

(4yo+)
', N'
                                    MansionBet Watch And Bet Novices'' Limited Handicap Chase (GBB Race)                                ', N'/racecards/101/worcester/2021-09-13/791567')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (220, 462, N'14:50', 791566, N'(Rain)', 5, N'Good', N'', N'2m110y', 2, N'

(4yo+0-150)
', N'
                                    Cazoo Handicap Chase (GBB Race)                                ', N'/racecards/101/worcester/2021-09-13/791566')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (221, 462, N'15:25', 791571, N'(Rain)', 8, N'Good', N'', N'2m', 5, N'

(4-6yo)
', N'
                                    MansionBet Best Odds Guaranteed Open National Hunt Flat Race (Category 1 Elimination) (GBB Race)                                ', N'/racecards/101/worcester/2021-09-13/791571')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (222, 462, N'16:00', 791568, N'(Rain)', 6, N'Good', N'', N'2m7f', 3, N'

(4yo+0-125)
', N'
                                    Cazoo Handicap Hurdle                                ', N'/racecards/101/worcester/2021-09-13/791568')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (223, 462, N'16:35', 791569, N'(Rain)', 14, N'Good', N'', N'2m', 4, N'

(4yo+)
', N'
                                    MansionBet Beaten By A Head Maiden Hurdle (Arc Summer Novices'' Brush Hurdle Qualifier) (GBB Race)                                ', N'/racecards/101/worcester/2021-09-13/791569')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (224, 463, N'14:10', 791639, N'(Light rain)', 5, N'Good To Firm', N'(STALLS Centre)', N'5f215y', 4, N'

(3yo+0-80)
', N'
                                    whichbookie.co.uk Free Racing Tips Handicap                                ', N'/racecards/7/brighton/2021-09-13/791639')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (225, 463, N'14:40', 791645, N'(Light rain)', 8, N'Good To Firm', N'(STALLS Centre)', N'6f210y', 6, N'

(2yo)
', N'
                                    whichbookie.co.uk Free Bets Restricted Maiden Stakes (For Horses In Band D) (IRE Incentive Race)                                ', N'/racecards/7/brighton/2021-09-13/791645')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (226, 463, N'15:15', 791640, N'(Light rain)', 6, N'Good To Firm', N'(STALLS Centre)', N'1m1f207y', 5, N'

(3yo+0-70)
', N'
                                    whichbookie.co.uk Bookmaker Reviews Apprentice Handicap                                ', N'/racecards/7/brighton/2021-09-13/791640')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (227, 463, N'15:50', 791642, N'(Light rain)', 5, N'Good To Firm', N'(STALLS Outside)', N'1m3f198y', 6, N'

(3yo+0-55)
', N'
                                    whichbookie.co.uk Free Football Tips Handicap                                ', N'/racecards/7/brighton/2021-09-13/791642')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (228, 463, N'16:25', 791641, N'(Light rain)', 6, N'Good To Firm', N'(STALLS Centre)', N'7f216y', 5, N'

(3yo+0-75)
', N'
                                    whichbookie.co.uk Best For Racing Handicap                                ', N'/racecards/7/brighton/2021-09-13/791641')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (229, 463, N'17:00', 791643, N'(Light rain)', 8, N'Good To Firm', N'(STALLS Centre)', N'6f210y', 6, N'

(3yo+0-65)
', N'
                                    whichbookie.co.uk Best For Football Handicap                                ', N'/racecards/7/brighton/2021-09-13/791643')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (230, 463, N'17:30', 791644, N'(Light rain)', 6, N'Good To Firm', N'(STALLS Centre)', N'5f60y', 6, N'

(3yo+0-65)
', N'
                                    whichbookie.co.uk Betting Offers Handicap                                ', N'/racecards/7/brighton/2021-09-13/791644')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (231, 464, N'16:45', 791873, N'(Mostly cloudy)', 10, N'Standard To Slow', N'(STALLS Inside)', N'1m', 6, N'

(2yo0-65)
', N'
                                    Try Our New Runner Boost At Unibet Nursery Handicap                                ', N'/racecards/1079/kempton-aw/2021-09-13/791873')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (232, 464, N'17:15', 791874, N'(Mostly cloudy)', 9, N'Standard To Slow', N'(STALLS Inside)', N'6f', 6, N'

(2yo0-60)
', N'
                                    Unibet Casino Deposit £10 Get £40 Bonus Nursery Handicap (Div I)                                ', N'/racecards/1079/kempton-aw/2021-09-13/791874')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (233, 464, N'17:45', 793324, N'(Mostly cloudy)', 10, N'Standard To Slow', N'(STALLS Inside)', N'6f', 6, N'

(2yo0-60)
', N'
                                    Unibet Casino Deposit £10 Get £40 Bonus Nursery Handicap (Div II)                                ', N'/racecards/1079/kempton-aw/2021-09-13/793324')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (234, 464, N'18:15', 791870, N'(Mostly cloudy)', 12, N'Standard To Slow', N'(STALLS Inside)', N'6f', 5, N'

(2yo)
', N'
                                    Unibet/British Stallion Studs EBF Fillies'' Restricted Novice Stakes (Horses In Band D) (GBB Race)                                ', N'/racecards/1079/kempton-aw/2021-09-13/791870')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (235, 464, N'18:45', 791869, N'(Mostly cloudy)', 11, N'Standard To Slow', N'(STALLS Inside)', N'7f', 5, N'

(2yo)
', N'
                                    Unibet Extra Place Offers Every Day EBF Maiden Fillies'' Stakes (GBB Race)                                ', N'/racecards/1079/kempton-aw/2021-09-13/791869')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (236, 464, N'19:15', 791868, N'(Mostly cloudy)', 6, N'Standard To Slow', N'(STALLS Inside)', N'7f', 3, N'

(3yo+0-95)
', N'
                                    Unibet 3 Uniboosts A Day Fillies'' Handicap                                ', N'/racecards/1079/kempton-aw/2021-09-13/791868')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (237, 464, N'19:45', 791871, N'(Mostly cloudy)', 13, N'Standard To Slow', N'(STALLS Inside)', N'1m3f219y', 5, N'

(3yo+0-75)
', N'
                                    Try Our New Super Boosts At Unibet Handicap                                ', N'/racecards/1079/kempton-aw/2021-09-13/791871')
GO
INSERT [dbo].[tb_race] ([race_id], [event_id], [race_time], [rp_race_id], [weather], [no_of_horses], [going], [stalls], [distance], [race_class], [ages], [description], [race_url]) VALUES (238, 464, N'20:15', 791872, N'(Mostly cloudy)', 12, N'Standard To Slow', N'(STALLS Inside)', N'1m3f219y', 6, N'

(3yo0-55)
', N'
                                    Unibet New Instant Roulette Handicap                                ', N'/racecards/1079/kempton-aw/2021-09-13/791872')
GO
SET IDENTITY_INSERT [dbo].[tb_race] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_race_horse] ON 
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (1, 209, 1, N'
9.
5', 2, 1, 1, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (2, 209, 2, N'
9.
5', 2, 2, 2, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (3, 209, 3, N'
9.
5', 2, 3, 3, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (4, 209, 4, N'
9.
5', 2, 3, 4, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (5, 209, 5, N'
9.
5', 2, 4, 5, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (6, 209, 6, N'
9.
0', 2, 5, 6, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (7, 209, 7, N'
9.
0', 2, 6, 7, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (8, 209, 8, N'
9.
0', 2, 7, 8, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (9, 209, 9, N'
9.
0', 2, 8, 9, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (10, 209, 10, N'
9.
0', 2, 9, 10, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (11, 210, 39, N'
9.
8', 2, 27, 39, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (12, 210, 40, N'
9.
7', 2, 35, 7, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (13, 210, 41, N'
9.
7', 2, 36, 6, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (14, 210, 42, N'
9.
6', 2, 37, 2, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (15, 210, 43, N'
9.
6', 2, 38, 40, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (16, 210, 44, N'
9.
6', 2, 39, 41, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (17, 210, 45, N'
9.
6', 2, 40, 42, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (18, 210, 46, N'
9.
2', 2, 41, 1, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (19, 210, 47, N'
9.
1', 2, 3, 4, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (20, 210, 48, N'
9.
1', 2, 42, 43, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (21, 210, 49, N'
9.
1', 2, 7, 10, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (22, 210, 50, N'
8.
13', 2, 43, 44, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (23, 210, 51, N'
8.
13', 2, 44, 45, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (24, 210, 52, N'
8.
11', 2, 45, 5, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (25, 210, 53, N'
8.
11', 2, 46, 46, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (26, 210, 54, N'
8.
9', 2, 7, 47, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (27, 210, 55, N'
8.
7', 2, 47, 48, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (28, 210, 56, N'
8.
4', 2, 3, 3, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (29, 210, 57, N'
8.
3', 2, 48, 49, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (30, 210, 58, N'
8.
1', 2, 7, 50, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (31, 211, 59, N'
9.
8', 4, 49, 51, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (32, 211, 60, N'
9.
7', 5, 50, 52, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (33, 211, 61, N'
9.
7', 5, 51, 46, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (34, 211, 62, N'
9.
6', 4, 52, 49, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (35, 211, 63, N'
9.
5', 3, 3, 4, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (36, 211, 64, N'
9.
4', 6, 53, 53, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (37, 211, 65, N'
9.
4', 4, 54, 54, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (38, 211, 66, N'
9.
1', 5, 45, 5, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (39, 211, 67, N'
9.
1', 3, 55, 55, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (40, 211, 68, N'
9.
0', 3, 5, 8, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (41, 211, 69, N'
9.
0', 4, 55, 39, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (42, 211, 70, N'
8.
13', 3, 56, 56, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (43, 211, 71, N'
8.
13', 4, 57, 45, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (44, 211, 72, N'
8.
12', 4, 58, 42, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (45, 211, 73, N'
8.
12', 4, 59, 41, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (46, 211, 74, N'
8.
11', 3, 53, 44, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (47, 211, 75, N'
8.
10', 3, 56, 2, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (48, 211, 76, N'
8.
10', 3, 60, 47, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (49, 211, 77, N'
8.
10', 3, 9, 10, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (50, 211, 78, N'
8.
10', 3, 61, 3, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (51, 212, 79, N'
9.
6', 3, 62, 39, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (52, 212, 80, N'
8.
13', 3, 58, 4, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (53, 212, 81, N'
8.
13', 3, 63, 57, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (54, 212, 82, N'
8.
13', 3, 64, 7, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (55, 212, 83, N'
8.
13', 3, 65, 58, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (56, 213, 84, N'
10.
3', 7, 47, 46, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (57, 213, 85, N'
9.
13', 4, 35, 7, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (58, 213, 86, N'
9.
9', 3, 3, 4, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (59, 213, 87, N'
9.
7', 3, 66, 39, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (60, 213, 88, N'
9.
6', 5, 67, 2, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (61, 213, 89, N'
8.
9', 4, 45, 45, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (62, 214, 90, N'
9.
9', 3, 62, 7, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (63, 214, 91, N'
9.
5', 4, 37, 2, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (64, 214, 92, N'
9.
5', 6, 46, 4, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (65, 214, 93, N'
9.
0', 3, 68, 6, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (66, 214, 94, N'
8.
12', 4, 55, 39, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (67, 214, 95, N'
8.
12', 3, 56, 56, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (68, 214, 96, N'
8.
11', 6, 69, 47, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (69, 214, 97, N'
8.
11', 8, 70, 59, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (70, 214, 98, N'
8.
9', 4, 71, 58, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (71, 215, 99, N'
9.
9', 6, 72, 7, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (72, 215, 100, N'
9.
7', 4, 73, 39, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (73, 215, 101, N'
9.
6', 5, 34, 60, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (74, 215, 102, N'
9.
5', 6, 42, 43, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (75, 215, 103, N'
9.
5', 9, 5, 6, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (76, 215, 104, N'
9.
3', 6, 52, 49, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (77, 215, 105, N'
9.
2', 6, 74, 10, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (78, 215, 106, N'
9.
1', 4, 45, 8, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (79, 215, 107, N'
9.
1', 4, 46, 46, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (80, 215, 108, N'
9.
0', 6, 71, 4, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (81, 215, 109, N'
8.
10', 10, 47, 48, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (82, 215, 110, N'
8.
8', 9, 69, 56, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (83, 216, 111, N'
9.
9', 6, 75, 5, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (84, 216, 112, N'
9.
7', 5, 4, 61, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (85, 216, 113, N'
9.
6', 6, 39, 40, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (86, 216, 114, N'
9.
5', 4, 76, 39, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (87, 216, 115, N'
9.
4', 4, 50, 52, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (88, 216, 116, N'
9.
2', 4, 26, 6, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (89, 216, 117, N'
9.
2', 7, 5, 41, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (90, 216, 118, N'
9.
1', 6, 46, 49, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (91, 216, 119, N'
9.
0', 4, 8, 9, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (92, 216, 120, N'
8.
13', 5, 44, 8, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (93, 216, 121, N'
8.
9', 4, 38, 45, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (94, 216, 122, N'
8.
4', 6, 69, 47, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (95, 217, 11, N'
11.
12', 6, 10, 11, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (96, 217, 12, N'
11.
8', 6, 11, 12, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (97, 217, 13, N'
11.
8', 7, 12, 13, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (98, 217, 14, N'
11.
3', 6, 13, 14, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (99, 217, 15, N'
11.
2', 5, 14, 15, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (100, 217, 16, N'
10.
13', 8, 15, 16, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (101, 217, 17, N'
10.
10', 8, 16, 17, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (102, 217, 18, N'
10.
4', 12, 17, 18, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (103, 217, 19, N'
10.
2', 10, 18, 19, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (104, 217, 20, N'
10.
0', 7, 19, 20, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (105, 218, 123, N'
11.
12', 6, 77, 62, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (106, 218, 124, N'
11.
11', 7, 16, 51, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (107, 218, 125, N'
11.
9', 10, 78, 63, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (108, 218, 126, N'
11.
5', 10, 79, 12, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (109, 218, 127, N'
11.
5', 6, 80, 64, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (110, 218, 128, N'
11.
0', 6, 14, 15, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (111, 218, 129, N'
10.
9', 7, 81, 18, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (112, 218, 130, N'
10.
6', 9, 82, 65, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (113, 218, 131, N'
10.
5', 6, 83, 19, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (114, 219, 132, N'
11.
8', 7, 84, 66, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (115, 219, 133, N'
11.
5', 7, 85, 67, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (116, 219, 134, N'
11.
1', 10, 86, 68, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (117, 219, 135, N'
10.
13', 8, 77, 69, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (118, 219, 136, N'
10.
11', 7, 84, 70, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (119, 220, 137, N'
11.
12', 6, 87, 71, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (120, 220, 138, N'
11.
6', 7, 88, 72, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (121, 220, 139, N'
11.
5', 9, 86, 68, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (122, 220, 140, N'
11.
5', 10, 36, 73, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (123, 220, 141, N'
10.
0', 8, 89, 74, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (124, 221, 142, N'
11.
7', 6, 14, 15, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (125, 221, 143, N'
11.
0', 6, 90, 75, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (126, 221, 144, N'
11.
0', 6, 91, 76, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (127, 221, 145, N'
11.
0', 5, 92, 68, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (128, 221, 146, N'
10.
12', 4, 93, 77, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (129, 221, 147, N'
10.
12', 4, 78, 78, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (130, 221, 148, N'
10.
12', 4, 94, 79, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (131, 221, 149, N'
10.
7', 6, 95, 80, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (132, 222, 150, N'
12.
0', 8, 92, 68, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (133, 222, 151, N'
11.
12', 8, 96, 81, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (134, 222, 152, N'
11.
11', 7, 97, 82, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (135, 222, 153, N'
11.
10', 7, 11, 12, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (136, 222, 154, N'
11.
9', 7, 84, 66, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (137, 222, 155, N'
10.
9', 8, 98, 83, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (138, 222, 156, N'
10.
8', 6, 80, 84, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (139, 222, 157, N'
10.
0', 7, 15, 80, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (140, 223, 158, N'
11.
4', 7, 77, 69, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (141, 223, 159, N'
11.
4', 5, 99, 85, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (142, 223, 160, N'
11.
4', 5, 100, 86, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (143, 223, 161, N'
11.
4', 6, 101, 78, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (144, 223, 162, N'
11.
4', 6, 77, 62, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (145, 223, 163, N'
11.
4', 5, 86, 68, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (146, 223, 164, N'
11.
4', 5, 102, 87, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (147, 223, 165, N'
11.
4', 6, 103, 88, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (148, 223, 166, N'
11.
4', 5, 17, 79, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (149, 223, 167, N'
11.
4', 6, 81, 89, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (150, 223, 168, N'
11.
4', 6, 98, 83, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (151, 223, 169, N'
11.
2', 4, 104, 65, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (152, 223, 170, N'
11.
2', 4, 105, 90, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (153, 223, 171, N'
10.
11', 5, 104, 91, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (154, 224, 21, N'
9.
9', 3, 20, 21, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (155, 224, 22, N'
9.
9', 8, 21, 22, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (156, 224, 23, N'
9.
7', 4, 22, 23, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (157, 224, 24, N'
9.
4', 4, 23, 24, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (158, 224, 25, N'
9.
3', 10, 24, 25, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (159, 224, 26, N'
9.
2', 5, 25, 26, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (160, 225, 172, N'
9.
5', 2, 23, 92, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (161, 225, 173, N'
9.
5', 2, 106, 51, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (162, 225, 174, N'
9.
5', 2, 107, 25, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (163, 225, 175, N'
9.
0', 2, 108, 93, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (164, 225, 176, N'
9.
0', 2, 109, 94, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (165, 225, 177, N'
9.
0', 2, 110, 22, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (166, 225, 178, N'
9.
0', 2, 111, 95, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (167, 225, 179, N'
9.
0', 2, 1, 96, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (168, 225, 180, N'
9.
0', 2, 112, 97, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (169, 226, 181, N'
9.
10', 4, 113, 98, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (170, 226, 182, N'
9.
8', 3, 114, 95, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (171, 226, 183, N'
9.
7', 3, 115, 99, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (172, 226, 184, N'
9.
7', 3, 26, 100, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (173, 226, 185, N'
9.
6', 5, 24, 101, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (174, 226, 186, N'
8.
12', 5, 24, 102, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (175, 227, 187, N'
9.
12', 4, 116, 103, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (176, 227, 188, N'
9.
12', 5, 113, 104, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (177, 227, 189, N'
9.
9', 7, 117, 105, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (178, 227, 190, N'
9.
8', 3, 24, 95, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (179, 227, 191, N'
9.
5', 3, 118, 22, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (180, 227, 192, N'
9.
5', 3, 119, 106, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (181, 228, 193, N'
9.
7', 3, 114, 107, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (182, 228, 194, N'
9.
5', 3, 120, 96, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (183, 228, 195, N'
9.
2', 3, 26, 23, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (184, 228, 196, N'
9.
2', 3, 24, 25, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (185, 228, 197, N'
9.
1', 3, 121, 108, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (186, 228, 198, N'
8.
13', 3, 112, 21, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (187, 229, 199, N'
10.
0', 3, 113, 109, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (188, 229, 200, N'
9.
11', 4, 114, 110, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (189, 229, 201, N'
9.
6', 3, 122, 111, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (190, 229, 202, N'
9.
6', 3, 111, 112, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (191, 229, 203, N'
9.
5', 4, 64, 113, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (192, 229, 204, N'
9.
5', 3, 120, 96, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (193, 229, 205, N'
9.
4', 4, 24, 101, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (194, 229, 206, N'
9.
2', 3, 115, 114, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (195, 230, 207, N'
9.
8', 5, 25, 26, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (196, 230, 208, N'
9.
7', 4, 123, 113, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (197, 230, 209, N'
9.
6', 4, 124, 95, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (198, 230, 210, N'
9.
2', 9, 125, 115, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (199, 230, 211, N'
8.
12', 7, 126, 116, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (200, 230, 212, N'
8.
5', 9, 24, 25, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (201, 230, 213, N'
8.
4', 3, 127, 117, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (202, 231, 27, N'
9.
9', 2, 26, 27, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (203, 231, 28, N'
9.
9', 2, 14, 28, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (204, 231, 29, N'
9.
9', 2, 27, 29, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (205, 231, 30, N'
9.
7', 2, 28, 30, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (206, 231, 31, N'
9.
5', 2, 24, 31, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (207, 231, 32, N'
9.
4', 2, 29, 32, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (208, 231, 33, N'
9.
4', 2, 30, 33, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (209, 231, 34, N'
9.
3', 2, 31, 34, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (210, 231, 35, N'
9.
2', 2, 32, 35, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (211, 231, 36, N'
9.
2', 2, 10, 36, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (212, 231, 37, N'
8.
11', 2, 33, 37, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (213, 231, 38, N'
8.
1', 2, 34, 38, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (214, 232, 214, N'
9.
7', 2, 124, 118, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (215, 232, 215, N'
9.
6', 2, 128, 32, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (216, 232, 216, N'
9.
5', 2, 129, 34, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (217, 232, 217, N'
9.
4', 2, 130, 36, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (218, 232, 218, N'
9.
3', 2, 131, 119, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (219, 232, 219, N'
9.
1', 2, 132, 37, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (220, 232, 220, N'
9.
0', 2, 118, 28, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (221, 232, 221, N'
8.
11', 2, 121, 120, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (222, 232, 222, N'
8.
8', 2, 125, 121, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (223, 232, 223, N'
8.
6', 2, 34, 38, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (224, 233, 224, N'
9.
7', 2, 131, 119, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (225, 233, 225, N'
9.
7', 2, 38, 122, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (226, 233, 226, N'
9.
6', 2, 133, 123, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (227, 233, 227, N'
9.
4', 2, 35, 31, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (228, 233, 228, N'
9.
3', 2, 34, 38, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (229, 233, 229, N'
9.
2', 2, 134, 124, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (230, 233, 230, N'
9.
1', 2, 115, 28, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (231, 233, 231, N'
8.
10', 2, 135, 30, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (232, 233, 232, N'
8.
7', 2, 136, 125, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (233, 233, 233, N'
8.
7', 2, 126, 121, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (234, 234, 234, N'
9.
0', 2, 125, 37, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (235, 234, 235, N'
9.
0', 2, 126, 121, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (236, 234, 236, N'
9.
0', 2, 111, 126, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (237, 234, 237, N'
9.
0', 2, 137, 127, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (238, 234, 238, N'
9.
0', 2, 138, 124, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (239, 234, 239, N'
9.
0', 2, 139, 128, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (240, 234, 240, N'
9.
0', 2, 140, 28, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (241, 234, 241, N'
9.
0', 2, 131, 119, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (242, 234, 242, N'
9.
0', 2, 141, 129, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (243, 234, 243, N'
9.
0', 2, 112, 36, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (244, 234, 244, N'
9.
0', 2, 142, 32, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (245, 234, 245, N'
9.
0', 2, 24, 31, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (246, 235, 246, N'
9.
0', 2, 143, 37, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (247, 235, 247, N'
9.
0', 2, 144, 29, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (248, 235, 248, N'
9.
0', 2, 145, 35, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (249, 235, 249, N'
9.
0', 2, 146, 31, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (250, 235, 250, N'
9.
0', 2, 28, 30, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (251, 235, 251, N'
9.
0', 2, 147, 32, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (252, 235, 252, N'
9.
0', 2, 64, 28, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (253, 235, 253, N'
9.
0', 2, 118, 130, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (254, 235, 254, N'
9.
0', 2, 148, 36, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (255, 235, 255, N'
9.
0', 2, 149, 128, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (256, 235, 256, N'
9.
0', 2, 20, 131, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (257, 235, 257, N'
9.
0', 2, 28, 124, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (258, 235, 258, N'
9.
0', 2, 29, 132, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (259, 235, 259, N'
9.
0', 2, 133, 123, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (260, 236, 260, N'
9.
12', 4, 122, 30, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (261, 236, 261, N'
9.
6', 3, 111, 133, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (262, 236, 262, N'
9.
5', 3, 76, 35, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (263, 236, 263, N'
9.
2', 3, 64, 29, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (264, 236, 264, N'
8.
12', 3, 121, 28, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (265, 236, 265, N'
8.
6', 3, 144, 36, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (266, 237, 266, N'
10.
0', 5, 150, 33, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (267, 237, 267, N'
9.
13', 7, 125, 37, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (268, 237, 268, N'
9.
11', 4, 115, 133, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (269, 237, 269, N'
9.
10', 6, 151, 31, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (270, 237, 270, N'
9.
8', 5, 24, 128, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (271, 237, 271, N'
9.
8', 4, 115, 119, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (272, 237, 272, N'
9.
8', 3, 138, 134, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (273, 237, 273, N'
9.
7', 4, 112, 36, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (274, 237, 274, N'
9.
6', 3, 14, 28, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (275, 237, 275, N'
9.
6', 3, 152, 121, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (276, 237, 276, N'
9.
6', 3, 153, 29, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (277, 237, 277, N'
9.
1', 3, 128, 124, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (278, 237, 278, N'
8.
12', 3, 64, 32, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (279, 238, 279, N'
9.
9', 3, 140, 32, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (280, 238, 280, N'
9.
9', 3, 154, 119, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (281, 238, 281, N'
9.
7', 3, 27, 36, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (282, 238, 282, N'
9.
6', 3, 143, 121, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (283, 238, 283, N'
9.
6', 3, 155, 134, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (284, 238, 284, N'
9.
4', 3, 156, 28, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (285, 238, 285, N'
9.
4', 3, 46, 35, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (286, 238, 286, N'
9.
4', 3, 157, 37, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (287, 238, 287, N'
9.
1', 3, 50, 135, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (288, 238, 288, N'
9.
0', 3, 30, 33, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (289, 238, 289, N'
9.
0', 3, 135, 30, NULL, 0, 0, NULL, NULL)
GO
INSERT [dbo].[tb_race_horse] ([race_horse_id], [race_id], [horse_id], [weight], [age], [trainer_id], [jockey_id], [jockey_weight], [finished], [position], [description], [rp_notes]) VALUES (290, 238, 290, N'
8.
12', 3, 158, 136, NULL, 0, 0, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[tb_race_horse] OFF
GO
SET IDENTITY_INSERT [dbo].[tb_trainer] ON 
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (1, N'
GeorgeScott


', N'/profile/trainer/31461/george-scott')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (2, N'
TonyCoyle


', N'/profile/trainer/25536/tony-coyle')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (3, N'
TimEasterby


', N'/profile/trainer/10152/tim-easterby')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (4, N'
DeclanCarroll


', N'/profile/trainer/3297/declan-carroll')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (5, N'
MichaelDods


', N'/profile/trainer/5079/michael-dods')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (6, N'
EdwardBethell


', N'/profile/trainer/38073/edward-bethell')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (7, N'
OlliePears


', N'/profile/trainer/19836/ollie-pears')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (8, N'
JohnQuinn


', N'/profile/trainer/8873/john-quinn')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (9, N'
BryanSmart


', N'/profile/trainer/373/bryan-smart')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (10, N'
AlanKing


', N'/profile/trainer/13928/alan-king')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (11, N'
NigelHawke


', N'/profile/trainer/10257/nigel-hawke')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (12, N'
MartinKeighley


', N'/profile/trainer/15642/martin-keighley')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (13, N'
OllyMurphy


', N'/profile/trainer/33553/olly-murphy')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (14, N'
IanWilliams


', N'/profile/trainer/12465/ian-williams')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (15, N'
SimonEarle


', N'/profile/trainer/9623/simon-earle')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (16, N'
AnthonyHoneyball


', N'/profile/trainer/18639/anthony-honeyball')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (17, N'
RyanPotter


', N'/profile/trainer/26798/ryan-potter')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (18, N'
MichaelScudamore


', N'/profile/trainer/20450/michael-scudamore')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (19, N'
JenniferMason


', N'/profile/trainer/24701/jennifer-mason')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (20, N'
SaeedbinSuroor


', N'/profile/trainer/9546/saeed-bin-suroor')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (21, N'
GeorgeBaker


', N'/profile/trainer/20005/george-baker')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (22, N'
MickQuinn


', N'/profile/trainer/3339/mick-quinn')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (23, N'
JohnGallagher


', N'/profile/trainer/10308/john-gallagher')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (24, N'
TonyCarroll


', N'/profile/trainer/9714/tony-carroll')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (25, N'
ChristopherMason


', N'/profile/trainer/25265/christopher-mason')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (26, N'
MarkJohnston


', N'/profile/trainer/3378/mark-johnston')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (27, N'
MickChannon


', N'/profile/trainer/4676/mick-channon')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (28, N'
RalphBeckett


', N'/profile/trainer/13917/ralph-beckett')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (29, N'
CharlesHills


', N'/profile/trainer/25628/charles-hills')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (30, N'
MarkUsher


', N'/profile/trainer/13/mark-usher')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (31, N'
JosephParr


', N'/profile/trainer/36992/joseph-parr')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (32, N'
DominicFfrenchDavis


', N'/profile/trainer/8683/dominic-ffrench-davis')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (33, N'
JSMoore


', N'/profile/trainer/5121/j-s-moore')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (34, N'
RichardFahey


', N'/profile/trainer/8010/richard-fahey')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (35, N'
DavidO''Meara


', N'/profile/trainer/22839/david-omeara')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (36, N'
AmyMurphy


', N'/profile/trainer/32325/amy-murphy')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (37, N'
PaulMidgley


', N'/profile/trainer/15948/paul-midgley')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (38, N'
NigelTinkler


', N'/profile/trainer/865/nigel-tinkler')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (39, N'
LawrenceMullaney


', N'/profile/trainer/11013/lawrence-mullaney')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (40, N'
TJKent


', N'/profile/trainer/37539/t-j-kent')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (41, N'
KRBurke


', N'/profile/trainer/5019/k-r-burke')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (42, N'
JeddO''Keeffe


', N'/profile/trainer/14335/jedd-okeeffe')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (43, N'
TimFitzgerald


', N'/profile/trainer/15673/tim-fitzgerald')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (44, N'
JulieCamacho


', N'/profile/trainer/12225/julie-camacho')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (45, N'
JohnWainwright


', N'/profile/trainer/486/john-wainwright')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (46, N'
Michael&DavidEasterby


', N'/profile/trainer/38543/michael-david-easterby')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (47, N'
RogerFell


', N'/profile/trainer/32385/roger-fell')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (48, N'
LesEyre


', N'/profile/trainer/29218/les-eyre')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (49, N'
LindaStubbs


', N'/profile/trainer/5755/linda-stubbs')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (50, N'
MarkLoughnane


', N'/profile/trainer/15429/mark-loughnane')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (51, N'
PhillipMakin


', N'/profile/trainer/35791/phillip-makin')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (52, N'
DavidEvans


', N'/profile/trainer/3846/david-evans')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (53, N'
AnnDuffield


', N'/profile/trainer/5372/ann-duffield')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (54, N'
ScottDixon


', N'/profile/trainer/26099/scott-dixon')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (55, N'
MichaelAppleby


', N'/profile/trainer/10363/michael-appleby')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (56, N'
IainJardine


', N'/profile/trainer/21516/iain-jardine')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (57, N'
MichaelHerrington


', N'/profile/trainer/22150/michael-herrington')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (58, N'
StephHollinshead


', N'/profile/trainer/28762/steph-hollinshead')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (59, N'
FrankBishop


', N'/profile/trainer/34707/frank-bishop')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (60, N'
AlanBerry


', N'/profile/trainer/13946/alan-berry')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (61, N'
StellaBarclay


', N'/profile/trainer/34717/stella-barclay')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (62, N'
WilliamHaggas


', N'/profile/trainer/3415/william-haggas')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (63, N'
AdrianNicholls


', N'/profile/trainer/33638/adrian-nicholls')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (64, N'
RogerCharlton


', N'/profile/trainer/4635/roger-charlton')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (65, N'
DavidLoughnane


', N'/profile/trainer/31698/david-loughnane')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (66, N'
HughieMorrison


', N'/profile/trainer/10704/hughie-morrison')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (67, N'
BrianEllison


', N'/profile/trainer/4431/brian-ellison')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (68, N'
DavidBarron


', N'/profile/trainer/542/david-barron')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (69, N'
RuthCarr


', N'/profile/trainer/20045/ruth-carr')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (70, N'
ChrisFairhurst


', N'/profile/trainer/8141/chris-fairhurst')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (71, N'
GeoffreyHarker


', N'/profile/trainer/11484/geoffrey-harker')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (72, N'
MarkWalford


', N'/profile/trainer/28876/mark-walford')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (73, N'
MickyHammond


', N'/profile/trainer/5020/micky-hammond')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (74, N'
DannyBrooke


', N'/profile/trainer/37724/danny-brooke')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (75, N'
BenHaslam


', N'/profile/trainer/22367/ben-haslam')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (76, N'
MichaelBell


', N'/profile/trainer/4113/michael-bell')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (77, N'
DonaldMcCain


', N'/profile/trainer/15674/donald-mccain')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (78, N'
NigelTwiston-Davies


', N'/profile/trainer/306/nigel-twiston-davies')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (79, N'
MaxYoung


', N'/profile/trainer/34679/max-young')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (80, N'
SamEngland


', N'/profile/trainer/31667/sam-england')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (81, N'
JonjoO''Neill


', N'/profile/trainer/397/jonjo-oneill')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (82, N'
SueGardner


', N'/profile/trainer/6173/sue-gardner')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (83, N'
AlastairRalph


', N'/profile/trainer/26259/alastair-ralph')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (84, N'
EmmaLavelle


', N'/profile/trainer/13116/emma-lavelle')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (85, N'
BenCase


', N'/profile/trainer/13888/ben-case')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (86, N'
PhilipHobbs


', N'/profile/trainer/135/philip-hobbs')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (87, N'
JamieSnowden


', N'/profile/trainer/20814/jamie-snowden')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (88, N'
FergalO''Brien


', N'/profile/trainer/13986/fergal-obrien')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (89, N'
DavidWeston


', N'/profile/trainer/29232/david-weston')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (90, N'
GraemeMcPherson


', N'/profile/trainer/18127/graeme-mcpherson')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (91, N'
ThomasGallagher


', N'/profile/trainer/32061/thomas-gallagher')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (92, N'
RobertStephens


', N'/profile/trainer/28265/robert-stephens')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (93, N'
MarkWeatherer


', N'/profile/trainer/32763/mark-weatherer')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (94, N'
AdrianWintle


', N'/profile/trainer/6845/adrian-wintle')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (95, N'
RichardHawker


', N'/profile/trainer/10030/richard-hawker')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (96, N'
WarrenGreatrex


', N'/profile/trainer/22011/warren-greatrex')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (97, N'
DavidPipe


', N'/profile/trainer/10157/david-pipe')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (98, N'
JohnnyFarrelly


', N'/profile/trainer/28343/johnny-farrelly')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (99, N'
RichardPrice


', N'/profile/trainer/268/richard-price')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (100, N'
ChristianWilliams


', N'/profile/trainer/33186/christian-williams')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (101, N'
DrRichardNewland


', N'/profile/trainer/17063/dr-richard-newland')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (102, N'
BillTurner


', N'/profile/trainer/739/bill-turner')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (103, N'
GaryBrown


', N'/profile/trainer/7722/gary-brown')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (104, N'
SheilaLewis


', N'/profile/trainer/26474/sheila-lewis')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (105, N'
SamuelDrinkwater


', N'/profile/trainer/32733/samuel-drinkwater')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (106, N'
BrettJohnson


', N'/profile/trainer/12600/brett-johnson')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (107, N'
IsmailMohammed


', N'/profile/trainer/16112/ismail-mohammed')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (108, N'
SirMarkPrescottBt


', N'/profile/trainer/67/sir-mark-prescott-bt')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (109, N'
HarryEustace


', N'/profile/trainer/38304/harry-eustace')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (110, N'
JimBoyle


', N'/profile/trainer/15108/jim-boyle')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (111, N'
MarcoBotti


', N'/profile/trainer/9003/marco-botti')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (112, N'
AndrewBalding


', N'/profile/trainer/15605/andrew-balding')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (113, N'
GaryMoore


', N'/profile/trainer/7833/gary-moore')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (114, N'
Simon&EdCrisford


', N'/profile/trainer/37478/simon-ed-crisford')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (115, N'
RichardHannon


', N'/profile/trainer/28787/richard-hannon')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (116, N'
JohnBerry


', N'/profile/trainer/9440/john-berry')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (117, N'
ConradAllen


', N'/profile/trainer/517/conrad-allen')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (118, N'
RichardHughes


', N'/profile/trainer/31188/richard-hughes')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (119, N'
JimmyFox


', N'/profile/trainer/900/jimmy-fox')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (120, N'
JamieOsborne


', N'/profile/trainer/13955/jamie-osborne')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (121, N'
EveJohnsonHoughton


', N'/profile/trainer/18920/eve-johnson-houghton')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (122, N'
RaeGuest


', N'/profile/trainer/3609/rae-guest')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (123, N'
RonaldHarris


', N'/profile/trainer/14931/ronald-harris')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (124, N'
JRJenkins


', N'/profile/trainer/35/j-r-jenkins')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (125, N'
JohnButler


', N'/profile/trainer/26177/john-butler')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (126, N'
MichaelAttwater


', N'/profile/trainer/16406/michael-attwater')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (127, N'
JohnRyan


', N'/profile/trainer/17862/john-ryan')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (128, N'
TomWard


', N'/profile/trainer/36341/tom-ward')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (129, N'
ChrisDwyer


', N'/profile/trainer/5063/chris-dwyer')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (130, N'
PatMurphy


', N'/profile/trainer/7168/pat-murphy')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (131, N'
ArchieWatson


', N'/profile/trainer/32276/archie-watson')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (132, N'
SeamusDurack


', N'/profile/trainer/24051/seamus-durack')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (133, N'
DarryllHolland


', N'/profile/trainer/38211/darryll-holland')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (134, N'
StuartWilliams


', N'/profile/trainer/8543/stuart-williams')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (135, N'
JonathanPortman


', N'/profile/trainer/12601/jonathan-portman')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (136, N'
RobertEddery


', N'/profile/trainer/25052/robert-eddery')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (137, N'
RogerIngram


', N'/profile/trainer/5321/roger-ingram')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (138, N'
Daniel&ClaireKubler


', N'/profile/trainer/37637/daniel-claire-kubler')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (139, N'
Paul&OliverCole


', N'/profile/trainer/37482/paul-oliver-cole')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (140, N'
DavidMenuisier


', N'/profile/trainer/29411/david-menuisier')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (141, N'
WilliamMuir&ChrisGrassick


', N'/profile/trainer/38350/william-muir-chris-grassick')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (142, N'
VictorDartnall


', N'/profile/trainer/9071/victor-dartnall')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (143, N'
SimonDow


', N'/profile/trainer/870/simon-dow')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (144, N'
KevinPhilippartDeFoy


', N'/profile/trainer/37949/kevin-philippart-de-foy')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (145, N'
SirMarkTodd


', N'/profile/trainer/15617/sir-mark-todd')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (146, N'
EdDunlop


', N'/profile/trainer/9036/ed-dunlop')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (147, N'
GeorgeBoughey


', N'/profile/trainer/36211/george-boughey')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (148, N'
HenryCandy


', N'/profile/trainer/336/henry-candy')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (149, N'
John&ThadyGosden


', N'/profile/trainer/38291/john-thady-gosden')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (150, N'
GayKelleway


', N'/profile/trainer/6964/gay-kelleway')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (151, N'
MichaelMadgwick


', N'/profile/trainer/435/michael-madgwick')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (152, N'
HarryDunlop


', N'/profile/trainer/18734/harry-dunlop')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (153, N'
PeterCharalambous


', N'/profile/trainer/22609/peter-charalambous')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (154, N'
WilliamKnight


', N'/profile/trainer/17966/william-knight')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (155, N'
RogerTeal


', N'/profile/trainer/19203/roger-teal')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (156, N'
EddeGiles


', N'/profile/trainer/7300/ed-de-giles')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (157, N'
AmandaPerrett


', N'/profile/trainer/10663/amanda-perrett')
GO
INSERT [dbo].[tb_trainer] ([trainer_id], [trainer_name], [trainer_url]) VALUES (158, N'
MattCrawley


', N'/profile/trainer/38707/matt-crawley')
GO
SET IDENTITY_INSERT [dbo].[tb_trainer] OFF
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
