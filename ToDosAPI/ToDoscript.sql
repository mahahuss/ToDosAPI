USE [master]
GO
/****** Object:  Database [ToDoDB]    Script Date: 11/15/2023 11:35:08 AM ******/
CREATE DATABASE [ToDoDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ToDoDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ToDoDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ToDoDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ToDoDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
ALTER DATABASE [ToDoDB] SET QUERY_STORE = OFF
GO
USE [ToDoDB]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 11/15/2023 11:35:08 AM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 11/15/2023 11:35:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](500) NULL,
	[Salt] [nvarchar](500) NULL,
	[FullName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 11/15/2023 11:35:08 AM ******/
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
/****** Object:  Table [dbo].[UserTypesLookup]    Script Date: 11/15/2023 11:35:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypesLookup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserType] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 

INSERT [dbo].[Tasks] ([Id], [TaskContent], [CreatedDate], [CreatedBy], [Status]) VALUES (1, N'try again', CAST(N'2023-10-30T07:54:35.693' AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (1, N'Maha', N'Maha@123', N'MahaMaha', N'Maha Hussain')
INSERT [dbo].[Users] ([Id], [Username], [Password], [Salt], [FullName]) VALUES (2, N'maha@maha', N'2EFC5A937D47E4BAF39B8688C444FE788528D45F61F5598699441033D5C335992806D299AD303AE472C08654A5F4027A826C3CA6EB8D6351F00343999540BBD1', N'gMBUew1wrV9bWjjqHtUBuW+nbP5x6JcJ17eyphgp5Y0ld6uq9LoY6fr88Nx+32QpElOM/zyk1vEuibTdz9mX9w==', N'Maha Hussain')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UsersRoles] ON 

INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (1, 2, 1)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (4, 2, 2)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (5, 1, 2)
INSERT [dbo].[UsersRoles] ([Id], [UserId], [RoleId]) VALUES (6, 1, 3)
SET IDENTITY_INSERT [dbo].[UsersRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[UserTypesLookup] ON 

INSERT [dbo].[UserTypesLookup] ([Id], [UserType]) VALUES (1, N'Admin')
INSERT [dbo].[UserTypesLookup] ([Id], [UserType]) VALUES (2, N'Moderator')
INSERT [dbo].[UserTypesLookup] ([Id], [UserType]) VALUES (3, N'Member')
SET IDENTITY_INSERT [dbo].[UserTypesLookup] OFF
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateTask]    Script Date: 11/15/2023 11:35:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CreateTask]
(@TaskContent nvarchar(200), @CreatedBy int, @Status int)
AS
BEGIN

insert into Tasks (TaskContent, CreatedDate, CreatedBy, Status)
Values (@TaskContent , GETDATE() ,@CreatedBy, @Status) 
-------------------------------------
select top 1 * from Tasks order by 1 desc

END
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateUser]    Script Date: 11/15/2023 11:35:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CreateUser] 
(@Username nvarchar (50), @Password nvarchar(500), @Salt nvarchar(500), @FullName nvarchar(50))
AS
BEGIN
insert into Users (Username, Password, Salt, FullName)
Values (@Username, @Password, @Salt, @FullName)
-----------------------------------------
select top 1 * from Users order by 1 desc
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteTask]    Script Date: 11/15/2023 11:35:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteTask]
(@taskId int)
AS
BEGIN

delete from Tasks where Id = @taskId

END
GO
/****** Object:  StoredProcedure [dbo].[sp_EditTask]    Script Date: 11/15/2023 11:35:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_EditTask]
(@taskContent nvarchar (200), @status int, @taskId int )
AS
BEGIN

Update Tasks set TaskContent = @taskContent, Status = @status where Id = @taskId

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllTasks]    Script Date: 11/15/2023 11:35:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllTasks]
AS
BEGIN

Select * From Tasks

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUser]    Script Date: 11/15/2023 11:35:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetUser]
(@Username nvarchar (50))
AS
BEGIN

select * from Users where Username = @username

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUsersWithRoles]    Script Date: 11/15/2023 11:35:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetUsersWithRoles]
(@Username nvarchar (50))
AS
BEGIN

select Users.* , UserTypesLookup.* from Users 
join UsersRoles on  UsersRoles.userId = Users.Id 
join UserTypesLookup on UserTypesLookup.id = UsersRoles.roleId where Username= @Username

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserTasks]    Script Date: 11/15/2023 11:35:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetUserTasks]
(@userId int)
AS
BEGIN

Select * From Tasks where CreatedBy = @userId 

END
GO
USE [master]
GO
ALTER DATABASE [ToDoDB] SET  READ_WRITE 
GO
