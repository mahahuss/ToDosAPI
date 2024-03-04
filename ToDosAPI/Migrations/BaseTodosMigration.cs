using FluentMigrator;

namespace ToDosAPI.Migrations;

public class BaseTodosMigration(int year, int month, int day, int hour, int minute, string author)
    : MigrationAttribute(CalculateValue(1, year, month, day, hour, minute))
{
    public string Author { get; private set; } = author;

    private static long CalculateValue(int branchNumber, int year, int month, int day, int hour, int minute)
    {
        return branchNumber * 1000000000000L + year * 100000000L + month * 1000000L + day * 10000L + hour * 100L + minute;
    }
}