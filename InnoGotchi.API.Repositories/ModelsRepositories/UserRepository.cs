using InnoGotchi.API.Entities.Models;
using InnoGotchi.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InnoGotchi.API.Contracts.IModelsRepositories;
using InnoGotchi.API.Entities.DataTransferObjects;

namespace InnoGotchi.API.Repositories.ModelsRepositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<User> GetAllUsers(bool trackChanges)
        {
            return FindAll(trackChanges: false).OrderBy(u => u.Id).ToList();
        }

        public void CreateUserByRegData(UserForRegistrationDto userData)
        {
            User newUser = new User();
            newUser.Login = userData.Login;
            newUser.Email = userData.Email;
            newUser.Password = userData.Password;
            newUser.Name = userData.Name;
            newUser.Surname = userData.Surname;
            newUser.Age = userData.Age;
            Create(newUser);
        }

        public User GetUserByAuthData(UserForAuthorizationDto userData, bool trackChanges)
        {
            return (FindByCondition(u => u.Login == userData.Login, trackChanges: false).ToList()).FirstOrDefault();   
        }

        public User GetUserById(int userId, bool trackChanges)
        {
            return (FindByCondition(u => u.Id == userId, trackChanges: false).ToList()).FirstOrDefault();
        }

        public UserInfoDto GetUserInfo(int userId, bool trackChanges)
        {
            User user = new User();
            user = FindByCondition(u => u.Id == userId, trackChanges: false).FirstOrDefault();

            UserInfoDto userInfo = new UserInfoDto();
            userInfo.Name = user.Name;
            userInfo.Surname = user.Surname;
            userInfo.Email = user.Email;
            userInfo.Password = user.Password;
            userInfo.Age = user.Age;
            userInfo.AvatarFileName = user.Settings.AvatarFileName;
            userInfo.IsInGame = user.Settings.IsInGame;
            userInfo.IsMusic = user.Settings.IsMusic;
            
            return userInfo;
    }

        public void ChangeUserInfo(UserInfoDto userInfo)
        {
            User user = new User();
            user.Name = userInfo.Name;
            user.Surname = userInfo.Surname;
            user.Email = userInfo.Email;
            user.Password = userInfo.Password;
            user.Age = userInfo.Age;
            user.Settings.AvatarFileName = userInfo.AvatarFileName;
            user.Settings.IsInGame = userInfo.IsInGame;
            user.Settings.IsMusic = userInfo.IsMusic;

            Update(user);
        }
    }
}
