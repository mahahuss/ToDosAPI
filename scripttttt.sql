USE [ToDoDb]
GO
/****** Object:  StoredProcedure [dbo].[sp_RolesGet]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TasksAttachmentsAddFiles]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TasksAttachmentsGet]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TasksCreate]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TasksDelete]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TasksEdit]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TasksGetAll]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TasksGetById]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_TasksGetCount]    Script Date: 12/28/2023 10:37:18 AM ******/
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
where CreatedBy = @userId or [SharedTasks].SharedTo = @userId)

select count(Id) as tasksCount from TasksForCount 


END
GO
/****** Object:  StoredProcedure [dbo].[sp_TasksGetUserTasks]    Script Date: 12/28/2023 10:37:18 AM ******/
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
            where CreatedBy = @userId or [SharedTasks].SharedTo = @userId

            order by Tasks.Id desc
            OFFSET @fromIndex ROWS FETCH NEXT @toIndex ROWS ONLY
        )

    select PaginatedTasks.*, [SharedTasks].[isEditable], users.FullName as SharedBy , TasksAttachments.*
    From PaginatedTasks
        left join TasksAttachments on TasksAttachments.TaskId = PaginatedTasks.Id
        left join [SharedTasks] on [SharedTasks].TaskId = PaginatedTasks.Id
        left join users on [SharedTasks].sharedby = users.id

END
GO
/****** Object:  StoredProcedure [dbo].[sp_TasksShare]    Script Date: 12/28/2023 10:37:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[sp_TasksShare]
(@userToShare int, @userId int, @isEditable bit, @taskId int )
AS
BEGIN

insert into SharedTasks (TaskId, SharedBy, SharedTo, IsEditable, SharedDate)
Values (@taskId , @userId ,@userToShare, @isEditable, getdate()) 

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UserChangeStatus]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserCreate]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserCredential]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserEdit]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserGet]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserGetAll]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UserGetUserWithRoles]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UsersEditRoles]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UsersRolesDelete]    Script Date: 12/28/2023 10:37:18 AM ******/
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
/****** Object:  StoredProcedure [dbo].[sp_UsersRolesGet]    Script Date: 12/28/2023 10:37:18 AM ******/
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
