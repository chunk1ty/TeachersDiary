namespace TeachersDiary.Data.Ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToStudentTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "UserId");
        }
    }
}
