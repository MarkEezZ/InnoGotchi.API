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
    public class BodyConfiguration : IEntityTypeConfiguration<Body>
    {
        public void Configure(EntityTypeBuilder<Body> builder)
        {
            builder.HasData(
                new Body
                {
                    Id = 1,
                    Name = "White Pear",
                    FileName = "pear_white.png",
                    Type = "pear"
                },
                new Body
                {
                    Id = 2,
                    Name = "White Oval",
                    FileName = "oval_white.png",
                    Type = "oval"
                },
                new Body
                {
                    Id = 3,
                    Name = "White Square",
                    FileName = "square_white.png",
                    Type = "square"
                },
                new Body
                {
                    Id = 4,
                    Name = "White Egg",
                    FileName = "egg_white.png",
                    Type = "egg"
                },
                new Body
                {
                    Id = 5,
                    Name = "White Circle",
                    FileName = "circle_white.png",
                    Type = "circle"
                },

                new Body
                {
                    Id = 6,
                    Name = "Black Pear",
                    FileName = "pear_black.png",
                    Type = "pear"
                },
                new Body
                {
                    Id = 7,
                    Name = "Black Oval",
                    FileName = "oval_black.png",
                    Type = "oval"
                },
                new Body
                {
                    Id = 8,
                    Name = "Black Square",
                    FileName = "square_black.png",
                    Type = "square"
                },
                new Body
                {
                    Id = 9,
                    Name = "Black Egg",
                    FileName = "egg_black.png",
                    Type = "egg"
                },
                new Body
                {
                    Id = 10,
                    Name = "Black Circle",
                    FileName = "circle_black.png",
                    Type = "circle"
                }
            );
        }
    }
}
