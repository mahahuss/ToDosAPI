using FluentMigrator.Runner.VersionTableInfo;

namespace Api.Migrations;

[VersionTableMetaData]
public class MigrationTableMetadata : DefaultVersionTableMetaData
{
    public override string TableName => "DbMigrations";
}