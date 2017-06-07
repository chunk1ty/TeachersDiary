using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachersDiary.Services.DateTime
{
    public partial class DateTimeService : DateTimeService.IDateTimeService
    {
        public System.DateTime UtcNow
        {
            get { return System.DateTime.UtcNow; }
        }
    }
}
