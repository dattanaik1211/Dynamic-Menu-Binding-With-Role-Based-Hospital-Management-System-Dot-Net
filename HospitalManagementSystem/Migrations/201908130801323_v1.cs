namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "HMS.MENUROLEMAP",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                        MainMenu_Id = c.Guid(),
                        Menu_Id = c.Guid(),
                        Role_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("HMS.MENU", t => t.MainMenu_Id)
                .ForeignKey("HMS.MENU", t => t.Menu_Id)
                .ForeignKey("HMS.ROLE", t => t.Role_Id)
                .Index(t => t.MainMenu_Id)
                .Index(t => t.Menu_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "HMS.MENU",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MenuName = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "HMS.ROLE",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoleName = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        LastModifiedOn = c.DateTime(nullable: false),
                        LastModifiedBy = c.Guid(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("HMS.MENUROLEMAP", "Role_Id", "HMS.ROLE");
            DropForeignKey("HMS.MENUROLEMAP", "Menu_Id", "HMS.MENU");
            DropForeignKey("HMS.MENUROLEMAP", "MainMenu_Id", "HMS.MENU");
            DropIndex("HMS.MENUROLEMAP", new[] { "Role_Id" });
            DropIndex("HMS.MENUROLEMAP", new[] { "Menu_Id" });
            DropIndex("HMS.MENUROLEMAP", new[] { "MainMenu_Id" });
            DropTable("HMS.ROLE");
            DropTable("HMS.MENU");
            DropTable("HMS.MENUROLEMAP");
        }
    }
}
