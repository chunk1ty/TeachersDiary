using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TeachersDiary.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public int SchoolId { get; set; }
        public virtual  SchoolEntity School { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserEntity> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}