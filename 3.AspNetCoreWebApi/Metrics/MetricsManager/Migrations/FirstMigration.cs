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
                .WithColumn("IsEnabled").AsInt32();

            Create.Index("Idx_CpuMetrics_Time_Unique")
                .OnTable("CpuMetrics")
                .OnColumn("AgentId").Ascending()
                .OnColumn("Time").Ascending()
                .WithOptions().Unique();

            Create.Index("Idx_DotNetMetrics_Time_Unique")
                .OnTable("DotNetMetrics")
                .OnColumn("AgentId").Ascending()
                .OnColumn("Time").Ascending()
                .WithOptions().Unique();

            Create.Index("Idx_HddMetrics_Time_Unique")
                .OnTable("HddMetrics")
                .OnColumn("AgentId").Ascending()
                .OnColumn("Time").Ascending()
                .WithOptions().Unique();

            Create.Index("Idx_RamMetrics_Time_Unique")
                .OnTable("RamMetrics")
                .OnColumn("AgentId").Ascending()
                .OnColumn("Time").Ascending()
                .WithOptions().Unique();

            Create.Index("Idx_NetworkMetrics_Time_Unique")
                .OnTable("NetworkMetrics")
                .OnColumn("AgentId").Ascending()
                .OnColumn("Time").Ascending()
                .WithOptions().Unique();
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