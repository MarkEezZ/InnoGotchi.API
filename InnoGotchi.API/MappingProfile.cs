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
                .ForMember(u => u.SettingsId, opt => opt.MapFrom(src => 1));

            CreateMap<User, UserInfoDto>()
                .ConvertUsing<UserWithSettingsConverter>();

            CreateMap<UserInfoDto, User>()
                .ConvertUsing<UserFromSettings>();
        }
    }

    public class UserFromSettings : ITypeConverter<UserInfoDto, User>
    {
        public User Convert(UserInfoDto source, User destination, ResolutionContext context)
        {
            destination = new User
            {
               Name = source.Name,
               Surname = source.Surname,
               Age = source.Age,
               Password = source.Password,
               Email = source.Email,
               Settings = new Settings
               {
                   AvatarFileName = source.AvatarFileName,
                   IsInGame = source.IsInGame,
                   IsMusic = source.IsMusic
               }
            };

            return destination;
        }
    }

    public class UserWithSettingsConverter : ITypeConverter<User, UserInfoDto>
    {
        public UserInfoDto Convert(User source, UserInfoDto destination, ResolutionContext context)
        {
            var settings = source.Settings;

            destination = new UserInfoDto
            {
                Name = source.Name,
                Surname = source.Surname,
                Email = source.Email,
                Password = source.Password,
                Age = source.Age,
                AvatarFileName = settings.AvatarFileName,
                IsInGame = settings.IsInGame,
                IsMusic = settings.IsMusic
            };
            return destination;
        }
    }
}