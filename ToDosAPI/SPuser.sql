USE [ToDoDB]
GO
/****** Object:  StoredProcedure [dbo].[sp_UserCredential]    Script Date: 11/16/2023 1:20:37 PM ******/
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
