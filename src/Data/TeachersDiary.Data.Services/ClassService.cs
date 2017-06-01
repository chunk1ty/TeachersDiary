﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bytes2you.Validation;

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
    public class ClassService : IClassService
    {
        private readonly IQuerySettings<ClassEntity> _querySettings;
        private readonly IEntityFrameworkGenericRepository<ClassEntity> _entityFrameworkGenericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;
        private readonly IEncryptingService _encryptingService;
       
        public ClassService(
            IEntityFrameworkGenericRepository<ClassEntity> entityFrameworkGenericRepository, 
            IUnitOfWork unitOfWork, 
            IMappingService mappingService, 
            IEncryptingService encryptingService, 
            IQuerySettings<ClassEntity> querySettings)
        {
            Guard.WhenArgument(entityFrameworkGenericRepository, nameof(entityFrameworkGenericRepository)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            Guard.WhenArgument(mappingService, nameof(mappingService)).IsNull().Throw();
            Guard.WhenArgument(encryptingService, nameof(encryptingService)).IsNull().Throw();

            _entityFrameworkGenericRepository = entityFrameworkGenericRepository;
            _unitOfWork = unitOfWork;
            _mappingService = mappingService;
            _encryptingService = encryptingService;
            _querySettings = querySettings;
        }
      
        public async Task<ClassDomain> GetClassWithStudentsByClassIdAsync(string classId)
        {
            var decodedClassId = _encryptingService.DecodeId(classId);

            _querySettings.Include(x => x.Students.Select(y => y.Absences));
            _querySettings.Where(x => x.Id == decodedClassId);
            _querySettings.ReadOnly = true;

            var claaEntity =  await _entityFrameworkGenericRepository.GetByIdAsync(decodedClassId, _querySettings);

            var classDomain = _mappingService.Map<ClassDomain>(claaEntity);

            return classDomain;
        }
        
        public async Task<IEnumerable<ClassDomain>> GetAllAvailableClassesForUserAsync(string userId)
        {
            _querySettings.Where(x => x.CreatedBy == userId);
            _querySettings.ReadOnly = true;

            var classeEntities = await _entityFrameworkGenericRepository.GetAllAsync(_querySettings);

            var classDomains = _mappingService.Map<IEnumerable<ClassDomain>>(classeEntities);

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
        public async Task DeleteById(string classId)
        {
            var decodedClassId = _encryptingService.DecodeId(classId);

            var classEntity = await _entityFrameworkGenericRepository.GetByIdAsync(decodedClassId);

            _entityFrameworkGenericRepository.Delete(classEntity);

            _unitOfWork.Commit();
        }
    }
}
