using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Contracts.IModelsRepositories
{
    public interface IBodyRepository
    {
        IEnumerable<Body> GetAllBodies(bool trackChanges);
        void CreateBody(Body body);
        void DeleteBody(Body body);
        Body GetBodyByName(string name, bool trackChanges);
        Body GetBodyById(int bodyId, bool trackChanges);
    }
}
