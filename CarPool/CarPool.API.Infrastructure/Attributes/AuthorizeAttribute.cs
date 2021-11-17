using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarPool.Common;
using CarPool.Services.Mapping.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarPool.API.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; } = GlobalConstants.UserRoleName;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items[GlobalConstants.UserRoleName] as ResponseAuthDTO;

            if (user == null || user.Role != Roles)
            {
                context.Result = new UnauthorizedObjectResult(GlobalConstants.NOT_AUTHORIZED);
            }
            else if (user.isBlocked)
            {
                context.Result = new UnauthorizedObjectResult(user.Message);
            }
        }
    }
}
