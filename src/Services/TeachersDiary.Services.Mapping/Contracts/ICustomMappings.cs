using AutoMapper;
using AutoMapper.Configuration;

namespace TeachersDiary.Services.Mapping.Contracts
{
    public interface ICustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
