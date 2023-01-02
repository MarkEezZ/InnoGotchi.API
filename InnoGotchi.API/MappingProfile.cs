using AutoMapper;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;

namespace InnoGotchi.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.AvatarFileName, opt => opt.MapFrom(src => "ava_default.png"))
                .ForMember(u => u.IsInGame, opt => opt.MapFrom(src => true))
                .ForMember(u => u.IsMusic, opt => opt.MapFrom(src => true));

            CreateMap<User, UserInfoDto>().ReverseMap();

            CreateMap<BodyPartDto, Body>();
            CreateMap<BodyPartDto, Eyes>();
            CreateMap<BodyPartDto, Mouth>();
            CreateMap<BodyPartDto, Nose>();
        }
    }
}