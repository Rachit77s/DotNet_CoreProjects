using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    // This controller created for in memory data source and Dependency Injection
    public class User2Controller : BaseApiController
    {
        private readonly IUser _repository;

        public User2Controller(IUser repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            IEnumerable<User> users = await _repository.GetUsers();
            return users.ToList();

            //return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            //Find method is used to find the record by its unique id
            var user = await _repository.GetUser(id);

            if (user == null)
            {
                return NotFound($"Could not find the User with the username of {user}");
            }

            return user;

            //return Ok($"Welcome {user.Username} user.");
        }
    }
}
