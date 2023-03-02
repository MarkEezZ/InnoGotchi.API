using InnoGotchi.API.Entities.Models;
using InnoGotchi.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoGotchi.API.Contracts.IModelsRepositories;
using InnoGotchi.API.Entities.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.API.Repositories.ModelsRepositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<User> GetAllUsers(bool trackChanges)
        {
            return FindAll(trackChanges: false).ToList();
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public User GetUserByLogin(string login, bool trackChanges)
        {
            return FindByCondition(u => u.Login == login, trackChanges).FirstOrDefault();
        }

        public User GetUserById(Guid userId, bool trackChanges)
        {
            return FindByCondition(u => u.Id == userId, trackChanges).FirstOrDefault();
        }

        public void UpdateUser(User updatedUser)
        {
            Update(updatedUser);
        }
    }
}
