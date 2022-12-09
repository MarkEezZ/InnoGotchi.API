using InnoGotchi.API.Entities.Models;
using InnoGotchi.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoGotchi.API.Contracts.IModelsRepositories;

namespace InnoGotchi.API.Repositories.ModelsRepositories
{
    public class MouthRepository : RepositoryBase<Mouth>, IMouthRepository
    {
        public MouthRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public IEnumerable<Mouth> GetAllMouthes(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(m => m.Name).ToList();
        }
    }
}
