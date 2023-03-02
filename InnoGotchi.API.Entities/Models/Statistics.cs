using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.Models
{
    public class Statistics
    {
        [Column("StatisticsId")]
        public int Id { get; set; }

        public int AlivePetsCount { get; set; }
        public int DeadPetsCount { get; set; }
        public int AverageFeedingPeriod { get; set; }
        public int AverageThirstPeriod { get; set; }
        public int AverageHappinessPeriod { get; set; }
        public int AverageAge { get; set; }

        [ForeignKey(nameof(Farm))]
        public int FarmId { get; set; }
        public Farm Farm { get; set; }
    }
}
