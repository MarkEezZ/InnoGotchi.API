using InnoGotchi.API.Entities;
using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoGotchi.API.Contracts.IModelsRepositories;

namespace InnoGotchi.API.Repositories.ModelsRepositories
{
    public class BodyRepository : RepositoryBase<Body>, IBodyRepository
    {
        public BodyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public IEnumerable<Body> GetAllBodies(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(b => b.Name).ToList();
        }
    }
}
