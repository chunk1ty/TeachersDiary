using AutoMapper;

namespace TeacherDiary.Clients.Mvc.Infrastructure.Mapping.Contracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfiguration configuration);
    }
}
