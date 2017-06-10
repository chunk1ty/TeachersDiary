using TeachersDiary.Data.Entities;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Domain
{
    public class TeacherDomain : IMap<TeacherEntity>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int SchoolId { get; set; }

        public string UserId { get; set; }
    }
}
