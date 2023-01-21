using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.DataTransferObjects
{
    public class PetToReturnDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public BodyDto Body { get; set; }
        public BodyPartDto Eyes { get; set; }
        public BodyPartDto? Nose { get; set; }
        public BodyPartDto Mouth { get; set; }
        public DateTime TimeOfCreating { get; set; }
        public DateTime LastEatTime { get; set; }
        public DateTime LastDrinkTime { get; set; }
        public DateTime LastHealthTime { get; set; }
        public DateTime LastMoodTime { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
    }
}
