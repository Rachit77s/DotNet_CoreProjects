using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
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


        public async Task<User> GetUser(int id)
        {
           var userResult = Task.Run(() => _user.Where(x => x.Id == id).FirstOrDefault());
           var user = await userResult;
           return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var usersResult = Task.Run(() => _user.AsEnumerable());
            // your thread is free to do other useful stuff right nw

            // after a while you need the result, await for myTask:
            var users = await usersResult;

            return users;
        }


    }
}
