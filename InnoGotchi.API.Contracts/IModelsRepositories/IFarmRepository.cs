using InnoGotchi.API.Entities.DataTransferObjects;
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
        Farm GetFarmByFarmId(int farmId, bool trackChanges);
        Farm GetFarmByFarmName(string farmName, bool trackChanges);
        void CreateFarm(Farm farm);
    }
}
