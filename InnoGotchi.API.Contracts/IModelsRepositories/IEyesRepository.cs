using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Contracts.IModelsRepositories
{
    public interface IEyesRepository
    {
        IEnumerable<Eyes> GetAllEyes(bool trackChanges);
        void CreateEyes(Eyes eyes);
        void DeleteEyes(Eyes eyes);
        Eyes GetEyesByName(string name, bool trackChanges);
        Eyes GetEyesById(int eyesId, bool trackChanges);
    }
}
