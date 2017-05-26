using System.Data.Entity;

using Bytes2you.Validation;

using TeachersDiary.Data.Contracts;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Ef.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ITeachersDiaryDbContext _teacherDiaryDbContext;

        public TeacherRepository(ITeachersDiaryDbContext teacherDiaryDbContext)
        {
            _teacherDiaryDbContext = teacherDiaryDbContext;
        }

        public void Add(TeacherEntity teacher)
        {
            Guard.WhenArgument(teacher, nameof(teacher)).IsNull().Throw();

            var entry = _teacherDiaryDbContext.Entry(teacher);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                _teacherDiaryDbContext.Teachers.Add(teacher);
            }
        }
    }
}
