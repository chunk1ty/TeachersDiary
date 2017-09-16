using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bytes2you.Validation;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Entities;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Data.Services
{
    public class StudentService: IStudentService
    {
        private readonly IEntityFrameworkGenericRepository<StudentEntity> _entityFrameworkGenericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;
        private readonly IEncryptingService _encryptingService;

        public StudentService(IEntityFrameworkGenericRepository<StudentEntity> entityFrameworkGenericRepository, IUnitOfWork unitOfWork, IMappingService mappingService, IEncryptingService encryptingService)
        {
            _entityFrameworkGenericRepository = entityFrameworkGenericRepository;
            _unitOfWork = unitOfWork;
            _mappingService = mappingService;
            _encryptingService = encryptingService;
        }

        public async Task<StudentDomain> GetByIdAsync(string id)
        {
            Guard.WhenArgument(id, nameof(id)).IsNull().Throw();

            var decodedId = _encryptingService.DecodeId(id);

            var student = await _entityFrameworkGenericRepository.GetByIdAsync(decodedId);
            var studentDomain = _mappingService.Map<StudentDomain>(student);

            return studentDomain;
        }

        public void Add(StudentDomain student)
        {
            Guard.WhenArgument(student, nameof(student)).IsNull().Throw();

            var studentEntity = _mappingService.Map<StudentEntity>(student);

            _entityFrameworkGenericRepository.Add(studentEntity);
            _unitOfWork.Commit();
        }

        public void Update(StudentDomain student)
        {
            Guard.WhenArgument(student, nameof(student)).IsNull().Throw();

            var studentEntity = _mappingService.Map<StudentEntity>(student);

            _entityFrameworkGenericRepository.Update(studentEntity);
            _unitOfWork.Commit();
        }


        // TODO delete with only one query ??
        public async Task DeleteByIdAsync(string id)
        {
            Guard.WhenArgument(id, nameof(id)).IsNull().Throw();

            var decodedClassId = _encryptingService.DecodeId(id);

            var studentEntity = await _entityFrameworkGenericRepository.GetByIdAsync(decodedClassId);

            if (studentEntity != null)
            {
                _entityFrameworkGenericRepository.Delete(studentEntity);

                _unitOfWork.Commit();
            }
        }
    }
}
