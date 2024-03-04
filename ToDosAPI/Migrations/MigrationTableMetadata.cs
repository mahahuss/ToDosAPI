using FluentMigrator.Runner.VersionTableInfo;

namespace ToDosAPI.Migrations;

[VersionTableMetaData]
public class MigrationTableMetadata : DefaultVersionTableMetaData
{
    public override string TableName => "DbMigrations";
}