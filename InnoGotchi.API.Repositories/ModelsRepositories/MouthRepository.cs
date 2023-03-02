using InnoGotchi.API.Entities.Models;
using InnoGotchi.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoGotchi.API.Contracts.IModelsRepositories;
using System.Xml.Linq;

namespace InnoGotchi.API.Repositories.ModelsRepositories
{
    public class MouthRepository : RepositoryBase<Mouth>, IMouthRepository
    {
        public MouthRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateMouth(Mouth mouth)
        {
            Create(mouth);
        }

        public void DeleteMouth(Mouth mouth)
        {
            Delete(mouth);
        }

        public IEnumerable<Mouth> GetAllMouthes(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(m => m.Id).ToList();
        }

        public Mouth GetMouthById(int mouthId, bool trackChanges)
        {
            return FindByCondition(m => m.Id == mouthId, trackChanges).FirstOrDefault();
        }

        public Mouth GetMouthByName(string name, bool trackChanges)
        {
            return FindByCondition(m => m.Name == name, trackChanges).FirstOrDefault();
        }
    }
}
