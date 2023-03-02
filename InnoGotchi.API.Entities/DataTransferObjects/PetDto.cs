using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.DataTransferObjects
{
    public class PetDto
    {
        public string Name { get; set; }
        public int BodyId { get; set; }
        public int EyesId { get; set; }
        public int? NoseId { get; set; }
        public int MouthId { get; set; }
    }
}
