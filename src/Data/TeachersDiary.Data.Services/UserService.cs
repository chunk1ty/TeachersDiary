using System.Collections.Generic;
using System.Threading.Tasks;
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

        public UserService(IIdentityUserManagerService userManager, IMappingService mappingService)
        {
            _mappingService = mappingService;
            _userManager = userManager;
        }

        public async Task<IEnumerable<UserDomain>> GetAllAsync()
        {
            var users = await _userManager.GetAllAsync();

            var domainUsers = _mappingService.Map<IEnumerable<UserDomain>>(users);

            return domainUsers;
        }
    }
}
