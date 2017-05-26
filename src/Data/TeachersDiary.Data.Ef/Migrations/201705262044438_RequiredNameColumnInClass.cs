using System.Data.Entity.Migrations;

namespace TeachersDiary.Data.Ef.Migrations
{
    public partial class RequiredNameColumnInClass : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Classes", "Name", c => c.String(nullable: false, maxLength: 3));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Classes", "Name", c => c.String(maxLength: 3));
        }
    }
}
