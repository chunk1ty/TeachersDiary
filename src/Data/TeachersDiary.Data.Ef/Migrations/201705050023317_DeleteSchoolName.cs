namespace TeachersDiary.Data.Ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteSchoolName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Schools", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schools", "Name", c => c.String());
        }
    }
}
