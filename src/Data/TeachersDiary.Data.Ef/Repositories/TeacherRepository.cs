using Bytes2you.Validation;

using TeachersDiary.Data.Contracts;
using TeachersDiary.Data.Ef.GenericRepository.Contracts;
using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Ef.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly IEntityFrameworkGenericRepository<TeacherEntity> _genericRepository;

        public TeacherRepository(IEntityFrameworkGenericRepository<TeacherEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public void Add(TeacherEntity teacher)
        {
            Guard.WhenArgument(teacher, nameof(teacher)).IsNull().Throw();

            _genericRepository.Add(teacher);
        }
    }
}
