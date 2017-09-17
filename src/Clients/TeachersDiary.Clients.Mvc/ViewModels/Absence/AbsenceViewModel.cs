using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.ViewModels.Absence
{
    public class AbsenceViewModel : IMap<AbsenceDomain>
    {
        public string Id { get; set; }

        public string StudentId { get; set; }

        public int MonthId { get; set; }

        public double Excused { get; set; }

        public double NotExcused { get; set; }

        public string NotExcusedAsFractionNumber { get; set; }
    }
}