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
    }
}
