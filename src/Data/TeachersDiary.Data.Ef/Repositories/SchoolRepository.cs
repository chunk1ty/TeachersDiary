using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TeachersDiary.Data.Contracts;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Ef.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly ITeachersDiaryDbContext _teachersDiaryDb;

        public SchoolRepository(ITeachersDiaryDbContext teachersDiaryDb)
        {
            _teachersDiaryDb = teachersDiaryDb;
        }

        public async Task<IEnumerable<SchoolEntity>> GetAllSchoolNamesAsync()
        {
            return await _teachersDiaryDb.Schools.ToListAsync();
        }
    }
}
