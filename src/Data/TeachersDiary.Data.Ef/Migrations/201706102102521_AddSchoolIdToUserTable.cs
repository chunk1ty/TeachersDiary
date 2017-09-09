namespace TeachersDiary.Data.Ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSchoolIdToUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "SchoolId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "SchoolId");
        }
    }
}
