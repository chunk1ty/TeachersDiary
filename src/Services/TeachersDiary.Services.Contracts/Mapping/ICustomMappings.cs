using AutoMapper;

namespace TeachersDiary.Services.Contracts.Mapping
{
    public interface ICustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
