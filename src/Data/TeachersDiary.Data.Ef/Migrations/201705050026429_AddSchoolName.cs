namespace TeachersDiary.Data.Ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSchoolName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schools", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schools", "Name");
        }
    }
}
