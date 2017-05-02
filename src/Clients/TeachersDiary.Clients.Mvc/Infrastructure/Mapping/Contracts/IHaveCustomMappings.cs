using AutoMapper;

namespace TeachersDiary.Clients.Mvc.Infrastructure.Mapping.Contracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfiguration configuration);
    }
}
