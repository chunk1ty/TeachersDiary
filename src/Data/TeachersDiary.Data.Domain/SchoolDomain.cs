using AutoMapper;

using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Services.Encrypting;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Data.Domain
{
    public class SchoolDomain : IMapFrom<SchoolEntity>, IHaveCustomMappings
    {
        private readonly IIdentifierProvider _identifierProvider;

        public SchoolDomain(IIdentifierProvider identifierProvider)
        {
            _identifierProvider = identifierProvider;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //configuration.CreateMap<SchoolEntity, SchoolDomain>()
            //    .ForMember(domain => domain.Id, entity => entity.MapFrom(x => "ss"));
        }
    }
}
