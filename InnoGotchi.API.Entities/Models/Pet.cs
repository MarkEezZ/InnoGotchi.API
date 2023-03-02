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

        [Required(ErrorMessage = "An Age is a required field.")]
        public int Age { get; set; }

        public DateTime TimeOfCreating { get; set; }
        public DateTime LastEatTime { get; set; }
        public DateTime LastDrinkTime { get; set; }
        public DateTime LastHealthTime { get; set; }
        public DateTime LastMoodTime { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public bool isDead { get; set; }

        [ForeignKey(nameof(Body))]
        public int BodyId { get; set; }

        [ForeignKey(nameof(Eyes))]
        public int EyesId { get; set; }

        [ForeignKey(nameof(Nose))]
        public int? NoseId { get; set; }

        [ForeignKey(nameof(Mouth))]
        public int MouthId { get; set; }

        [ForeignKey(nameof(Farm))]
        public int FarmId { get; set; }
    }
}
