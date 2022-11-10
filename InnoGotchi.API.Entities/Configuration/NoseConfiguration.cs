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
    public class NoseConfiguration : IEntityTypeConfiguration<Nose>
    {
        public void Configure(EntityTypeBuilder<Nose> builder)
        {
            builder.HasData(
                new Nose
                {
                    Id = 1,
                    Name = "Cat Nose",
                    FileName = "nose_cat.png"
                },
                new Nose
                {
                    Id = 2,
                    Name = "Round Nose",
                    FileName = "nose_round.png"
                },
                new Nose
                {
                    Id = 3,
                    Name = "Anime Nose",
                    FileName = "nose_anime.png"
                },
                new Nose
                {
                    Id = 4,
                    Name = "Egg Nose",
                    FileName = "nose_egg.png"
                },
                new Nose
                {
                    Id = 5,
                    Name = "Sharp Nose",
                    FileName = "nose_sharp.png"
                }
            );
        }
    }
}
