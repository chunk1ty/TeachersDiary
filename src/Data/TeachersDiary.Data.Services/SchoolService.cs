using System.Collections.Generic;
using System.Threading.Tasks;

using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Entities;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Data.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly IEntityFrameworkGenericRepository<SchoolEntity> _entityFrameworkGenericRepository;
        private readonly IMappingService _mappingService;

        public SchoolService(
            IEntityFrameworkGenericRepository<SchoolEntity> entityFrameworkGenericRepository, 
            IMappingService mappingService)
        {
            _entityFrameworkGenericRepository = entityFrameworkGenericRepository;
            _mappingService = mappingService;
        }

        public async Task<IEnumerable<SchoolDomain>> GetAllAsync()
        {
            var schoolEntities = await _entityFrameworkGenericRepository.GetAllAsync();

            var schoolDomains = _mappingService.Map<IEnumerable<SchoolDomain>>(schoolEntities);

            return schoolDomains;
        }
    }
}
