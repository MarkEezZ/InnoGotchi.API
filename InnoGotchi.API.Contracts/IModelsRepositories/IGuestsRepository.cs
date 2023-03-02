using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Contracts.IModelsRepositories
{
    public interface IGuestsRepository
    {
        IEnumerable<Guests> GetGuestFarmsByUserId(Guid userId, bool trackChanges);
        IEnumerable<Guests> GetGuestsByFarmId(int farmId, bool trackChanges);
        Guests GetGuestByUserAndFarm(Guid userId, int farmId, bool trackChanges);
        void CreateGuest(Guests guest);
        void DeleteGuest(Guests guest);
    }
}
