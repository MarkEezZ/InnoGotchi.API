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
    public class EyesConfiguration : IEntityTypeConfiguration<Eyes>
    {
        public void Configure(EntityTypeBuilder<Eyes> builder)
        {
            builder.HasData(
                new Eyes
                {
                    Id = 1,
                    Name = "Round Eyes",
                    FileName = "eyes_round.png"
                },
                new Eyes
                {
                    Id = 2,
                    Name = "Oblong Eyes",
                    FileName = "eyes_oblong.png"
                },
                new Eyes
                {
                    Id = 3,
                    Name = "Vertical Oval Eyes",
                    FileName = "eyes_vertical_oval.png"
                },
                new Eyes
                {
                    Id = 4,
                    Name = "Vertical Ellipse Eyes",
                    FileName = "eyes_vertical_ellipse.png"
                },
                new Eyes
                {
                    Id = 5,
                    Name = "Square Eyes",
                    FileName = "eyes_square.png"
                },
                new Eyes
                {
                    Id = 6,
                    Name = "One Eye",
                    FileName = "eyes_one.png"
                },
                new Eyes
                {
                    Id = 7,
                    Name = "Tired Oval Eyes",
                    FileName = "eyes_tired_oval.png"
                }
            );
        }
    }
}
