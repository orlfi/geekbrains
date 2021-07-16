// using FluentMigrator;
// using FluentMigrator.Infrastructure;

// namespace MetricsManager.Migrations
// {
//     [Migration(2)]
//     public class SecondMigration : Migration
//     {
//         public override void Up()
//         {
//             Alter.Table("Agents").AlterColumn("Enabled").
                        
//             // .Table("Agents")
//             //     .WithColumn("AgentId").AsInt64().PrimaryKey().Identity()
//             //     .WithColumn("AgentUrl").AsString()
//             //     .WithColumn("Enabled").AsInt32();
            

//             // Create.Index("Idx_NetworkMetrics_Time_Unique")
//             //     .OnTable("NetworkMetrics")
//             //     .OnColumn("AgentId").Ascending()
//             //     .OnColumn("Time").Ascending()
//             //     .WithOptions().Unique();
//         }

//         public override void Down()
//         {
//         }
//     }
// }