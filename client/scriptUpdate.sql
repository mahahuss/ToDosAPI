USE [ToDoDb]
GO
/****** Object:  StoredProcedure [dbo].[sp_TasksAttachmentsGet]    Script Date: 12/17/2023 2:57:16 PM ******/
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
