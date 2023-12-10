USE [master]
GO
/****** Object:  Database [ToDoDb]    Script Date: 12/10/2023 3:27:38 PM ******/
CREATE DATABASE [ToDoDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ToDoDb', FILENAME = N'E:\DATAs\ToDoDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ToDoDb_log', FILENAME = N'E:\DATAs\ToDoDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ToDoDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ToDoDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ToDoDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ToDoDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ToDoDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ToDoDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [ToDoDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ToDoDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ToDoDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ToDoDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ToDoDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ToDoDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ToDoDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ToDoDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ToDoDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ToDoDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ToDoDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ToDoDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ToDoDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ToDoDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ToDoDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ToDoDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ToDoDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ToDoDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ToDoDb] SET  MULTI_USER 
GO
ALTER DATABASE [ToDoDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ToDoDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ToDoDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ToDoDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ToDoDb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ToDoDb', N'ON'
GO
ALTER DATABASE [ToDoDb] SET QUERY_STORE = OFF
GO
USE [ToDoDb]
GO
/****** Object:  User [tododb_user]    Script Date: 12/10/2023 3:27:38 PM ******/
CREATE USER [tododb_user] FOR LOGIN [tododb_user] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [tododb_user]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/10/2023 3:27:38 PM ******/
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
/****** Object:  Table [dbo].[Tasks]    Script Date: 12/10/2023 3:27:38 PM ******/
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
/****** Object:  Table [dbo].[TasksAttachs]    Script Date: 12/10/2023 3:27:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TasksAttachs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[FileName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_TasksAttachs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/10/2023 3:27:38 PM ******/
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
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 12/10/2023 3:27:38 PM ******/
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

INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (5, N'try to add from swaggerrrrfiyf', CAST(N'2023-11-16T09:03:23.153' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (6, N'try to add from swaggerrrrrrro', CAST(N'2023-11-16T09:30:44.413' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (1007, N'123', CAST(N'2023-11-30T15:25:44.070' AS DateTime), 1, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (1008, N'123', CAST(N'2023-11-30T15:25:45.547' AS DateTime), 1, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (1009, N'123', CAST(N'2023-11-30T15:25:48.770' AS DateTime), 1, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2002, N'dgsdgdghhh', CAST(N'2023-12-04T13:13:18.027' AS DateTime), 7, 1)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2003, N'gjfjf', CAST(N'2023-12-04T13:14:05.153' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2004, N'gjgj
hfhh', CAST(N'2023-12-04T13:21:59.563' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2005, N'hxfhfx
dgdzgdz
dzgdz', CAST(N'2023-12-04T13:27:13.080' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2006, N'fefeagesges
egsgs
segsghs
gfegwgh', CAST(N'2023-12-04T16:16:38.223' AS DateTime), 7, 0)
INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (2007, N'a new task
hello', CAST(N'2023-12-04T16:34:47.057' AS DateTime), 7, 0)
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[TasksAttachs] ON 

INSERT [dbo].[TasksAttachs] ([Id], [TaskId], [FileName]) VALUES (3, 5, N'maha.png')
INSERT [dbo].[TasksAttachs] ([Id], [TaskId], [FileName]) VALUES (4, 5, N'm.pdf')
SET IDENTITY_INSERT [dbo].[TasksAttachs] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (1, N'Maha', N'Maha@123', N'MahaMaha', N'Maha Hussain')
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (2, N'maha@maha', N'2EFC5A937D47E4BAF39B8688C444FE788528D45F61F5598699441033D5C335992806D299AD303AE472C08654A5F4027A826C3CA6EB8D6351F00343999540BBD1', N'gMBUew1wrV9bWjjqHtUBuW+nbP5x6JcJ17eyphgp5Y0ld6uq9LoY6fr88Nx+32QpElOM/zyk1vEuibTdz9mX9w==', N'Maha Hussain')
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (3, N'su', N'df', N'dsdg', N'gsg')
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (4, N'string99', N'5BE3C97D50062FBDF2856F0CF0B345E062136348EF776B0B0AC1B8CFEB8CCC2D283A65E1125456A2E0C95D97B83CB70BA4AD5DC7A8FBDEF22F1A5F785D12401B', N'D/np27fDR8HJWF0Qz0IxtSlFw+VVLVeebmtlJP61Rd20GBLfR2zeTz6L/F8URNUSchBOnHbifLdrwGVgoR/VLg==', N'string')
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (5, N'string999', N'A4FE1936C118926AA07D3234966E89E57023B30A9B6ACA7F76F45CB660DA181343A4FA7EC4BDA4E5C82ECB19F9365FDCF35E3FF1B9B89D7E1B7A4052DA130958', N'Vku1RhPkegH0cnQ53eK9WSbDV+0WPmGFWTJe4wk5EVDY1Kn87ndwYZf92T/K8G5m4QW8fbYe4I0yZp8vYGGaBw==', N'string')
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (6, N'string9991', N'8044425ECE6CA59990CF59FBD0C159643AD4FDD1F3042A7D5B2033C9D150315FC42BC8FAAAAE1CFC2176685CA10DB5131C63CDD534BB323AC11F45D617B70981', N'cf+/iq4yqo0fz6yhGcrGwppO49nS9Tg65IZH+sIdvDeuQAHbJBypEZ/Tikkn7h2cPcaMXAChwxUuATzAxFwoCQ==', N'string')
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (7, N'string2121', N'73384B027F7680C1E51E89A0B8A123FA5A87D0A46C7F5D6A75D0D25A2C36D1F8F2BA8B23B90CAD6CCF8CC1F6E040BCFDD5361AA770DF42389B52817FE9798446', N'3O6A8s6P64jl5jookgnL3qNixcaCUUhf2VhA8ml4Wvn7fnOVgd2zULAvnHm11lrepGuuiTVwNcGlHAgThptt+Q==', N'mahagh1')
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (8, N'mahamaha', N'386C14D5DC82E3B3C7F7BA40E4B13839B526D432EC4D36CDF10D9948B0D260B8585E80B90DD30A84BEF50FFA219F9D1A01C1CF57C93B84B7C35AFD0939159107', N'yh0stCpI4Lfjyr0hiwH7zrpiwA9EnT5gZPATOFhMi9WnPZJCcyet3Yq4EELkJJGzl5bYgnkdajr1FjLt1uMnqg==', N'string')
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (10, N'maha1@maha', N'ajama', N'wdwafdwaf', N'wfawfwafaf')
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (12, N'maha@maha1', N'EB592DFCE66265EAE5FB0CDA2EF266E42AAD8CED0AEE33C0CE5BB0432A77B504224B6AB9718159085FBA0CDA4BCD6A8DB7A08A79B566FA5AB7D054848B2DC542', N'vuol0Yq9AtV5yO5CN5nkmaOzh9WlFY5z1nFWSYfLBSD8DramKXgjYV6lDUnO40PBOuWs+v5njIw2Z7NoCVYtIg==', N'string')
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (13, N'string', N'91066B83D51C0EEF7947A4D29BE1C23A52569D01DF5FAA4ADCC31426D0BD1711E6D3657F6BF4DC65F7A5DD58CFFD0D18087DBF6AC48BB9B1313EB9AEDD30B09A', N'Vq6B5lRzZOiWM7ggMMuRgXL0HwvUFwZzkfqRybPbd+L4auGko+6r3+sVHd0jbLv369gcWxP738RrvfF8x4G/Gg==', N'string')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UsersRoles] ON 

INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (1, 2, 1)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (4, 2, 2)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (5, 1, 2)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (6, 1, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (7, 6, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (8, 7, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (9, 7, 2)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (10, 8, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (11, NULL, 1)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (12, 10, 1)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (13, NULL, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (14, 12, 3)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (15, 13, 3)
SET IDENTITY_INSERT [dbo].[UsersRoles] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_1]    Script Date: 12/10/2023 3:27:38 PM ******/
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [IX_Users_1] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TasksAttachs]  WITH CHECK ADD  CONSTRAINT [FK_TasksAttachs_Tasks] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[TasksAttachs] CHECK CONSTRAINT [FK_TasksAttachs_Tasks]
GO
/****** Object:  StoredProcedure [dbo].[sp_TaskCreate]    Script Date: 12/10/2023 3:27:38 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TaskDelete]    Script Date: 12/10/2023 3:27:38 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TaskEdit]    Script Date: 12/10/2023 3:27:38 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TaskGetAll]    Script Date: 12/10/2023 3:27:38 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TaskGetUserTasks]    Script Date: 12/10/2023 3:27:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TaskGetUserTasks]
(@userId int)
AS
BEGIN

Select Tasks.*, TasksAttachs.FileName From Tasks 
left join TasksAttachs on TasksAttachs.TaskId = Tasks.Id
where CreatedBy = @userId 

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TasksAttachsAddFiles]    Script Date: 12/10/2023 3:27:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_TasksAttachsAddFiles]
(@Id int, @FileName nvarchar(200))
AS
BEGIN

insert into TasksAttachs (TaskId, [FileName])
Values (@Id, @FileName) 

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserCreate]    Script Date: 12/10/2023 3:27:38 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserCredential]    Script Date: 12/10/2023 3:27:38 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserEdit]    Script Date: 12/10/2023 3:27:38 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserGet]    Script Date: 12/10/2023 3:27:38 PM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserGetUserWithRoles]    Script Date: 12/10/2023 3:27:38 PM ******/
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
USE [master]
GO
ALTER DATABASE [ToDoDb] SET  READ_WRITE 
GO
