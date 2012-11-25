namespace BSTSmartScholarship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Votes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Votes", "UserId", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Votes", "UserId", c => c.Int(nullable: false));
        }
    }
}
