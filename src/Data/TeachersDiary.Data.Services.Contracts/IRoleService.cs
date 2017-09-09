using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersDiary.Common.Enumerations;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface IRoleService
    {
        Task ChangeUserRoleAsync(string userId, ApplicationRoles role);
    }
}
