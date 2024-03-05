using FluentMigrator;

namespace Infrastructure.Migrations;

[BaseTodosMigration(2024, 3, 4, 2, 43, "Maha")]
public class AddCreatedAtToPostsMigration : Migration
{
    public override void Up()
    {
        Execute.Sql("""
                    alter table Posts
                        ADD [CreatedAt] datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
                    """);
    }

    public override void Down()
    {
    }
}