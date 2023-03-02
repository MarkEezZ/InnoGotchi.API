using InnoGotchi.API.Entities;
using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoGotchi.API.Contracts.IModelsRepositories;
using System.Xml.Linq;

namespace InnoGotchi.API.Repositories.ModelsRepositories
{
    public class BodyRepository : RepositoryBase<Body>, IBodyRepository
    {
        public BodyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBody(Body body)
        {
            Create(body);
        }

        public void DeleteBody(Body body)
        {
            Delete(body);
        }

        public IEnumerable<Body> GetAllBodies(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(b => b.Id).ToList();
        }

        public Body GetBodyById(int bodyId, bool trackChanges)
        {
            return FindByCondition(b => b.Id == bodyId, trackChanges).FirstOrDefault();
        }

        public Body GetBodyByName(string name, bool trackChanges)
        {
            return FindByCondition(b => b.Name == name, trackChanges).FirstOrDefault();
        }
    }
}
