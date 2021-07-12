using FluentMigrator;
using FluentMigrator.Infrastructure;

namespace MetricsManager.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Up()
        {
            Create.Table("CpuMetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt64().ForeignKey("Agents", "AgentId")
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("DotNetMetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt64().ForeignKey("Agents", "AgentId")
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("RamMetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt64().ForeignKey("Agents", "AgentId")
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("NetworkMetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt64().ForeignKey("Agents", "AgentId")
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("HddMetrics")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentId").AsInt64().ForeignKey("Agents", "AgentId")
                .WithColumn("Value").AsInt32()
                .WithColumn("Time").AsInt64();

            Create.Table("Agents")
                .WithColumn("AgentId").AsInt64().PrimaryKey().Identity()
                .WithColumn("AgentUrl").AsString()
                .WithColumn("Enabled").AsInt32();
        }

        public override void Down()
        {
            Delete.Table("CpuMetrics");
            Delete.Table("DotNetMetrics");
            Delete.Table("RamMetrics");
            Delete.Table("NetworkMetrics");
            Delete.Table("HddMetrics");
            Delete.Table("Agents");
        }
    }
}