using AutoMapper;
using Data.Entities.Context;

namespace Models
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<USER, UserDto>();
        }
    }
}
