using InnoGotchi.API.Contracts;
using InnoGotchi.API.Contracts.IModelsRepositories;
using InnoGotchi.API.Entities;
using InnoGotchi.API.Repositories.ModelsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext repositoryContext;
        private IBodyRepository bodyRepository;
        private INoseRepository noseRepository;
        private IMouthRepository mouthRepository;
        private IEyesRepository eyesRepository;
        private IFarmRepository farmRepository;
        private IPetRepository petRepository;
        private IUserRepository userRepository;
        private IOwnersRepository ownersRepository;
        private IGuestsRepository guestsRepository;

        public RepositoryManager(RepositoryContext _repositoryContext)
        {
            repositoryContext = _repositoryContext;
        }

        public IBodyRepository Body
        {
            get
            {
                if (bodyRepository == null)
                    bodyRepository = new BodyRepository(repositoryContext);

                return bodyRepository;
            }
        }

        public IEyesRepository Eyes
        {
            get
            {
                if (eyesRepository == null)
                    eyesRepository = new EyesRepository(repositoryContext);

                return eyesRepository;
            }
        }

        public INoseRepository Nose
        {
            get
            {
                if (noseRepository == null)
                    noseRepository = new NoseRepository(repositoryContext);

                return noseRepository;
            }
        }

        public IMouthRepository Mouth
        {
            get
            {
                if (mouthRepository == null)
                    mouthRepository = new MouthRepository(repositoryContext);

                return mouthRepository;
            }
        }

        public IFarmRepository Farm
        {
            get
            {
                if (farmRepository == null)
                    farmRepository = new FarmRepository(repositoryContext);

                return farmRepository;
            }
        }

        public IPetRepository Pet
        {
            get
            {
                if (petRepository == null)
                    petRepository = new PetRepository(repositoryContext);

                return petRepository;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(repositoryContext);

                return userRepository;
            }
        }

        public IGuestsRepository Guests
        {
            get
            {
                if (guestsRepository == null)
                    guestsRepository = new GuestsRepository(repositoryContext);

                return guestsRepository;
            }
        }

        public IOwnersRepository Owners
        {
            get
            {
                if (ownersRepository == null)
                    ownersRepository = new OwnersRepository(repositoryContext);

                return ownersRepository;
            }
        }

        void IRepositoryManager.Save()
        {
            repositoryContext.SaveChanges();
        }
    }
}
