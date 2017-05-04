using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Bytes2you.Validation;

using TeachersDiary.Data.Contracts;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Data.Services.Contracts;

namespace TeachersDiary.Data.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly ITeachersDiaryDbContextSaveChanges _contextSaveChanges;

        public ClassService(IClassRepository classRepository, ITeachersDiaryDbContextSaveChanges contextSaveChanges)
        {
            Guard.WhenArgument(classRepository, nameof(classRepository)).IsNull().Throw();
            Guard.WhenArgument(contextSaveChanges, nameof(contextSaveChanges)).IsNull().Throw();

            _classRepository = classRepository;
            _contextSaveChanges = contextSaveChanges;
        }

        public async Task<ClassEntity> GetClassWithStudentsByClassIdAsync(Guid classId)
        {
            return await _classRepository.GetClassWithStudentsAndAbsencesByClassIdAsync(classId);
        }

        public async Task<IEnumerable<ClassEntity>> GetAllAsync()
        {
            return await _classRepository.GetAllAsync();
        }

        public void Add(ClassEntity @class)
        {
            Guard.WhenArgument(@class, nameof(@class)).IsNull().Throw();

            _classRepository.Add(@class);

            _contextSaveChanges.SaveChanges();
        }

        public void AddRange(List<ClassEntity> classes)
        {
            Guard.WhenArgument(classes, nameof(classes)).IsNull().Throw();

            _classRepository.AddRange(classes);

            _contextSaveChanges.SaveChanges();
        }

        public async Task DeleteById(Guid classId)
        {
            var @class = await _classRepository.GetByIdAsync(classId);

            _classRepository.Delete(@class);

            _contextSaveChanges.SaveChanges();
        }
    }
}
