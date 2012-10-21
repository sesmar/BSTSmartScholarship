namespace BSTSmartScholarship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatesForApplicant : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applicants", "PhoneNumber", c => c.String(nullable: false, maxLength: 14));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applicants", "PhoneNumber", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
