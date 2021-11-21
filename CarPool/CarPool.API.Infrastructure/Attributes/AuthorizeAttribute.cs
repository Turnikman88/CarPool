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
        public string Roles { get; set; } = $"{GlobalConstants.NotConfirmedRoleName},{GlobalConstants.UserRoleName}";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items[GlobalConstants.UserRoleName] as ResponseAuthDTO;

            if (user != null && user.isBlocked == true)
            {
                context.Result = new UnauthorizedObjectResult(user.Message);
            }
            if (user != null && user.Role == GlobalConstants.AdministratorRoleName)
            {

            }
            else if (user == null || !Roles.Contains(user.Role))
            {
                context.Result = new UnauthorizedObjectResult(GlobalConstants.NOT_AUTHORIZED);
            }
            
        }
    }
}
