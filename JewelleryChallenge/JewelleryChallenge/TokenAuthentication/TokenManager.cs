using JewelleryChallenge.Controllers;
using JewelleryChallenge.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelleryChallenge.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private List<Token> listToken;

        public TokenManager()
        {
            listToken = new List<Token>();
        }

        //private  IUser _repository;

        //IUser userController = new UserController(_repository);

        public bool Authenticate(string username, string password)
        {
            UserService user = new UserService();
            var singleUser =  user.GetUser(username, password);

            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password) 
                && singleUser != null)
            {
                //username.ToLower() == "normal" && password.ToLower() == "normal"
                return true;
            }

            //if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
            //{
            //    UserController userController = new UserController();
            //    userController.GetUser(username, password);
            //    return true;
            //}               
            else
                return false;
        }


        public Token NewToken(string username)
        {
            var token = new Token
            {
                Value = Guid.NewGuid().ToString() + username,
                ExpiryDate = DateTime.Now.AddMinutes(20)
            };

            listToken.Add(token);
            return token;
        }


        public bool VerifyToken(string token)
        {
            if (listToken.Any(x => x.Value == token && x.ExpiryDate > DateTime.Now))
            {
                return true;
            }

            return false;
        }
    }
}
