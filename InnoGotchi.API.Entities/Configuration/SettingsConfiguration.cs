using InnoGotchi.API.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.Configuration
{
    public class SettingsConfiguration : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.HasData(
                new Settings
                {
                    Id = 1,
                    AvatarFileName = "ava_1.png",
                    IsInGame = false,
                    IsMusic = true
                } ,
                new Settings
                {
                    Id = 2,
                    AvatarFileName = "ava_2.png",
                    IsInGame = false,
                    IsMusic = false
                }
            );
        }
    }
}
