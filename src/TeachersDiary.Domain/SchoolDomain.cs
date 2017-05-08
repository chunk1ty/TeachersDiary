using AutoMapper;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Services;
using TeachersDiary.Services.Contracts;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Domain
{
    public class SchoolDomain : IMapFrom<SchoolEntity>, ICustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            IEncryptingService encryptingService = new EncryptingService();

            configuration.CreateMap<SchoolEntity, SchoolDomain>()
                .ForMember(domain => domain.Id, x => x.MapFrom(entity => encryptingService.EncodeId(entity.Id)));

            configuration.CreateMap<SchoolDomain, SchoolEntity>()
                .ForMember(entity => entity.Id, x => x.MapFrom(domain => encryptingService.DecodeId(domain.Id)));
        }
    }
}
