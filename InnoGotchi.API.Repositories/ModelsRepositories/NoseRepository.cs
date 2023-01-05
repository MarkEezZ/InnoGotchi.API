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
    public class NoseRepository : RepositoryBase<Nose>, INoseRepository
    {
        public NoseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateNose(Nose nose)
        {
            Create(nose);
        }

        public void DeleteNose(Nose nose)
        {
            Delete(nose);
        }

        public IEnumerable<Nose> GetAllNoses(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(n => n.Id).ToList();
        }

        public Nose GetNoseById(int noseId, bool trackChanges)
        {
            return FindByCondition(n => n.Id == noseId, trackChanges).FirstOrDefault();
        }

        public Nose GetNoseByName(string name, bool trackChanges)
        {
            return FindByCondition(n => n.Name == name, trackChanges).FirstOrDefault();
        }
    }
}
