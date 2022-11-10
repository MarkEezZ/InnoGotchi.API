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
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasData(
                new Pet
                {
                    Id = 1,
                    Name = "Dungeon Master",
                    Age = 0,
                    BodyId = 5,
                    MouthId = 2,
                    EyesId = 1,
                    NoseId = 5,
                    FarmId = 1
                },
                new Pet
                {
                    Id = 2,
                    Name = "Grossmeister",
                    Age = 0,
                    BodyId = 8,
                    MouthId = 3,
                    EyesId = 4,
                    NoseId = 4,
                    FarmId = 2
                }
            );
        }
    }
}
