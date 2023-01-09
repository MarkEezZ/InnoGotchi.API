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

        public void CreatePet(Pet pet)
        {
            Create(pet);
        }

        public void DeletePet(Pet pet)
        {
            Delete(pet);
        }

        public IEnumerable<Pet> GetAllFarmPets(int farmId, bool trackChanges)
        {
            return FindByCondition(p => p.FarmId == farmId, trackChanges).OrderBy(p => p.Age).ToList();
        }

        public Pet GetPetByName(string petName, bool trackChanges)
        {
            return FindByCondition(p => p.Name == petName, trackChanges).FirstOrDefault();
        }

        public void UpdatePet(Pet pet)
        {
            Update(pet);
        }
    }
}
