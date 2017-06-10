using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using TeachersDiary.Common.Extensions;
using TeachersDiary.Data.Entities;
using TeachersDiary.Services.Encrypting;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Domain
{
    public class StudentDomain : IMap<StudentEntity>, ICustomMappings
    {
        public StudentDomain()
        {
            Absences = new HashSet<AbsenceDomain>();
        }

        public string EncodedId { get; set; }

        public int Number { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string EncodedIdClassId { get; set; }

        public double TotalExcusedAbsences
        {
            get
            {
                return Absences.Sum(x => x.Excused);
            }
        }

        public double TotalNotExcusedAbsences
        {
            get
            {
                return Absences.Sum(x => x.NotExcused);
            }
        }
        public double EnteredTotalNotExcusedAbsences { get; set; }

        public double EnteredTotalExcusedAbsences { get; set; }

        public ICollection<AbsenceDomain> Absences { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            IEncryptingService encryptingService = new EncryptingService();

            configuration.CreateMap<StudentEntity, StudentDomain>()
                .ForMember(domain => domain.EncodedId, x => x.MapFrom(entity => encryptingService.EncodeId(entity.Id)))
                .ForMember(domain => domain.EncodedIdClassId, x => x.MapFrom(entity => encryptingService.EncodeId(entity.ClassId)));

            configuration.CreateMap<StudentDomain, StudentEntity>()
                .ForMember(entity => entity.Id, x => x.MapFrom(domain => encryptingService.DecodeId(domain.EncodedId)))
                .ForMember(entity => entity.ClassId, x => x.MapFrom(domain => encryptingService.DecodeId(domain.EncodedIdClassId)));
        }
    }
}
