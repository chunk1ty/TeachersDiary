using TeachersDiary.Common.Enumerations;
using TeachersDiary.Data.Entities;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Domain
{
    public class UserDomain : IMap<UserEntity>
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
        
        public string Id { get; set; }
       
        public string UserName { get; set; }

        public int SchoolId { get; set; }

        public ApplicationRoles Role { get; set; }
    }
}
