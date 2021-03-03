using JewelleryChallenge.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelleryChallenge.Data.Services
{
    public class UserService : IUser
    {
        private readonly List<User> _user;

        public UserService()
        {

            _user = new List<User>()
            {
                new User() { Id = 1, Username = "Normal", Password = "normal" },
                new User() { Id = 2, Username = "Privileged", Password = "privileged" },
            };
        }


        public User GetUser(string username, string password)
        {
            return _user.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
        }

        public User GetUser(string username)
        {
            return _user.Where(x => x.Username == username).FirstOrDefault();
        }

        //new UserModel() { Id = 3, Username = "Normal", Password = "normal" },
        //new UserModel() { Id = 4, Username = "Normal", Password = "normal" },
        //new UserModel() { Id = 5, Username = "Normal", Password = "normal" },
        //new UserModel() { Id = 6, Username = "Normal", Password = "normal" },
        //new UserModel() { Id = 7, Username = "Normal", Password = "normal" },
    }
}
