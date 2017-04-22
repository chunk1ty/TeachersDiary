using System.Data.Entity;
using TeacherDiary.Data.Contracts;
using TeacherDiary.Data.Ef.Contracts;
using TeacherDiary.Data.Entities;

namespace TeacherDiary.Data.Ef.Repositories
{
    public class EfClassRepository : IClassRepository
    {
        private readonly ITeacherDiaryDbContext _teacherDiaryDbContext;

        public EfClassRepository(ITeacherDiaryDbContext teacherDiaryDbContext)
        {
            _teacherDiaryDbContext = teacherDiaryDbContext;
        }

        public void Add(Class system)
        {
            var entry = _teacherDiaryDbContext.Entry(system);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                _teacherDiaryDbContext.Classes.Add(system);
            }
        }
    }
}
