using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Contracts.IModelsRepositories
{
    public interface IFarmRepository
    {
        IEnumerable<Farm> GetAllFarms(bool trackChanges);
    }
}
