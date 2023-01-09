﻿using AutoMapper;
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
            CreateMap<BodyDto, Body>();
            CreateMap<BodyPartDto, Eyes>();
            CreateMap<BodyPartDto, Mouth>();
            CreateMap<BodyPartDto, Nose>();
            CreateMap<PetDto, Pet>()
                .ForMember(p => p.Age, age => age.MapFrom(src => 0));
            CreateMap<Pet, PetToReturnDto>();
            CreateMap<FarmToCreate, Farm>();
            CreateMap<User, GuestInfo>();
            CreateMap<Statistics, StatisticsDto>();
        }
    }
}