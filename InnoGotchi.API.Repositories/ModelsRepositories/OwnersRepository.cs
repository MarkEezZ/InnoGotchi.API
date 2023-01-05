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

        public Owners GetOwnFarmByUserId(int userId, bool trackChanges)
        {
            return FindByCondition(u => u.UserId == userId, trackChanges).FirstOrDefault();
        }
    }
}
