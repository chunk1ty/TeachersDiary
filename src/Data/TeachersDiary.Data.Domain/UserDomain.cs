using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Data.Domain
{
    public class UserDomain : IMapFrom<UserEntity>, IMapTo<UserEntity>
    {
        public string Email { get; set; }
        
        public string Id { get; set; }
       
        public string UserName { get; set; }
    }
}
