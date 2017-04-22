using Microsoft.AspNet.Identity.EntityFramework;

namespace TeacherDiary.Data.Ef
{
    public class TeacherDiaryDbContext : IdentityDbContext<AspNetUser>
    {
        public TeacherDiaryDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static TeacherDiaryDbContext Create()
        {
            return new TeacherDiaryDbContext();
        }
    }
}