using Bytes2you.Validation;

using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Entities;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Data.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IEntityFrameworkGenericRepository<TeacherEntity> _entityFrameworkGenericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;

        public TeacherService(
            IUnitOfWork unitOfWork,
            IMappingService mappingService,
            IEntityFrameworkGenericRepository<TeacherEntity> entityFrameworkGenericRepository)
        {
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            Guard.WhenArgument(mappingService, nameof(mappingService)).IsNull().Throw();
            Guard.WhenArgument(entityFrameworkGenericRepository, nameof(entityFrameworkGenericRepository)).IsNull().Throw();

            _unitOfWork = unitOfWork;
            _mappingService = mappingService;
            _entityFrameworkGenericRepository = entityFrameworkGenericRepository;
        }

        public void Add(TeacherDomain teacherDomain)
        {
            Guard.WhenArgument(teacherDomain, nameof(teacherDomain)).IsNull().Throw();

            var classEntity = _mappingService.Map<TeacherEntity>(teacherDomain);

            _entityFrameworkGenericRepository.Add(classEntity);

            _unitOfWork.Commit();
        }
    }
}
