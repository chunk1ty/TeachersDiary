using System.Collections.Generic;

using AutoMapper;

using TeachersDiary.Clients.Mvc.ViewModels.Absence;
using TeachersDiary.Common.Extensions;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.ViewModels.Student
{
    public class StudentViewModel : IMap<StudentDomain>, ICustomMappings
    {
        public StudentViewModel()
        {
            Absences = new List<AbsenceViewModel>();
        }

        public string EncodedId { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string TotalExcusedAbsences { get; set; }

        public string TotalNotExcusedAbsences { get; set; }

        public List<AbsenceViewModel> Absences { get; set; }

        public string GetTotalNotExcusedAbsences()
        {
            // workaround if TotalNotExcusedAbsences comes after error
            if (TotalNotExcusedAbsences.IsDoubleNumber())
            {
                var totalNotExcusedAbsencesAsDouble = double.Parse(TotalNotExcusedAbsences);
                var totalNotExcusedAbsencesAsFraction = totalNotExcusedAbsencesAsDouble.ToFractionNumber();

                return totalNotExcusedAbsencesAsFraction;
            }

            return TotalNotExcusedAbsences;

        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<StudentViewModel, StudentDomain>()
                .ForMember(domain => domain.EnteredTotalExcusedAbsences, x => x.MapFrom(view => view.TotalExcusedAbsences))
                .ForMember(domain => domain.EnteredTotalNotExcusedAbsences, x => x.MapFrom(view => view.TotalNotExcusedAbsences.ToDoubleNumber()));
        }
    }
}