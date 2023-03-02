using AutoMapper;
using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.DataTransferObjects;
using InnoGotchi.API.Entities.Models;
using InnoGotchi.API.Entities.Static;

namespace InnoGotchi.API
{
    public class MappingProfile : Profile
    {
        const int petPositionX = 1136;
        const int petPositionY = 864;

        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.Role, opt => opt.MapFrom(src => Roles.USER))
                .ForMember(u => u.AvatarFileName, opt => opt.MapFrom(src => "ava_default.png"))
                .ForMember(u => u.IsInGame, opt => opt.MapFrom(src => true))
                .ForMember(u => u.IsMusic, opt => opt.MapFrom(src => true));
            CreateMap<User, UserInfoWithoutPasswordDto>();
            CreateMap<UserInfoDto, User>();
            CreateMap<BodyDto, Body>().ReverseMap();
            CreateMap<BodyPartDto, Eyes>().ReverseMap();
            CreateMap<BodyPartDto, Mouth>().ReverseMap();
            CreateMap<BodyPartDto, Nose>().ReverseMap();
            CreateMap<PetDto, Pet>()
                .ForMember(p => p.Age, age => age.MapFrom(src => 0))
                .ForMember(p => p.TimeOfCreating, time => time.MapFrom(src => DateTime.Now))
                .ForMember(p => p.LastEatTime, time => time.MapFrom(src => DateTime.Now))
                .ForMember(p => p.LastDrinkTime, time => time.MapFrom(src => DateTime.Now))
                .ForMember(p => p.LastHealthTime, time => time.MapFrom(src => DateTime.Now))
                .ForMember(p => p.LastMoodTime, time => time.MapFrom(src => DateTime.Now))
                .ForMember(p => p.positionX, pos => pos.MapFrom(src => petPositionX))
                .ForMember(p => p.positionY, pos => pos.MapFrom(src => petPositionY))
                .ForMember(p => p.isDead, pos => pos.MapFrom(src => false));
            CreateMap<Pet, PetToReturnDto>();
            CreateMap<FarmToCreate, Farm>();
            CreateMap<User, GuestInfo>();
            CreateMap<Statistics, StatisticsDto>();
            CreateMap<StatisticsBase, Statistics>();
        }
    }
}