using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Contracts
{
    public interface ITeacherRepository
    {
        void Add(TeacherEntity teacher);
    }
}
