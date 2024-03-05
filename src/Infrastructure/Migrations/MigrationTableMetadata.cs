using FluentMigrator.Runner.VersionTableInfo;

namespace Infrastructure.Migrations;

[VersionTableMetaData]
public class MigrationTableMetadata : DefaultVersionTableMetaData
{
    public override string TableName => "DbMigrations";
}