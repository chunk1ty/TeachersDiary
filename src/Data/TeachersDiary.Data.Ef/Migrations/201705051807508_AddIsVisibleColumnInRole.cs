namespace TeachersDiary.Data.Ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsVisibleColumnInRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "IsVisible", c => c.Boolean(false, false));
            // Discriminator is comming from EF because of multiple inheritance
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropColumn("dbo.AspNetRoles", "IsVisible");
        }
    }
}
