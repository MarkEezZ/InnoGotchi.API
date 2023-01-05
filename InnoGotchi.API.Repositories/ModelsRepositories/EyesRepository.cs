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
    public class EyesRepository : RepositoryBase<Eyes>, IEyesRepository
    {
        public EyesRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateEyes(Eyes eyes)
        {
            Create(eyes);
        }

        public void DeleteEyes(Eyes eyes)
        {
            Delete(eyes);
        }

        public IEnumerable<Eyes> GetAllEyes(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(e => e.Id).ToList();
        }

        public Eyes GetEyesById(int eyesId, bool trackChanges)
        {
            return FindByCondition(e => e.Id == eyesId, trackChanges).FirstOrDefault();
        }

        public Eyes GetEyesByName(string name, bool trackChanges)
        {
            return FindByCondition(e => e.Name == name, trackChanges).FirstOrDefault();
        }
    }
}
