using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersDiary.Data.Entities;

namespace TeachersDiary.Data.Contracts
{
    public interface ITeacherRepository
    {
        void Add(TeacherEntity teacher);
    }
}
