using AutoMapper;

using TakeHomeMunros.Data;
using TakeHomeMunrosApi.Models;

namespace TakeHomeMunrosApi.Mappings
{
    public class MunroProfile : Profile
    {
        public MunroProfile()
        {
            CreateMap<Munro, MunroModel>()
                .ForMember(dest => dest.HillCategory,
                    opt => opt.MapFrom(src => src.HillCategoryPost1997));
        }
    }
}
