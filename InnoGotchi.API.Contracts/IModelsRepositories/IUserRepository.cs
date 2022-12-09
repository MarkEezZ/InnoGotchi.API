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
        void CreateUserByRegData(UserForRegistrationDto userData);
        User GetUserByAuthData(UserForAuthorizationDto userData, bool trackChanges);
        User GetUserById(int userId, bool trackChanges);
        UserInfoDto GetUserInfo(int userId, bool trackChanges);
        void ChangeUserInfo(UserInfoDto userInfo);
        IEnumerable<User> GetAllUsers(bool trackChanges);
    }
}
