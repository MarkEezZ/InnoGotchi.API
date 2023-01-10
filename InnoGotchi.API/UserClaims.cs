using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.DataTransferObjects
{
    public class UserClaims
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public string OwnFarm { get; set; }
    }
}
