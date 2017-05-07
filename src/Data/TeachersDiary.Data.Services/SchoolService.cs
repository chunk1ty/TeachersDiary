using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeachersDiary.Data.Contracts;
using TeachersDiary.Data.Domain;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Services.Encrypting;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Data.Services
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMappingService _mappingService;

        public SchoolService(ISchoolRepository schoolRepository, IMappingService mappingService)
        {
            _schoolRepository = schoolRepository;
            _mappingService = mappingService;
        }

        public async Task<IEnumerable<SchoolDomain>> GetAllSchoolNamesAsync()
        {
            var schoolEntities = await _schoolRepository.GetAllSchoolNamesAsync();

            var schoolDomains = _mappingService.Map<IEnumerable<SchoolDomain>>(schoolEntities);

            return schoolDomains;
        }
    }
}
