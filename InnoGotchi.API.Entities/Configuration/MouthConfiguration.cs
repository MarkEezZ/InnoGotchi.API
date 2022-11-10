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
    public class MouthConfiguration : IEntityTypeConfiguration<Mouth>
    {
        public void Configure(EntityTypeBuilder<Mouth> builder)
        {
            builder.HasData(
                new Mouth
                {
                    Id = 1,
                    Name = "Cat Mouth",
                    FileName = "mouth_cat.png"
                },
                new Mouth
                {
                    Id = 2,
                    Name = "Small Smile",
                    FileName = "mouth_small_smile.png"
                },
                new Mouth
                {
                    Id = 3,
                    Name = "Monster Mouth",
                    FileName = "mouth_monster.png"
                },
                new Mouth
                {
                    Id = 4,
                    Name = "Monster Libs",
                    FileName = "mouth_monster_libs.png"
                },
                new Mouth
                {
                    Id = 5,
                    Name = "Vampire Mouth",
                    FileName = "mouth_vampire.png"
                },
                new Mouth
                {
                    Id = 6,
                    Name = "Small Dash",
                    FileName = "mouth_small_dash.png"
                }
            );
        }
    }
}
