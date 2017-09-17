using AutoMapper;

using TeachersDiary.Common.Extensions;
using TeachersDiary.Data.Entities;
using TeachersDiary.Services;
using TeachersDiary.Services.Contracts;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Domain
{
   public class AbsenceDomain : IMap<AbsenceEntity>, ICustomMappings
    {
        public string Id { get; set; }

        public string StudentId { get; set; }

        public int MonthId { get; set; }

        public double Excused { get; set; }

        public double NotExcused { get; set; }

        public string NotExcusedAsFractionNumber
        {
            get { return NotExcused.ToFractionNumber(); }
        }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            IEncryptingService encryptingService = new EncryptingService();

            configuration.CreateMap<AbsenceEntity, AbsenceDomain>()
                .ForMember(domain => domain.Id, x => x.MapFrom(entity => encryptingService.EncodeId(entity.Id)))
                .ForMember(domain => domain.StudentId,
                    x => x.MapFrom(entity => encryptingService.EncodeId(entity.StudentId)));

            configuration.CreateMap<AbsenceDomain, AbsenceEntity>()
                .ForMember(entity => entity.Id, x => x.MapFrom(domain => encryptingService.DecodeId(domain.Id)))
                .ForMember(entity => entity.StudentId, x => x.MapFrom(domain => encryptingService.DecodeId(domain.StudentId)));
        }
    }
}
