using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using Bytes2you.Validation;

using TeachersDiary.Data.Contracts;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Ef.Extensions;
using TeachersDiary.Data.Ef.GenericRepository;
using TeachersDiary.Data.Ef.GenericRepository.Contracts;
using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Ef.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ITeachersDiaryDbContext _teacherDiaryDbContext;
        private readonly IEntityFrameworkGenericRepository<ClassEntity> _genericRepository;

        public ClassRepository(ITeachersDiaryDbContext teacherDiaryDbContext, IEntityFrameworkGenericRepository<ClassEntity> genericRepository)
        {
            Guard.WhenArgument(teacherDiaryDbContext, nameof(teacherDiaryDbContext)).IsNull().Throw();

            _teacherDiaryDbContext = teacherDiaryDbContext;
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<ClassEntity>> GetAllWithStudentsAsync()
        {
            var query = new QuerySettings<ClassEntity>();
            query.Include(x => x.Students);
            query.ReadOnly = true;

            //return await _genericRepository.All(query);

            //return await _teacherDiaryDbContext.Classes
            //    .Include(x => x.Students)
            //    .ToListAsync();

            return null;
        }

        public async Task<IEnumerable<ClassEntity>> GetAllClassesForUserAsync(string userId)
        {
            Guard.WhenArgument(userId, nameof(userId)).IsNull().Throw();

            var query = new QuerySettings<ClassEntity>();
            query.Where(x => x.CreatedBy == userId);
            query.ReadOnly = true;

           // var query1 = _genericRepository.GetAllAsync(query);

            var query2 = _teacherDiaryDbContext.Classes.Where(x => x.CreatedBy == userId);
                

            return await  query2.ToListAsync();
        }

        public async Task<ClassEntity> GetClassWithStudentsAndAbsencesByClassIdAsync(int classId)
        {
            // SingleOrDefault ?
            var query = new QuerySettings<ClassEntity>();
            query.Include(x => x.Students.Select(y => y.Absences));
            query.Where(x => x.Id == classId);
            query.ReadOnly = true;

           // var query1 = _genericRepository.GetAllAsync(query);

            //return result.SingleOrDefault();

            var query2 = _teacherDiaryDbContext.Classes
                .Include(x => x.Students.Select(y => y.Absences))
                .Where(x => x.Id == classId);

            //return @class;

            return null;
        }

        public async Task<ClassEntity> GetClassByIdAsync(int classId)
        {
            // SingleOrDefault ?
            return await _teacherDiaryDbContext.Classes.FirstAsync(x => x.Id == classId);
        }

        public void AddRange(List<ClassEntity> clases)
        {
            Guard.WhenArgument(clases, nameof(clases)).IsNull().Throw();

            _teacherDiaryDbContext.Classes.AddRange(clases);
        }

        public void Delete(ClassEntity @class)
        {
            Guard.WhenArgument(@class, nameof(@class)).IsNull().Throw();

            _teacherDiaryDbContext.Classes.Remove(@class);
        }
    }
}
