using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuringWebAPIUsingJWT.Responses
{
    // we need a special Response object, to be returned for the valid customer which will include basic user info and their access token (in JWT format) so that they can pass it within their subsequent requests to the authorized APIs through the Authorization Header as a Bearer token
    public class LoginResponse
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
    }
}
