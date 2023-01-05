using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Contracts.IModelsRepositories
{
    public interface IPetRepository
    {
        void CreatePet(Pet pet);
        IEnumerable<Pet> GetAllFarmPets(int farmId, bool trackChanges);
        Pet GetPetByName(string petName, bool trackChanges);
        void DeletePet(Pet pet);
    }
}
