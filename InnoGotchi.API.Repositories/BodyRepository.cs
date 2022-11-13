using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities;
using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Repositories
{
    public class BodyRepository : RepositoryBase<Body>, IBodyRepository
    {
        public BodyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
