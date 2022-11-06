using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.Models
{
    public class Pet
    {
        [Column("PetId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "A Name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50")]
        public string Name { get; set; }


        [ForeignKey(nameof(Body))]
        public int BodyId { get; set; }
        public Body Body { get; set; }


        [ForeignKey(nameof(Eyes))]
        public int EyesId { get; set; }
        public Eyes Eyes { get; set; }


        [ForeignKey(nameof(Nose))]
        public int NoseId { get; set; }
        public Nose Nose { get; set; }


        [ForeignKey(nameof(Mouth))]
        public int MouthId { get; set; }
        public Mouth Mouth { get; set; }


        [ForeignKey(nameof(Farm))]
        public int FarmId { get; set; }
        public Farm Farm { get; set; }
    }
}
