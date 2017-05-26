namespace TeachersDiary.Data.Ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeacherRole : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Roles values (NEWID(), 'Teacher')");
        }
        
        public override void Down()
        {
            Sql(@" delete from Roles where name = 'Teacher'");
        }
    }
}
