using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.Models
{
    public class Farm
    {
        [Column("FarmId")]
        public int Id { get; set; }

        [Required(ErrorMessage = "A Name field is required.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the Name is 50.")]
        public string Name { get; set; }
    }
}
