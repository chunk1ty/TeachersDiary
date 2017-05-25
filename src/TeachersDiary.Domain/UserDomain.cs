using TeachersDiary.Data.Ef.Models;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Domain
{
    public class UserDomain : IMapFrom<UserEntity>, IMapTo<UserEntity>
    {
        public string Email { get; set; }
        
        public string Id { get; set; }
       
        public string UserName { get; set; }
    }
}
