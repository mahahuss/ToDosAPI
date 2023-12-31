USE [ToDoDb]
GO
/****** Object:  StoredProcedure [dbo].[sp_RolesGet]    Script Date: 12/27/2023 6:49:11 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserGet]    Script Date: 12/27/2023 6:49:11 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserGetAll]    Script Date: 12/27/2023 6:49:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_UserGetAll]
AS
BEGIN

 -- select Users.Id,Username,FullName,Users.[Status], count(Tasks.Id) as TotalTasks ,UsersRoles.RoleId as Id ,[Roles].UserType  from Users
  -- left join tasks on tasks.CreatedBy = users.Id 
  -- join UsersRoles on UsersRoles.UserId= users.Id
   --join Roles on Roles.Id= UsersRoles.RoleId
   
  -- group by  Users.Id,Username,FullName,Users.[Status], UsersRoles.UserId ,UsersRoles.RoleId ,[Roles].UserType
   --order by 1 asc


      select Users.Id,Username,FullName,Users.[Status], count(Tasks.Id) as TotalTasks  from Users
   left join tasks on tasks.CreatedBy = users.Id 
   
   group by  Users.Id,Username,FullName,Users.[Status]
   order by 1 asc

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UsersEditRoles]    Script Date: 12/27/2023 6:49:11 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UsersRolesDelete]    Script Date: 12/27/2023 6:49:11 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UsersRolesGet]    Script Date: 12/27/2023 6:49:11 AM ******/
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
