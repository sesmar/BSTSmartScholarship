namespace BSTSmartScholarship.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applicants",
                c => new
                    {
                        StudentNumber = c.String(nullable: false, maxLength: 10),
                        FirstName = c.String(nullable: false, maxLength: 128),
                        LastName = c.String(nullable: false, maxLength: 128),
                        PhoneNumber = c.String(nullable: false, maxLength: 10),
                        EmailAddress = c.String(nullable: false, maxLength: 128),
                        Gender = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        CumulativeGPA = c.Double(nullable: false),
                        CreditHours = c.Int(nullable: false),
                        IsEligible = c.Boolean(),
                    })
                .PrimaryKey(t => t.StudentNumber);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Applicants");
        }
    }
}
