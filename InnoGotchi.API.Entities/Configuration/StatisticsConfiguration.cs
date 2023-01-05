using InnoGotchi.API.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.Configuration
{
    public class StatisticsConfiguration : IEntityTypeConfiguration<Statistics>
    {
        public void Configure(EntityTypeBuilder<Statistics> builder)
        {
            builder.HasData(
                new Statistics
                {
                    Id = 1,
                    AlivePetsCount = 1,
                    DeadPetsCount = 0,
                    AverageFeedingPeriod = 0,
                    AverageThirstPeriod = 0,
                    AverageHappinessPeriod = 0,
                    AverageAge = 0,
                    FarmId = 1
                },
                new Statistics
                {
                    Id = 2,
                    AlivePetsCount = 1,
                    DeadPetsCount = 0,
                    AverageFeedingPeriod = 0,
                    AverageThirstPeriod = 0,
                    AverageHappinessPeriod = 0,
                    AverageAge = 0,
                    FarmId = 2
                }
            );
        }
    }
}