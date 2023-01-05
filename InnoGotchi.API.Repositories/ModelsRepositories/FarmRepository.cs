using InnoGotchi.API.Entities.Models;
using InnoGotchi.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoGotchi.API.Contracts.IModelsRepositories;
using InnoGotchi.API.Entities.DataTransferObjects;

namespace InnoGotchi.API.Repositories.ModelsRepositories
{
    public class FarmRepository : RepositoryBase<Farm>, IFarmRepository
    {
        public FarmRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateFarm(Farm farm)
        {
            Create(farm);
        }

        public Farm GetFarmByFarmId(int farmId, bool trackChanges)
        {
            return FindByCondition(f => f.Id == farmId, trackChanges).FirstOrDefault();
        }

        public Farm GetFarmByFarmName(string farmName, bool trackChanges)
        {
            return FindByCondition(f => f.Name == farmName, trackChanges).FirstOrDefault();
        }
    }
}
