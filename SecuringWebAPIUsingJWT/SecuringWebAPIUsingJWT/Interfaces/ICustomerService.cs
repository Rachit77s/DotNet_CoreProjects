using SecuringWebAPIUsingJWT.Requests;
using SecuringWebAPIUsingJWT.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringWebAPIUsingJWT.Interfaces
{
    public interface ICustomerService
    {
        Task<LoginResponse> Login(LoginRequest loginRequest);
    }
}
