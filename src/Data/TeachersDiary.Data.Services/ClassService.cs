using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bytes2you.Validation;
using TeachersDiary.Common;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Entities;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Data.Services
{
    public class ClassService : IClassService
    {
        private readonly IEntityFrameworkGenericRepository<ClassEntity> _entityFrameworkGenericRepository;
        private readonly IQuerySettings<ClassEntity> _querySettings;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;
        private readonly IEncryptingService _encryptingService;
        private readonly IUserService _userService;
        private readonly ILoggingService _loggingService;
       
        public ClassService(
            IEntityFrameworkGenericRepository<ClassEntity> entityFrameworkGenericRepository,
            IQuerySettings<ClassEntity> querySettings,
            IUnitOfWork unitOfWork, 
            IMappingService mappingService, 
            IEncryptingService encryptingService, IUserService userService, ILoggingService loggingService)
        {
            Guard.WhenArgument(entityFrameworkGenericRepository, nameof(entityFrameworkGenericRepository)).IsNull().Throw();
            Guard.WhenArgument(querySettings, nameof(querySettings)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            Guard.WhenArgument(mappingService, nameof(mappingService)).IsNull().Throw();
            Guard.WhenArgument(encryptingService, nameof(encryptingService)).IsNull().Throw();

            _entityFrameworkGenericRepository = entityFrameworkGenericRepository;
            _unitOfWork = unitOfWork;
            _mappingService = mappingService;
            _encryptingService = encryptingService;
            _userService = userService;
            _loggingService = loggingService;
            _querySettings = querySettings;
        }
      
        public async Task<ClassDomain> GetClassByClassIdAsync(string classId)
        {
           Guard.WhenArgument(classId, nameof(classId)).IsNull().Throw();

            var decodedClassId = _encryptingService.DecodeId(classId);

            _querySettings.Include(x => x.Students.Select(y => y.Absences));
            _querySettings.Where(x => x.Id == decodedClassId);
            _querySettings.ReadOnly = false;

            var classEntities =  await _entityFrameworkGenericRepository.GetAllAsync(_querySettings);

            var @class = classEntities.SingleOrDefault();
            if (@class == null)
            {
                return new ClassDomain();
            }

            var classDomain = _mappingService.Map<ClassDomain>(@class);

            return classDomain;
        }
        
        public async Task<IEnumerable<ClassDomain>> GetClassesBySchoolIdAsync(int  schoolId)
        {
            _querySettings.Where(x => x.SchoolId == schoolId);
            _querySettings.ReadOnly = true;

            var classeEntities = await _entityFrameworkGenericRepository.GetAllAsync(_querySettings);
            var classDomains = _mappingService.Map<IEnumerable<ClassDomain>>(classeEntities);

            var users = await _userService.GetTeachersBySchoolIdAsync();

            foreach (var @class in classDomains)
            {
                var classTeacher = users.SingleOrDefault(x => x.Id == @class.ClassTeacherId);

                @class.ClassTeacher = classTeacher;
            }

            return classDomains;
        }

        public void AddRange(List<ClassDomain> classDomains)
        {
            Guard.WhenArgument(classDomains, nameof(classDomains)).IsNull().Throw();

            var classEntities = _mappingService.Map<List<ClassEntity>>(classDomains);

            _entityFrameworkGenericRepository.AddRange(classEntities);
            _unitOfWork.Commit();
        }

        // TODO delete with only one query ??
        public async Task DeleteByIdAsync(string classId)
        {
            Guard.WhenArgument(classId, nameof(classId)).IsNull().Throw();

            var decodedClassId = _encryptingService.DecodeId(classId);

            var classEntity = await _entityFrameworkGenericRepository.GetByIdAsync(decodedClassId);

            if (classEntity != null)
            {
                _entityFrameworkGenericRepository.Delete(classEntity);

                _unitOfWork.Commit();
            }
        }

        public OperationStatus Add(ClassDomain @class)
        {
            Guard.WhenArgument(@class, nameof(@class)).IsNull().Throw();
            
            try
            {
                // TODO techdeb only 1 for Blagoev
                @class.SchoolId = 1;

                var classEntity = _mappingService.Map<ClassEntity>(@class);
                _entityFrameworkGenericRepository.Add(classEntity);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("Cannot insert duplicate key row in object"))
                {
                    return new FailureStatus("Съществува клас с избраният класен ръководител.");
                }

                // TODO how to log classEntity?
                _loggingService.Error(ex);

                return new FailureStatus("Възникна грешка при създаването на класа. Моля свържете се със ситемният администратор.");
            }

            return new SuccessStatus();
        }
    }
}
