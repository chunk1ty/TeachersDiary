namespace TeachersDiary.Data.Ef.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationRoles : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Roles values (NEWID(), 'Student')");
            Sql("insert into Roles values (NEWID(), 'SchoolAdministrator')");
            Sql("insert into Roles values (NEWID(), 'Administrator')");
        }

        public override void Down()
        {
            Sql(@" delete from Roles where name = 'Student'");
            Sql(@" delete from Roles where name = 'SchoolAdministrator'");
            Sql(@" delete from Roles where name = 'Administrator'");
        }
    }
}
