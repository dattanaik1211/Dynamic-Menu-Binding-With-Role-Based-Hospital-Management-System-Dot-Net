namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("HMS.MENU", "IsParent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("HMS.MENU", "IsParent");
        }
    }
}
