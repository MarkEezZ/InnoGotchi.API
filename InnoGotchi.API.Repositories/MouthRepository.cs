﻿using InnoGotchi.API.Contracts;
using InnoGotchi.API.Entities.Models;
using InnoGotchi.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Repositories
{
    public class MouthRepository : RepositoryBase<Mouth>, IMouthRepository
    {
        public MouthRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
    }
}
