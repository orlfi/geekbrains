using FluentMigrator;
using FluentMigrator.Infrastructure;

namespace MetricsManager.Migrations
{
    [Migration(2)]
    public class SecondMigration : Migration
    {
        public override void Up()
        {
            Create.Index("Idx_Agents_AgentUrl_Unique")
                .OnTable("Agents")
                .OnColumn("AgentUrl").Ascending()
                .WithOptions().Unique();
        }

        public override void Down()
        {
        }
    }
}