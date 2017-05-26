using TeachersDiary.Domain;

namespace TeachersDiary.Data.Services.Contracts
{
    public interface ITeacherService
    {
        void Add(TeacherDomain teacherDomain);
    }
}
