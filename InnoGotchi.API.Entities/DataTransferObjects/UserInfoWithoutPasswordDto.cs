using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.DataTransferObjects
{
    public class UserInfoWithoutPasswordDto
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string AvatarFileName { get; set; }
        public bool IsInGame { get; set; }
        public bool IsMusic { get; set; }
    }
}
