using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.DataTransferObjects
{
    public class UserInfoDto
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
#nullable enable
        public string? Password { get; set; }
        public string? NewPassword { get; set; }
        public int? Age { get; set; }
#nullable disable
        public string AvatarFileName { get; set; }
        public bool IsInGame { get; set; }
        public bool IsMusic { get; set; }
    }
}
