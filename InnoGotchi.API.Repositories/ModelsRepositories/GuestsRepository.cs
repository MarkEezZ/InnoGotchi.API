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
    public class GuestsRepository : RepositoryBase<Guests>, IGuestsRepository
    {
        public GuestsRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateGuest(Guests guest)
        {
            Create(guest);
        }

        public void DeleteGuest(Guests guest)
        {
            Delete(guest);
        }

        public Guests GetGuestByUserAndFarm(Guid userId, int farmId, bool trackChanges)
        {
            return FindByCondition(r => r.UserId == userId && r.FarmId == farmId, trackChanges).FirstOrDefault();
        }

        public IEnumerable<Guests> GetGuestFarmsByUserId(Guid userId, bool trackChanges)
        {
            return FindByCondition(r => r.UserId == userId, trackChanges).ToList();
        }

        public IEnumerable<Guests> GetGuestsByFarmId(int farmId, bool trackChanges)
        {
            return FindByCondition(r => r.FarmId == farmId, trackChanges).ToList();
        }
    }
}
