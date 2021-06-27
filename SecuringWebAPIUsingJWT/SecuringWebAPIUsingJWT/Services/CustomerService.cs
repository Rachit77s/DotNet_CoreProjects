using SecuringWebAPIUsingJWT.Entities;
using SecuringWebAPIUsingJWT.Helpers;
using SecuringWebAPIUsingJWT.Interfaces;
using SecuringWebAPIUsingJWT.Requests;
using SecuringWebAPIUsingJWT.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringWebAPIUsingJWT.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomersDbContext customersDbContext;
        public CustomerService(CustomersDbContext customersDbContext)
        {
            this.customersDbContext = customersDbContext;
        }

        // function checks in the database for the active customer’s username, password, if these conditions match, then we will generate a JWT and return it in the LoginResponse for the caller, otherwise it will just return a null value in the LoginReponse.
        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            var customer = customersDbContext.Customers.SingleOrDefault(customer => customer.Active && customer.Username == loginRequest.Username);

            if (customer == null) return null;

            var passwordHash = HashingHelper.HashUsingPbkdf2(loginRequest.Password, customer.PasswordSalt);

            if (customer.Password != passwordHash) return null;

            var token = await Task.Run(() => TokenHelper.GenerateToken(customer));

            return new LoginResponse { Username = customer.Username, FirstName = customer.FirstName, LastName = customer.LastName, Token = token };
        }

    }
}
