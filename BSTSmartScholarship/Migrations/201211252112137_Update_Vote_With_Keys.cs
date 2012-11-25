namespace BSTSmartScholarship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Vote_With_Keys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        StudentNumber = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => new { t.UserId, t.StudentNumber })
                .ForeignKey("dbo.Applicants", t => t.StudentNumber, cascadeDelete: true)
                .Index(t => t.StudentNumber);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Votes", new[] { "StudentNumber" });
            DropForeignKey("dbo.Votes", "StudentNumber", "dbo.Applicants");
            DropTable("dbo.Votes");
        }
    }
}
