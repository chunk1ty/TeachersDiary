using System.Collections.Generic;
using System.Threading.Tasks;
using Bytes2you.Validation;

using TeachersDiary.Data.Contracts;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Entities;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Encrypting;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Data.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;
        private readonly IEncryptingService _encryptingService;

        public ClassService(
            IClassRepository classRepository, 
            IUnitOfWork unitOfWork, 
            IMappingService mappingService, 
            IEncryptingService encryptingService)
        {
            Guard.WhenArgument(classRepository, nameof(classRepository)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            Guard.WhenArgument(mappingService, nameof(mappingService)).IsNull().Throw();
            Guard.WhenArgument(encryptingService, nameof(encryptingService)).IsNull().Throw();

            _classRepository = classRepository;
            _unitOfWork = unitOfWork;
            _mappingService = mappingService;
            _encryptingService = encryptingService;
        }

        public async Task<ClassDomain> GetClassWithStudentsByClassIdAsync(string classId)
        {
            var decodedClassId = _encryptingService.DecodeId(classId);

            var claaEntity = await _classRepository.GetClassWithStudentsAndAbsencesByClassIdAsync(decodedClassId);

            var classDomain = _mappingService.Map<ClassDomain>(claaEntity);

            return classDomain;
        }

        public async Task<IEnumerable<ClassDomain>> GetAllAvailableClassesForUserAsync(string userId)
        {
            var classeEntities = await _classRepository.GetAllClassesForUserAsync(userId);

            var classDomains = _mappingService.Map<IEnumerable<ClassDomain>>(classeEntities);

            return classDomains;
        }

        public void AddRange(List<ClassDomain> classDomains)
        {
            Guard.WhenArgument(classDomains, nameof(classDomains)).IsNull().Throw();

            var classEntities = _mappingService.Map<List<ClassEntity>>(classDomains);

            _classRepository.AddRange(classEntities);

            _unitOfWork.SaveChanges();
        }

        // TODO delete with only one query ??
        public async Task DeleteById(string classId)
        {
            var decodedClassId = _encryptingService.DecodeId(classId);

            var classEntity = await _classRepository.GetClassByIdAsync(decodedClassId);

            _classRepository.Delete(classEntity);

            _unitOfWork.SaveChanges();
        }
    }
}
