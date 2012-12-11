namespace BSTSmartScholarship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AwardedAmount_To_Award : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Awards", "AwardAmount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Awards", "AwardAmount");
        }
    }
}
