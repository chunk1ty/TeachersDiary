using TeachersDiary.Domain;

namespace TeachersDiary.Clients.Mvc.Infrastructure.Session
{
    public class SessionState
    {
        public UserDomain User { get; set; }

        public SchoolDomain School { get; set; }
    }
}