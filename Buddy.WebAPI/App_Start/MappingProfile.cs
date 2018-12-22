using AutoMapper;
using Buddy.CodeFirst.Models.Log;
using Buddy.WebAPI.Dtos.Log;

namespace Buddy.WebAPI.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Log, LogDto>();
        }
    }
}