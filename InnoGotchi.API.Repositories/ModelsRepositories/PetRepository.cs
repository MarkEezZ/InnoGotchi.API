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
    public class PetRepository : RepositoryBase<Pet>, IPetRepository
    {
        public PetRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public IEnumerable<Pet> GetAllUserPets(bool trackChanges, User user)
        {
            return FindAll(trackChanges)
                .Where(p => p.FarmId == user.Id)
                .OrderBy(p => p.FarmId)
                .ToList();
        }
    }
}
