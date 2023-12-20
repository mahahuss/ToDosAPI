USE [ToDoDb]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/20/2023 3:27:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserType] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 12/20/2023 3:27:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskContent] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TasksAttachments]    Script Date: 12/20/2023 3:27:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TasksAttachments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[FileName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TasksAttachs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/20/2023 3:27:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](500) NOT NULL,
	[Salt] [nvarchar](500) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 12/20/2023 3:27:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[RoleId] [int] NULL,
 CONSTRAINT [PK_UsersRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [UserType]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [UserType]) VALUES (2, N'Moderator')
INSERT [dbo].[Roles] ([Id], [UserType]) VALUES (3, N'Member')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 

INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (1007, N'task 1', CAST(N'2023-11-30T15:25:44.070' AS DateTime), 1, 1)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (1008, N'test from Db', CAST(N'2023-11-30T15:25:45.547' AS DateTime), 1, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (1009, N'1234657577 ', CAST(N'2023-11-30T15:25:48.770' AS DateTime), 1, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2025, N'hello testtt', CAST(N'2023-12-17T13:46:41.040' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2029, N'fhfjjgjg', CAST(N'2023-12-17T14:03:20.240' AS DateTime), 7, 1)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2036, N'mgh,kjh,.jh', CAST(N'2023-12-17T14:20:58.003' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2037, N'hellloooo', CAST(N'2023-12-17T16:56:50.477' AS DateTime), 7, 1)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2038, N'hello with attachment', CAST(N'2023-12-17T16:57:22.257' AS DateTime), 7, 1)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2039, N'hellooo', CAST(N'2023-12-19T12:56:02.663' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2046, N'maha gh', CAST(N'2023-12-19T13:08:08.950' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2047, N'helo', CAST(N'2023-12-20T17:14:13.587' AS DateTime), 7, 0)
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[TasksAttachments] ON 

INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (21, 2035, N'مثال قالب الإشعارات - إدارة علاقات الشركاء92da70032eec49f89418d5dbd27d0d96.pdf')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (22, 2036, N'2023-03 (9)9c871566a0354c1f9b952cda8c082773.png')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (23, 2036, N'ارقام التواصلcbb507a34df8423580e2913a57923865.pdf')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (24, 2038, N'IntgraionGuide-Naba2023.01.2984d957384c8149b3a0a544b274d929cf.pdf')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (25, 2038, N'2023-04 (2)86c3d569e94842a69885503391d3a018.png')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (29, 2044, N'2023-08SVInvoiced1dd54b4efb74b9a9d45e301b3977261.pdf')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (30, 2045, N'2023-04 (2)86c3d569e94842a69885503391d3a018 (1)e767fae1917b44be8b8142d45cd3a030.png')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (31, 2046, N'2023-09 (12)40cb33cd4f8d4dfe9578ca1b28bb0a37.png')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (32, 2046, N'Carrier_Portal_request_form (13)b105711dcdba4cf6ac11ebc4834b00a3.pdf')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (33, 2047, N'Carrier_Portal_request_form (13)b105711dcdba4cf6ac11ebc4834b00a3672add523ade474684716b6c2396aa45.pdf')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (34, 2047, N'2023-09 (12)40cb33cd4f8d4dfe9578ca1b28bb0a37eb4cc85b03cd44c296ec74ccf29fbef8.png')
SET IDENTITY_INSERT [dbo].[TasksAttachments] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (1, N'Maha', N'Maha@123', N'MahaMaha', N'Maha Hussain', 1)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (2, N'maha@maha', N'2EFC5A937D47E4BAF39B8688C444FE788528D45F61F5598699441033D5C335992806D299AD303AE472C08654A5F4027A826C3CA6EB8D6351F00343999540BBD1', N'gMBUew1wrV9bWjjqHtUBuW+nbP5x6JcJ17eyphgp5Y0ld6uq9LoY6fr88Nx+32QpElOM/zyk1vEuibTdz9mX9w==', N'Maha Hussain', 0)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (3, N'su', N'df', N'dsdg', N'gsg', 0)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (4, N'string99', N'5BE3C97D50062FBDF2856F0CF0B345E062136348EF776B0B0AC1B8CFEB8CCC2D283A65E1125456A2E0C95D97B83CB70BA4AD5DC7A8FBDEF22F1A5F785D12401B', N'D/np27fDR8HJWF0Qz0IxtSlFw+VVLVeebmtlJP61Rd20GBLfR2zeTz6L/F8URNUSchBOnHbifLdrwGVgoR/VLg==', N'string', 0)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (5, N'string999', N'A4FE1936C118926AA07D3234966E89E57023B30A9B6ACA7F76F45CB660DA181343A4FA7EC4BDA4E5C82ECB19F9365FDCF35E3FF1B9B89D7E1B7A4052DA130958', N'Vku1RhPkegH0cnQ53eK9WSbDV+0WPmGFWTJe4wk5EVDY1Kn87ndwYZf92T/K8G5m4QW8fbYe4I0yZp8vYGGaBw==', N'string', 1)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (6, N'string9991', N'8044425ECE6CA59990CF59FBD0C159643AD4FDD1F3042A7D5B2033C9D150315FC42BC8FAAAAE1CFC2176685CA10DB5131C63CDD534BB323AC11F45D617B70981', N'cf+/iq4yqo0fz6yhGcrGwppO49nS9Tg65IZH+sIdvDeuQAHbJBypEZ/Tikkn7h2cPcaMXAChwxUuATzAxFwoCQ==', N'string', 1)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (7, N'string2121', N'73384B027F7680C1E51E89A0B8A123FA5A87D0A46C7F5D6A75D0D25A2C36D1F8F2BA8B23B90CAD6CCF8CC1F6E040BCFDD5361AA770DF42389B52817FE9798446', N'3O6A8s6P64jl5jookgnL3qNixcaCUUhf2VhA8ml4Wvn7fnOVgd2zULAvnHm11lrepGuuiTVwNcGlHAgThptt+Q==', N'maha', 1)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (8, N'mahamaha', N'386C14D5DC82E3B3C7F7BA40E4B13839B526D432EC4D36CDF10D9948B0D260B8585E80B90DD30A84BEF50FFA219F9D1A01C1CF57C93B84B7C35AFD0939159107', N'yh0stCpI4Lfjyr0hiwH7zrpiwA9EnT5gZPATOFhMi9WnPZJCcyet3Yq4EELkJJGzl5bYgnkdajr1FjLt1uMnqg==', N'string', 1)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (10, N'maha1@maha', N'ajama', N'wdwafdwaf', N'wfawfwafaf', 1)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (12, N'maha@maha1', N'EB592DFCE66265EAE5FB0CDA2EF266E42AAD8CED0AEE33C0CE5BB0432A77B504224B6AB9718159085FBA0CDA4BCD6A8DB7A08A79B566FA5AB7D054848B2DC542', N'vuol0Yq9AtV5yO5CN5nkmaOzh9WlFY5z1nFWSYfLBSD8DramKXgjYV6lDUnO40PBOuWs+v5njIw2Z7NoCVYtIg==', N'string', 1)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (13, N'string', N'91066B83D51C0EEF7947A4D29BE1C23A52569D01DF5FAA4ADCC31426D0BD1711E6D3657F6BF4DC65F7A5DD58CFFD0D18087DBF6AC48BB9B1313EB9AEDD30B09A', N'Vq6B5lRzZOiWM7ggMMuRgXL0HwvUFwZzkfqRybPbd+L4auGko+6r3+sVHd0jbLv369gcWxP738RrvfF8x4G/Gg==', N'string', 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UsersRoles] ON 

INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (1, 2, 1)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (4, 2, 2)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (5, 1, 2)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (6, 1, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (7, 6, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (10, 8, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (12, 10, 1)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (14, 12, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (15, 13, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (1004, 7, 1)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (1005, 7, 2)
SET IDENTITY_INSERT [dbo].[UsersRoles] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_1]    Script Date: 12/20/2023 3:27:45 PM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [IX_Users_1] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_TaskCreate]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TaskCreate]
(@TaskContent nvarchar(200), @CreatedBy int, @Status int)
AS
BEGIN

insert into Tasks (TaskContent, CreatedDate, CreatedBy, Status)
Values (@TaskContent , GETDATE() ,@CreatedBy, @Status) 

declare @taskId int;
set @taskId = SCOPE_IDENTITY(); 

-------------------------------------
select * from Tasks where Id = @taskId
END
GO
/****** Object:  StoredProcedure [dbo].[sp_TaskDelete]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TaskDelete]
(@taskId int)
AS
BEGIN

delete from Tasks where Id = @taskId

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TaskEdit]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TaskEdit]
    (@taskContent nvarchar (200),
    @status int,
    @Id int
)
AS
BEGIN

    Update Tasks set TaskContent = @taskContent, Status = @status where Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TaskGetAll]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TaskGetAll]
AS
BEGIN

Select * From Tasks

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TaskGetUserTasks]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TaskGetUserTasks]
(@userId int)
AS
BEGIN

Select Tasks.*, TasksAttachments.* From Tasks 
left join TasksAttachments on TasksAttachments.TaskId = Tasks.Id
where CreatedBy = @userId 

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TasksAttachmentsAddFiles]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TasksAttachmentsAddFiles]
(@Id int, @FileName nvarchar(200))
AS
BEGIN

insert into TasksAttachments (TaskId, [FileName])
Values (@Id, @FileName) 

declare @fileId int;
set @fileId = SCOPE_IDENTITY(); 

-------------------------------------
select * from TasksAttachments where Id = @fileId

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TasksAttachmentsGet]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_TasksAttachmentsGet]
(@Id int)
AS
BEGIN

select * from TasksAttachments where Id = @Id

END

GO
/****** Object:  StoredProcedure [dbo].[sp_TasksGetById]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TasksGetById]
( @Id int)
AS
BEGIN
-------------------------------------
  select * from [Tasks] where id = @Id
  end
GO
/****** Object:  StoredProcedure [dbo].[sp_UserChangeStatus]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UserChangeStatus]
    (@status bit, @userId int)
AS
BEGIN

    Update Users set [Status] = @status where Id = @userId

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserCreate]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UserCreate] 
(@Username nvarchar (50), @Password nvarchar(500), @Salt nvarchar(500), @FullName nvarchar(50), @roleId int)
AS
BEGIN transaction
insert into Users (Username, Password, Salt, FullName)
Values (@Username, @Password, @Salt, @FullName)
declare @userId int;
set @userId = SCOPE_IDENTITY(); 

insert into UsersRoles values (@userId,@roleId)
-----------------------------------------
select * from Users where Id = @userId;


commit;
GO
/****** Object:  StoredProcedure [dbo].[sp_UserCredential]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_UserCredential]
(@username nvarchar(50))
AS
BEGIN

Select Password, Salt from Users where username = @username 

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserEdit]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_UserEdit]
    (@name nvarchar (50), @id int)
AS
BEGIN

    Update Users set FullName = @name where Id = @id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserGet]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UserGet]
(@Username nvarchar (50))
AS
BEGIN

select * from Users where Username = @username

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserGetAll]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UserGetAll]
AS
BEGIN

   select Users.Id,Username,FullName,Users.[Status], count(Tasks.Id) as TotalTasks from Users
   left join tasks on tasks.CreatedBy = users.Id group by  Users.Id,Username,FullName,Users.[Status]
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserGetUserWithRoles]    Script Date: 12/20/2023 3:27:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UserGetUserWithRoles]
(@Username nvarchar (50))
AS
BEGIN

select Users.* , Roles.* from Users 
join UsersRoles on  UsersRoles.userId = Users.Id 
join Roles on Roles.id = UsersRoles.roleId where Username= @Username

END
GO
