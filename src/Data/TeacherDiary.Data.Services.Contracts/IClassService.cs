using System.Collections.Generic;
using System.Threading.Tasks;
using TeacherDiary.Data.Entities;

namespace TeacherDiary.Data.Services.Contracts
{
    public interface IClassService
    {
        void Add(Class system);

        Task<IEnumerable<Class>> GetAllAsync();
    }
}
