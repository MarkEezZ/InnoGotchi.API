using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.Models
{
    public class Body
    {
        [Column("BodyId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "A Name field is required.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A FileName field is required.")]
        [MaxLength(100, ErrorMessage = "Maximum length for the FileName is 100.")]
        public string FileName { get; set; }
    }
}
