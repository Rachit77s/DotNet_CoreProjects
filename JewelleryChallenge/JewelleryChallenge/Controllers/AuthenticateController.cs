using JewelleryChallenge.TokenAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelleryChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ITokenManager tokenManager;

        public AuthenticateController(ITokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }


        public IActionResult Authenticate(string username, string password)
        {
            if (tokenManager.Authenticate(username, password))
            {
                return Ok(new { Token = tokenManager.NewToken(username) });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not authorized.");
                return Unauthorized("Unauthorized, You are not authorized.");
            }
        }
    }
}
