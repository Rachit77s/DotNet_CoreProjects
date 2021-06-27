using JWTWebApiProject.Entities;
using JWTWebApiProject.Helpers;
using JWTWebApiProject.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTWebApiProject.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 2 hours
            // First, we need to the create security token handler, which will create the token.
            var tokenHandler = new JwtSecurityTokenHandler();

            // Once SecurityTokenHandler is created, we want to make sure that the token key is private key or encrypted.
            // Pass the byte array and get the byte array of the token key.
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            // SecurityTokenDescriptor tells us what kind of claims the user has, what kind of key we are using, what kind of algo we are using.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // This signifies that ClaimType is based on name and name is nothing but the Username passed
                //Subject = new ClaimsIdentity(new[] 
                //{ 
                //    new Claim("id", user.Id.ToString()) 
                //}),

                //ClaimType is based on id and id is nothing but the user.Id passed
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Get the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            // Return the JWT Token to the User
            return tokenHandler.WriteToken(token);
        }
    }
}