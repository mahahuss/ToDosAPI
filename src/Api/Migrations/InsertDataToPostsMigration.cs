using FluentMigrator;

namespace Api.Migrations;

[BaseTodosMigration(2024, 3, 5, 10, 1, "Maha")]

public class InsertDataToPostsMigration : Migration
{

    public override void Up()
    {
        Execute.Sql("""
                    USE [ToDoDB]
                    GO
                  
                    SET ANSI_NULLS ON
                    GO
                    SET QUOTED_IDENTIFIER ON
                    GO
                    Create PROCEDURE [dbo].[sp_PostsCreate]
                    (@Id int, @Title nvarchar(255), @Body nvarchar(MAX))
                    AS
                    BEGIN

                    insert into Posts (Id, Title, Body, CreatedAt)
                    Values (@Id , @Title , @Body, getdate()) 

                    END
                    """);
    }
    public override void Down()
    {
    }


}

