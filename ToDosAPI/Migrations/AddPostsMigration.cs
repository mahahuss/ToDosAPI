using FluentMigrator;

namespace ToDosAPI.Migrations;

[BaseTodosMigration(2024, 3, 4, 2, 37, "Maha")]
public class AddPostsMigration : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                    create table Posts (
                        Id int primary key,
                        Title nvarchar(255),
                        Body nvarchar(MAX)
                    )
                    """);
    }

    public override void Down()
    {
    }
}