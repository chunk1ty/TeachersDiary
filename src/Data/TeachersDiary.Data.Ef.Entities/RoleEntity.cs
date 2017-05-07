using Microsoft.AspNet.Identity.EntityFramework;

namespace TeachersDiary.Data.Ef.Entities
{
    public class RoleEntity : IdentityRole
    {
        public bool IsVisible { get; set; }
    }
}
