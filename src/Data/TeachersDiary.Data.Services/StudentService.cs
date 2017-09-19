using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class StudentService: IStudentService
    {
        private readonly IEntityFrameworkGenericRepository<StudentEntity> _entityFrameworkGenericRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappingService _mappingService;
        private readonly IEncryptingService _encryptingService;
        private readonly ILoggingService _loggingService;

        public StudentService(IEntityFrameworkGenericRepository<StudentEntity> entityFrameworkGenericRepository, IUnitOfWork unitOfWork, IMappingService mappingService, IEncryptingService encryptingService, ILoggingService loggingService)
        {
            _entityFrameworkGenericRepository = entityFrameworkGenericRepository;
            _unitOfWork = unitOfWork;
            _mappingService = mappingService;
            _encryptingService = encryptingService;
            _loggingService = loggingService;
        }

        public async Task<StudentDomain> GetByIdAsync(string id)
        {
            Guard.WhenArgument(id, nameof(id)).IsNull().Throw();

            var decodedId = _encryptingService.DecodeId(id);

            var student = await _entityFrameworkGenericRepository.GetByIdAsync(decodedId);
            var studentDomain = _mappingService.Map<StudentDomain>(student);

            return studentDomain;
        }

        public OperationStatus Add(StudentDomain student)
        {
            Guard.WhenArgument(student, nameof(student)).IsNull().Throw();

            try
            {
                var studentEntity = _mappingService.Map<StudentEntity>(student);

                _entityFrameworkGenericRepository.Add(studentEntity);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("Cannot insert duplicate key row in object")){
                    
                    return new FailureStatus($"Ученик с номер { student.Number } вече съществува!");
                }

                // TODO how to log classEntity?
                _loggingService.Error(ex);

                return new FailureStatus("Възникна грешка при създаването на ученикът. Моля свържете се със ситемният администратор.");
            }

            return new SuccessStatus();
        }

        public OperationStatus Update(StudentDomain student)
        {
            Guard.WhenArgument(student, nameof(student)).IsNull().Throw();

            try
            {
                var studentEntity = _mappingService.Map<StudentEntity>(student);

                _entityFrameworkGenericRepository.Update(studentEntity);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.InnerException.Message.Contains("Cannot insert duplicate key row in object"))
                {

                    return new FailureStatus($"Ученик с номер { student.Number } вече съществува!");
                }

                // TODO how to log classEntity?
                _loggingService.Error(ex);

                return new FailureStatus("Възникна грешка при създаването на ученикът. Моля свържете се със ситемният администратор.");
            }

            return new SuccessStatus();
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
