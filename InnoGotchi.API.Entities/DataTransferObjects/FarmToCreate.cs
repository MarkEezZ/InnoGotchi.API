using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Entities.DataTransferObjects
{
    public class FarmToCreate
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
