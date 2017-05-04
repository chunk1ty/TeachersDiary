using AutoMapper;
using AutoMapper.Configuration;

namespace TeachersDiary.Services.Mapping.Contracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
