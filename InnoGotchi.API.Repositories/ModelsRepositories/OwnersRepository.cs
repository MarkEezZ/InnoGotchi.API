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
    public class OwnersRepository : RepositoryBase<Owners>, IOwnersRepository
    {
        public OwnersRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void AddFarmOwner(Owners record)
        {
            Create(record);
        }

        public void RemoveFarmOwner(Owners record)
        {
            Delete(record);
        }

        public Owners GetOwnFarmByUserId(Guid userId, bool trackChanges)
        {
            return FindByCondition(u => u.UserId == userId, trackChanges).FirstOrDefault();
        }

        public Owners GetUserByOwnFarmId(int farmId, bool trackChanges)
        {
            return FindByCondition(r => r.FarmId == farmId, trackChanges).FirstOrDefault();
        }
    }
}
