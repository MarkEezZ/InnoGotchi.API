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
    }
}
