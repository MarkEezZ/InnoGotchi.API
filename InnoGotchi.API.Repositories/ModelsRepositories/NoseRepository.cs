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
    }
}
