namespace HospitalManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("HMS.MENU", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("HMS.MENU", "Order");
        }
    }
}
