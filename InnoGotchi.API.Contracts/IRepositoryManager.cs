using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoGotchi.API.Contracts.IModelsRepositories;

namespace InnoGotchi.API.Contracts
{
    public interface IRepositoryManager
    {
        IBodyRepository Body { get; }
        IEyesRepository Eyes { get; }
        INoseRepository Nose { get; }
        IMouthRepository Mouth { get; }
        IFarmRepository Farm { get; }
        IPetRepository Pet { get; }
        IUserRepository User { get; }
        IGuestsRepository Guests { get; }
        IOwnersRepository Owners { get; }
        void Save();
    }
}
