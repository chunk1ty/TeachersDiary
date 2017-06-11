using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.ViewModels.User
{
    public class UserViewModel : IMap<UserDomain>
    {
        public string Email { get; set; }

        public string Id { get; set; }

        public string UserName { get; set; }

        public int SchoolId { get; set; }
    }
}