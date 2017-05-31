using Bytes2you.Validation;

using TeachersDiary.Data.Contracts;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Entities;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Data.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;

        public TeacherService(
            IUnitOfWork unitOfWork,
            IMappingService mappingService, 
            ITeacherRepository teacherRepository)
        {
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            Guard.WhenArgument(mappingService, nameof(mappingService)).IsNull().Throw();
            Guard.WhenArgument(teacherRepository, nameof(teacherRepository)).IsNull().Throw();

            _unitOfWork = unitOfWork;
            _mappingService = mappingService;
            _teacherRepository = teacherRepository;
        }

        public void Add(TeacherDomain teacherDomain)
        {
            Guard.WhenArgument(teacherDomain, nameof(teacherDomain)).IsNull().Throw();

            var classEntity = _mappingService.Map<TeacherEntity>(teacherDomain);

            _teacherRepository.Add(classEntity);

            _unitOfWork.Commit();
        }
    }
}
