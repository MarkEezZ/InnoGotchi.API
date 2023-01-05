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
        public int BodyId { get; set; }
        public int EyesId { get; set; }
        public int? NoseId { get; set; }
        public int MouthId { get; set; }
    }
}
