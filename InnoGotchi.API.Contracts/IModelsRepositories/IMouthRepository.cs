using InnoGotchi.API.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Contracts.IModelsRepositories
{
    public interface IMouthRepository
    {
        IEnumerable<Mouth> GetAllMouthes(bool trackChanges);
        void CreateMouth(Mouth mouth);
        void DeleteMouth(Mouth mouth);
        Mouth GetMouthByName(string name, bool trackChanges);
        Mouth GetMouthById(int mouthId, bool trackChanges);
    }
}
