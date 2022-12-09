﻿using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Contracts.IModelsRepositories
{
    public interface IOwnersRepository
    {
        IEnumerable<Owners> GetAllOwners(bool trackChanges);
    }
}
