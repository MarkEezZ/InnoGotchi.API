using InnoGotchi.API.Entities.Models;
using InnoGotchi.API.Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchi.API.Contracts.IModelsRepositories
{
    public interface IUserRepository
    {
        void CreateUserByRegData(User user);
        void DeleteUser(User user);
        User GetUserByLogin(string login, bool trackChanges);
        User GetUserById(int userId, bool trackChanges);
        void UpdateUser(User updatedUser);
        IEnumerable<User> GetAllUsers(bool trackChanges);
    }
}
