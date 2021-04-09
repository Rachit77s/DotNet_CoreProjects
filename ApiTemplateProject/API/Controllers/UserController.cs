using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly DataContext _context;
        
        public UserController(DataContext context)
        {
            _context = context;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;

            //return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            //Find method is used to find the record by its unique id
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound($"Could not find the User with the username of {user}");
            }

            return user;
            
            //return Ok($"Welcome {user.Username} user.");
        }
    }
}
