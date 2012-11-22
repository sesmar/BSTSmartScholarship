namespace BSTSmartScholarship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_IsVerified_For_Applicant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Applicants", "IsVerified", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Applicants", "IsVerified");
        }
    }
}
