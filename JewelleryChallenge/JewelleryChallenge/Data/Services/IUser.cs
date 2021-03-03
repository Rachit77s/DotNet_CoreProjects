using JewelleryChallenge.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelleryChallenge.Data.Services
{
    public interface IUser
    {
        User GetUser(string username);
        User GetUser(string username, string password);
    }
}
