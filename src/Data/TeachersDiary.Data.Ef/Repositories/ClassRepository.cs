using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Bytes2you.Validation;

using TeachersDiary.Data.Contracts;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Ef.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ITeachersDiaryDbContext _teacherDiaryDbContext;

        public ClassRepository(ITeachersDiaryDbContext teacherDiaryDbContext)
        {
            Guard.WhenArgument(teacherDiaryDbContext, nameof(teacherDiaryDbContext)).IsNull().Throw();

            _teacherDiaryDbContext = teacherDiaryDbContext;
        }

        public async Task<IEnumerable<ClassEntity>> GetAllWithStudentsAsync()
        {
            return await _teacherDiaryDbContext.Classes
                .Include(x => x.Students)
                .ToListAsync();
        }

        public async Task<IEnumerable<ClassEntity>> GetAllClassesForUserAsync(string userId)
        {
            Guard.WhenArgument(userId, nameof(userId)).IsNull().Throw();

            return await _teacherDiaryDbContext.Classes.Where(x => x.CreatedBy == userId)
                .ToListAsync();
        }

        public async Task<ClassEntity> GetClassWithStudentsAndAbsencesByClassIdAsync(int classId)
        {
            var @class = await _teacherDiaryDbContext.Classes
                .Include(x => x.Students.Select(y => y.Absences))
                .SingleOrDefaultAsync(x => x.Id == classId);

            return @class;
        }

        public async Task<ClassEntity> GetClassByIdAsync(int classId)
        {
            return await _teacherDiaryDbContext.Classes.FirstOrDefaultAsync(x => x.Id == classId);
        }

        public void BulkInsert(List<ClassEntity> clases)
        {
            Guard.WhenArgument(clases, nameof(clases)).IsNull().Throw();

            _teacherDiaryDbContext.Insert(clases);
        }

        public void Delete(ClassEntity @class)
        {
            Guard.WhenArgument(@class, nameof(@class)).IsNull().Throw();

            _teacherDiaryDbContext.Classes.Remove(@class);
        }
    }
}
