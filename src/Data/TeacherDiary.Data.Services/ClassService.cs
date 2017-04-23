using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bytes2you.Validation;
using TeacherDiary.Data.Contracts;
using TeacherDiary.Data.Ef.Contracts;
using TeacherDiary.Data.Entities;
using TeacherDiary.Data.Services.Contracts;

namespace TeacherDiary.Data.Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly ITeacherDiaryDbContextSaveChanges _contextSaveChanges;

        public ClassService(IClassRepository classRepository, ITeacherDiaryDbContextSaveChanges contextSaveChanges)
        {
            Guard.WhenArgument(classRepository, nameof(classRepository)).IsNull().Throw();
            Guard.WhenArgument(contextSaveChanges, nameof(contextSaveChanges)).IsNull().Throw();

            _classRepository = classRepository;
            _contextSaveChanges = contextSaveChanges;
        }

        public void Add(Class @class)
        {
            Guard.WhenArgument(@class, nameof(@class)).IsNull().Throw();

            if (@class == null)
            {
                throw new ArgumentNullException(nameof(@class));
            }

            _classRepository.Add(@class);

            _contextSaveChanges.SaveChanges();
        }
    }
}
