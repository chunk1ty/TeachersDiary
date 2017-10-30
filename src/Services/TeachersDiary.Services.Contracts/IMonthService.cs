using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersDiary.Common;

namespace TeachersDiary.Services.Contracts
{
    public interface IMonthService
    {
        List<Month> GetCurrentAndPreviousMonth();
    }
}
