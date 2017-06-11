using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TeachersDiary.Data.Ef.Models
{
    public class UserEntity : IdentityUser
    {
        public int SchoolId { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserEntity> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}