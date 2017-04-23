using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Class>> GetAllWithStudentsAsync()
        {
            return await _teacherDiaryDbContext.Classes
                .Include(x => x.Students)
                .ToListAsync();
        }

        public async Task<Class> GetClassWithStudentsByClassIdAsync(Guid classId)
        {
            var @class = await _teacherDiaryDbContext.Classes
                .Include(x => x.Students)
                .SingleOrDefaultAsync(x => x.Id == classId);

            return @class;
        }
    }
}
