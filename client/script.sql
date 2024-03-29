USE [master]
GO
/****** Object:  Database [ToDoDB]    Script Date: 2/6/2024 2:19:42 PM ******/
CREATE DATABASE [ToDoDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ToDoDB', FILENAME = N'E:\DATAs\ToDoDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ToDoDB_log', FILENAME = N'E:\DATAs\ToDoDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ToDoDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ToDoDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ToDoDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ToDoDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ToDoDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ToDoDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ToDoDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ToDoDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ToDoDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ToDoDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ToDoDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ToDoDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ToDoDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ToDoDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ToDoDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ToDoDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ToDoDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ToDoDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ToDoDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ToDoDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ToDoDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ToDoDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ToDoDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ToDoDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ToDoDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ToDoDB] SET  MULTI_USER 
GO
ALTER DATABASE [ToDoDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ToDoDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ToDoDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ToDoDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ToDoDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ToDoDB', N'ON'
GO
ALTER DATABASE [ToDoDB] SET QUERY_STORE = OFF
GO
USE [ToDoDB]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  Table [dbo].[SharedTasks]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SharedTasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[SharedBy] [int] NOT NULL,
	[SharedWith] [int] NOT NULL,
	[IsEditable] [bit] NOT NULL,
	[SharedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SharedTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskContent] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TasksAttachments]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TasksAttachments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NULL,
	[FileName] [nvarchar](200) NULL,
 CONSTRAINT [PK_TasksAttachs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](500) NULL,
	[Salt] [nvarchar](500) NULL,
	[FullName] [nvarchar](50) NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 2/6/2024 2:19:42 PM ******/
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
SET IDENTITY_INSERT [dbo].[SharedTasks] ON 

INSERT [dbo].[SharedTasks] ([Id], [TaskId], [SharedBy], [SharedWith], [IsEditable], [SharedDate]) VALUES (1, 39, 2, 7, 1, CAST(N'2024-01-06T18:22:45.590' AS DateTime))
INSERT [dbo].[SharedTasks] ([Id], [TaskId], [SharedBy], [SharedWith], [IsEditable], [SharedDate]) VALUES (3, 42, 7, 4, 0, CAST(N'2024-02-06T08:40:45.010' AS DateTime))
INSERT [dbo].[SharedTasks] ([Id], [TaskId], [SharedBy], [SharedWith], [IsEditable], [SharedDate]) VALUES (4, 41, 7, 4, 1, CAST(N'2024-02-06T08:40:57.130' AS DateTime))
INSERT [dbo].[SharedTasks] ([Id], [TaskId], [SharedBy], [SharedWith], [IsEditable], [SharedDate]) VALUES (5, 43, 7, 2, 0, CAST(N'2024-02-06T11:08:31.850' AS DateTime))
SET IDENTITY_INSERT [dbo].[SharedTasks] OFF
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 

INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (37, N'hhhh
hhh
hhhhh
', CAST(N'2023-12-03T15:14:14.560' AS DateTime), 7, 1)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (38, N'test', CAST(N'2023-12-03T15:30:49.417' AS DateTime), 7, 1)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (39, N'testing againn', CAST(N'2023-12-03T15:31:07.483' AS DateTime), 2, 1)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (40, N'heekkoooo', CAST(N'2024-01-12T23:41:55.837' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (41, N'hellloooo', CAST(N'2024-01-12T23:42:02.140' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (42, N'hekooooo', CAST(N'2024-01-12T23:42:16.903' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (43, N'hellooooo', CAST(N'2024-01-12T23:42:52.190' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (44, N'try', CAST(N'2024-02-06T11:45:07.893' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (45, N'trrryy', CAST(N'2024-02-06T11:45:21.810' AS DateTime), 7, 0)
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[TasksAttachments] ON 

INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (1, 37, N'maha.pdf')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (2, 38, N'm.png')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (3, 38, N'mahaa.pdf')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (4, 43, N'Carrier_Portal_request_form (6)ca90a1f148c742efa80c47f80ad78f03.pdf')
INSERT [dbo].[TasksAttachments] ([Id], [TaskId], [FileName]) VALUES (5, 45, N'Carrier_Portal_request_form (6)ca90a1f148c742efa80c47f80ad78f03f72a6a3951524eb18a9cb18060ec5aa9.pdf')
SET IDENTITY_INSERT [dbo].[TasksAttachments] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (1, N'Maha', N'73384B027F7680C1E51E89A0B8A123FA5A87D0A46C7F5D6A75D0D25A2C36D1F8F2BA8B23B90CAD6CCF8CC1F6E040BCFDD5361AA770DF42389B52817FE9798446', N'3O6A8s6P64jl5jookgnL3qNixcaCUUhf2VhA8ml4Wvn7fnOVgd2zULAvnHm11lrepGuuiTVwNcGlHAgThptt+Q==', N'Maha', 1)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (2, N'maha@maha', N'73384B027F7680C1E51E89A0B8A123FA5A87D0A46C7F5D6A75D0D25A2C36D1F8F2BA8B23B90CAD6CCF8CC1F6E040BCFDD5361AA770DF42389B52817FE9798446', N'3O6A8s6P64jl5jookgnL3qNixcaCUUhf2VhA8ml4Wvn7fnOVgd2zULAvnHm11lrepGuuiTVwNcGlHAgThptt+Q==', N'mahaGh', 0)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (3, N'su', N'73384B027F7680C1E51E89A0B8A123FA5A87D0A46C7F5D6A75D0D25A2C36D1F8F2BA8B23B90CAD6CCF8CC1F6E040BCFDD5361AA770DF42389B52817FE9798446', N'3O6A8s6P64jl5jookgnL3qNixcaCUUhf2VhA8ml4Wvn7fnOVgd2zULAvnHm11lrepGuuiTVwNcGlHAgThptt+Q==', N'gsg', 1)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (4, N'string99', N'73384B027F7680C1E51E89A0B8A123FA5A87D0A46C7F5D6A75D0D25A2C36D1F8F2BA8B23B90CAD6CCF8CC1F6E040BCFDD5361AA770DF42389B52817FE9798446', N'3O6A8s6P64jl5jookgnL3qNixcaCUUhf2VhA8ml4Wvn7fnOVgd2zULAvnHm11lrepGuuiTVwNcGlHAgThptt+Q==', N'string99', 1)
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName], [Status]) VALUES (7, N'string2121', N'73384B027F7680C1E51E89A0B8A123FA5A87D0A46C7F5D6A75D0D25A2C36D1F8F2BA8B23B90CAD6CCF8CC1F6E040BCFDD5361AA770DF42389B52817FE9798446', N'3O6A8s6P64jl5jookgnL3qNixcaCUUhf2VhA8ml4Wvn7fnOVgd2zULAvnHm11lrepGuuiTVwNcGlHAgThptt+Q==', N'string2121', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UsersRoles] ON 

INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (1, 2, 1)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (4, 2, 2)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (5, 1, 2)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (6, 1, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (7, 6, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (10, 8, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (11, NULL, 1)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (12, 10, 1)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (13, NULL, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (14, 12, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (15, 7, 1)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (16, 7, 2)
SET IDENTITY_INSERT [dbo].[UsersRoles] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_1]    Script Date: 2/6/2024 2:19:42 PM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [IX_Users_1] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_RolesGet]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[sp_RolesGet]

AS
BEGIN

select * from Roles

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TaskCreate]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TaskDelete]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TaskEdit]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TaskEdit]
(@taskContent nvarchar (200), @status int, @Id int )
AS
BEGIN

