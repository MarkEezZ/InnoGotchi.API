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
    public class OwnersConfiguration : IEntityTypeConfiguration<Owners>
    {
        public void Configure(EntityTypeBuilder<Owners> builder)
        {
            builder.HasData(
                new Owners
                {
                    Id = 1,
                    FarmId = 1,
                    UserId = 1
                },
                new Owners
                {
                    Id = 2,
                    FarmId = 2,
                    UserId = 2
                }
            );
        }
    }
}
