using TeachersDiary.Domain;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.ViewModels.Absence
{
    public class AbsenceViewModel : IMap<AbsenceDomain>
    {
        public string EncodedId { get; set; }

        public string EncodedStudentId { get; set; }

        public int MonthId { get; set; }

        public double Excused { get; set; }

        public double NotExcused { get; set; }

        public string NotExcusedAsFractionNumber { get; set; }
    }
}