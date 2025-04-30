using AutoMapper;
using VialambreAppTest1.Models;
using VialambreAppTest1.ViewModels;

namespace VialambreAppTest1.Utility
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UserInputVM>().ReverseMap();

            CreateMap<AppUser, LoginVM>().ReverseMap();

            CreateMap<Brief, BriefVM>().ReverseMap();

            CreateMap<Pieza, PiezaVM>().ReverseMap();
        }
    }
}
