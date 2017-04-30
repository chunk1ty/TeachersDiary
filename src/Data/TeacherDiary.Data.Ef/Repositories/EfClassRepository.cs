using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Bytes2you.Validation;
using TeacherDiary.Data.Contracts;
using TeacherDiary.Data.Ef.Contracts;
using TeacherDiary.Data.Ef.Extensions;
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

        public void Add(Class @class)
        {
            Guard.WhenArgument(@class, nameof(@class)).IsNull().Throw();

            var entry = _teacherDiaryDbContext.Entry(@class);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                _teacherDiaryDbContext.Classes.Add(@class);
            }
        }

        public void AddRange(List<Class> clases)
        {
            Guard.WhenArgument(clases, nameof(clases)).IsNull().Throw();

            _teacherDiaryDbContext.Classes.AddRange(clases);
        }

        public async Task<IEnumerable<Class>> GetAllWithStudentsAsync()
        {
            return await _teacherDiaryDbContext.Classes
                .Include(x => x.Students)
                .ToListAsync();
        }

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            return await _teacherDiaryDbContext.Classes
                .ToListAsync();
        }

        public async Task<Class> GetClassWithStudentsAndAbsencesByClassIdAsync(Guid classId)
        {
            var @class = await _teacherDiaryDbContext.Classes
                .Include(x => x.Students.Select(y => y.Absences))
                .SingleOrDefaultAsync(x => x.Id == classId);

            return @class;
        }
    }
}
