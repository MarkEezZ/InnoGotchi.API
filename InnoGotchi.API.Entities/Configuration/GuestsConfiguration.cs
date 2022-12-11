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
    public class GuestsConfiguration : IEntityTypeConfiguration<Guests>
    {
        public void Configure(EntityTypeBuilder<Guests> builder)
        {
            builder.HasData(
                new Guests
                {
                    Id = 1,
                    UserId = 1,
                    FarmId = 2,
                },
                new Guests
                {
                    Id = 2,
                    UserId = 2,
                    FarmId = 1,
                }
            );
        }
    }
}