Update Tasks set TaskContent = @taskContent, Status = @status where Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_TaskGetAll]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TaskGetUserTasks]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TasksAttachmentsAddFiles]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TasksAttachmentsGet]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TasksCreate]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TasksCreate]
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
/****** Object:  StoredProcedure [dbo].[sp_TasksDelete]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TasksDelete]
(@taskId int)
AS
BEGIN

delete from Tasks where Id = @taskId

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TasksEdit]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TasksEdit]
    (@taskContent nvarchar (200),
    @status int,
    @Id int
)
AS
BEGIN

    Update Tasks set TaskContent = @taskContent, Status = @status where Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TasksGetAll]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TasksGetAll]
AS
BEGIN

Select * From Tasks

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TasksGetById]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TasksGetCount]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TasksGetCount]
(@userId int)
AS
BEGIN


with TasksForCount as ( Select distinct Tasks.Id From Tasks 
left join TasksAttachments on TasksAttachments.TaskId = Tasks.Id
full outer join [SharedTasks] on [SharedTasks].TaskId = Tasks.Id
left join users on [SharedTasks].sharedby = users.id
where CreatedBy = @userId or [SharedTasks].SharedWith = @userId)

select count(Id) as tasksCount from TasksForCount 


END
GO
/****** Object:  StoredProcedure [dbo].[sp_TasksGetUserTasks]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TasksGetUserTasks]
    (@userId int,
    @fromIndex int ,
    @toIndex int
)
AS
BEGIN

    with
        PaginatedTasks
        as
        (
            Select Tasks.*
            from Tasks
                left join [SharedTasks] on [SharedTasks].TaskId = Tasks.Id
            where CreatedBy = @userId or [SharedTasks].SharedWith = @userId

            order by Tasks.Id desc
            OFFSET @fromIndex ROWS FETCH NEXT @toIndex ROWS ONLY
        )

    select PaginatedTasks.*, case when CreatedBy = 7 then null else [SharedTasks].[isEditable] end as isEditable, users.FullName as SharedBy , TasksAttachments.*
    From PaginatedTasks
        left join TasksAttachments on TasksAttachments.TaskId = PaginatedTasks.Id
        left join [SharedTasks] on [SharedTasks].TaskId = PaginatedTasks.Id
        left join users on [SharedTasks].sharedby = users.id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TasksShare]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TasksShare]
(@userToShare int, @userId int, @isEditable bit, @taskId int )
AS
BEGIN

insert into SharedTasks (TaskId, SharedBy, SharedWith, IsEditable, SharedDate)
Values (@taskId , @userId ,@userToShare, @isEditable, getdate()) 

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserChangeStatus]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserCreate]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserCredential]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserEdit]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UserEdit]
(@name nvarchar(50), @Id int )
AS
BEGIN

Update Users set FullName = @name where Id = @id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserGet]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserGetAll]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UserGetAll]
( @userId int )
AS
BEGIN

 -- select Users.Id,Username,FullName,Users.[Status], count(Tasks.Id) as TotalTasks ,UsersRoles.RoleId as Id ,[Roles].UserType  from Users
  -- left join tasks on tasks.CreatedBy = users.Id 
  -- join UsersRoles on UsersRoles.UserId= users.Id
   --join Roles on Roles.Id= UsersRoles.RoleId
   
  -- group by  Users.Id,Username,FullName,Users.[Status], UsersRoles.UserId ,UsersRoles.RoleId ,[Roles].UserType
   --order by 1 asc


   select Users.Id,Username,FullName,Users.[Status], count(Tasks.Id) as TotalTasks  from Users
   left join tasks on tasks.CreatedBy = users.Id where Users.Id != @userId
   
   group by  Users.Id,Username,FullName,Users.[Status]
   order by 1 asc


END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserGetUserWithRoles]    Script Date: 2/6/2024 2:19:42 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UsersEditRoles]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_UsersEditRoles]
    ( @Id int, @Fullname nvarchar(50), @Role int)
AS
BEGIN
update Users set FullName=@Fullname where Id=@Id

insert into UsersRoles values (@Id, @Role)

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UsersGetShareWith]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UsersGetShareWith]
( @UserId int, @TaskId int )
AS
BEGIN

    select Users.Id, Users.Username, Users.FullName  from Users 
	where Users.Id not in (select SharedTasks.SharedWith from SharedTasks where TaskId=@TaskId )
	and Users.Id != @UserId
	
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UsersRolesDelete]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_UsersRolesDelete]
    ( @Id int)
AS
BEGIN

    delete from UsersRoles where UserId = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UsersRolesGet]    Script Date: 2/6/2024 2:19:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UsersRolesGet]
(@userId int)
AS
BEGIN

select UsersRoles.RoleId as Id, Roles.UserType
  from Roles join UsersRoles on UsersRoles.RoleId = Roles.Id
  where UsersRoles.UserId=@userId
END
GO
USE [master]
GO
ALTER DATABASE [ToDoDB] SET  READ_WRITE 
GO
