using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecuringWebAPIUsingJWT.Interfaces;
using SecuringWebAPIUsingJWT.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// http://codingsonata.com/secure-asp-net-core-web-api-using-jwt-authentication/
namespace SecuringWebAPIUsingJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            this._customerService = customerService;
        }

        // Follow the below link for this project
        // http://codingsonata.com/secure-asp-net-core-web-api-using-jwt-authentication/
        [HttpPost]
        [Route("login")]
        // Return the JWT token along with other customer details 
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null) return BadRequest("Missing login details");
            if (string.IsNullOrEmpty(loginRequest.Username)) return BadRequest("Missing login details");
            if (string.IsNullOrEmpty(loginRequest.Password)) return BadRequest("Missing login details");

            var loginResponse = await _customerService.Login(loginRequest);

            if (loginResponse == null)
            {
                return BadRequest($"Invalid credentials");
            }

            return Ok(loginResponse);
        }
    }
}
