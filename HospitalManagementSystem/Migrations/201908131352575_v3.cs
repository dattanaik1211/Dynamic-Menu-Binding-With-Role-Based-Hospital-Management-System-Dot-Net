namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("HMS.MENUROLEMAP", "MainMenu_Id", "HMS.MENU");
            DropIndex("HMS.MENUROLEMAP", new[] { "MainMenu_Id" });
            AddColumn("HMS.MENU", "MainMenu_Id", c => c.Guid());
            CreateIndex("HMS.MENU", "MainMenu_Id");
            AddForeignKey("HMS.MENU", "MainMenu_Id", "HMS.MENU", "Id");
            DropColumn("HMS.MENUROLEMAP", "MainMenu_Id");
        }
        
        public override void Down()
        {
            AddColumn("HMS.MENUROLEMAP", "MainMenu_Id", c => c.Guid());
            DropForeignKey("HMS.MENU", "MainMenu_Id", "HMS.MENU");
            DropIndex("HMS.MENU", new[] { "MainMenu_Id" });
            DropColumn("HMS.MENU", "MainMenu_Id");
            CreateIndex("HMS.MENUROLEMAP", "MainMenu_Id");
            AddForeignKey("HMS.MENUROLEMAP", "MainMenu_Id", "HMS.MENU", "Id");
        }
    }
}
