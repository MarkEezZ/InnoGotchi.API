using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.Models
{
    public class Settings
    {
        [Column("SettingsId")]
        public int Id { get; set; }
        public string AvatarFileName { get; set; }
        public string ActivityStatus { get; set; }
        public bool IsInGame { get; set; }
        public bool IsMusic { get; set; }
    }
}
