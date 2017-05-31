using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bytes2you.Validation;

using TeachersDiary.Data.Contracts;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Ef.GenericRepository;
using TeachersDiary.Data.Ef.GenericRepository.Contracts;
using TeachersDiary.Data.Entities;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Encrypting;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Data.Services
{
    // to much responsibilities ?
    public class ClassService : IClassService
    {
        private readonly IEntityFrameworkGenericRepository<ClassEntity> _repository;
        private readonly IQuerySettings<ClassEntity> _querySettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;
        private readonly IEncryptingService _encryptingService;

        // injections ?
        public ClassService(
            IEntityFrameworkGenericRepository<ClassEntity> repository, 
            IUnitOfWork unitOfWork, 
            IMappingService mappingService, 
            IEncryptingService encryptingService, 
            IQuerySettings<ClassEntity> querySettings)
        {
            Guard.WhenArgument(repository, nameof(repository)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            Guard.WhenArgument(mappingService, nameof(mappingService)).IsNull().Throw();
            Guard.WhenArgument(encryptingService, nameof(encryptingService)).IsNull().Throw();

            _repository = repository;
            _unitOfWork = unitOfWork;
            _mappingService = mappingService;
            _encryptingService = encryptingService;
            _querySettings = querySettings;
        }

        // to much responsibilities ?
        public async Task<ClassDomain> GetClassWithStudentsByClassIdAsync(string classId)
        {
            var decodedClassId = _encryptingService.DecodeId(classId);

            //var claaEntity = await _repository.GetClassWithStudentsAndAbsencesByClassIdAsync(decodedClassId);
            _querySettings.Include(x => x.Students.Select(y => y.Absences));
            _querySettings.Where(x => x.Id == decodedClassId);
            _querySettings.ReadOnly = true;

            var claaEntity =  await _repository.GetAllAsync(_querySettings);

            var classDomain = _mappingService.Map<ClassDomain>(claaEntity.SingleOrDefault());

            return classDomain;
        }
        
        public async Task<IEnumerable<ClassDomain>> GetAllAvailableClassesForUserAsync(string userId)
        {
            //var classeEntities = await _repository.GetAllClassesForUserAsync(userId);

            _querySettings.Where(x => x.CreatedBy == userId);
            _querySettings.ReadOnly = true;

            var classeEntities = await _repository.GetAllAsync(_querySettings);

            var classDomains = _mappingService.Map<IEnumerable<ClassDomain>>(classeEntities);

            return classDomains;
        }

        public void AddRange(List<ClassDomain> classDomains)
        {
            Guard.WhenArgument(classDomains, nameof(classDomains)).IsNull().Throw();

            var classEntities = _mappingService.Map<List<ClassEntity>>(classDomains);

            _repository.AddRange(classEntities);
            _unitOfWork.Commit();
        }

        // TODO delete with only one query ??
        public async Task DeleteById(string classId)
        {
            var decodedClassId = _encryptingService.DecodeId(classId);

            //var classEntity = await _repository.GetClassByIdAsync(decodedClassId);

            var classEntity = await _repository.GetByIdAsync(decodedClassId);

            _repository.Delete(classEntity);

            _unitOfWork.Commit();
        }
    }
}
