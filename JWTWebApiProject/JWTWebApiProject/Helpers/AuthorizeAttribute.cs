using JWTWebApiProject.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTWebApiProject.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        // checks if there is an authenticated user attached to the current request (context.HttpContext.Items["User"])
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // An authenticated user is attached by the custom jwt middleware if the request contains a valid JWT access token.
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }

            //On successful authorization no action is taken and the request is passed through to the controller action method, if authorization fails a 401 Unauthorized response is returned.
        }
    }
}
