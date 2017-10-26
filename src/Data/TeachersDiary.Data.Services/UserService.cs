using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bytes2you.Validation;
using TeachersDiary.Common.Constants;
using TeachersDiary.Common.Enumerations;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Identity.Contracts;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Data.Services
{
    public class UserService : IUserService
    {
        private readonly IIdentityUserManagerService _userManager;
        private readonly IMappingService _mappingService;
        private readonly ITeachersDiaryDbContext _dbContext;

        public UserService(IIdentityUserManagerService userManager, IMappingService mappingService, ITeachersDiaryDbContext dbContext)
        {
            _mappingService = mappingService;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDomain>> GetAllAsync()
        {
            // TODO techdeb only 1 for Blagoev
            var users = await _userManager.GetAllBySchoolIdAsync(1);
            var roles = _dbContext.GetRoles();
            var usersDomain = new List<UserDomain>();

            var teacherRole = roles.SingleOrDefault(x => x.Name == ApplicationRoles.Teacher.ToString());
            var teachers = users.Where(x => x.Roles.Any(y => y.RoleId == teacherRole.Id));
            var teachersDomain = _mappingService.Map<IEnumerable<UserDomain>>(teachers).ToList();
            teachersDomain.ForEach(x => x.Role = ApplicationRoles.Teacher);
            usersDomain.AddRange(teachersDomain);

            var usersWithoutRoles = users.Where(x => x.Roles.Count == 0);
            var usersWithoutRolesDomain = _mappingService.Map<IEnumerable<UserDomain>>(usersWithoutRoles).ToList();
            usersWithoutRolesDomain.ForEach(x => x.Role = ApplicationRoles.None);
            usersDomain.AddRange(usersWithoutRolesDomain);

            return usersDomain;
        }

        public async Task<IEnumerable<UserDomain>> GetTeachersBySchoolIdAsync()
        {
            // TODO techdeb only 1 for Blagoev
            var users = await _userManager.GetAllBySchoolIdAsync(1);
            var roles = _dbContext.GetRoles();

            var teacherRole = roles.SingleOrDefault(x => x.Name == ApplicationRoles.Teacher.ToString());
            var teachers = users.Where(x => x.Roles.Any(y => y.RoleId == teacherRole.Id));
            var teachersDomain = _mappingService.Map<IEnumerable<UserDomain>>(teachers).ToList();

            return teachersDomain;
        }

        public async Task<UserDomain> GetUserByIdAsync(string id)
        {
            // TODO ??
            if (id == null)
            {
                return null;
            }

            var userEntity = await _userManager.GetUserWithSchoolByUserIdAsync(id);
            
            if (userEntity == null)
            {
                // null or new User ??
                return null;
            }

            var userDomain = _mappingService.Map<UserDomain>(userEntity);

            return userDomain;
        }


        public UserDomain GetUserById(string id)
        {
            // TODO ??
            if (id == null)
            {
                return null;
            }

            var userEntity = _userManager.GetUserWithSchoolByUserId(id);

            if (userEntity == null)
            {
                // null or new User ??
                return null;
            }

            var userDomain = _mappingService.Map<UserDomain>(userEntity);

            return userDomain;
        }
    }
}
