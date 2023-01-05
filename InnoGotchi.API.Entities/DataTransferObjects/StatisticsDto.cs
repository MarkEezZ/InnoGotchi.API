using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.DataTransferObjects
{
    public class StatisticsDto
    {
        public int AlivePetsCount { get; set; }
        public int DeadPetsCount { get; set; }
        public int AverageFeedingPeriod { get; set; }
        public int AverageThirstPeriod { get; set; }
        public int AverageHappinessPeriod { get; set; }
        public int AverageAge { get; set; }
    }
}
