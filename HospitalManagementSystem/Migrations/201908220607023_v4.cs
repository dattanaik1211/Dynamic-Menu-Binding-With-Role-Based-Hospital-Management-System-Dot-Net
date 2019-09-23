namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "HMS.Employees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MobileNo = c.String(),
                        EmailId = c.String(),
                        Password = c.String(),
                        DateOfJoining = c.DateTime(nullable: false),
                        Salary = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        Role_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("HMS.ROLE", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("HMS.Employees", "Role_Id", "HMS.ROLE");
            DropIndex("HMS.Employees", new[] { "Role_Id" });
            DropTable("HMS.Employees");
        }
    }
}
