using TeachersDiary.Data.Ef.Models;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Domain
{
    public class UserDomain : IMap<UserEntity>
    {
        public string Email { get; set; }
        
        public string Id { get; set; }
       
        public string UserName { get; set; }
    }
}
