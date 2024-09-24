using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace NewsAgregator.Web.Filters
{
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PolicyAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string Permission { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!UserHasPermission(context.HttpContext.User, Permission))
            {
                context.Result = new ForbidResult();
            }
        }

        private bool UserHasPermission(ClaimsPrincipal user, string requiredPolicies)
        {
            var policiesClaim = user.Claims.FirstOrDefault(c => c.Type == "RolePolicies");


            var policies = policiesClaim.Value.Split(';')
                                          .Select(p => p.Trim())
                                          .ToList();
            var listRequirePolicies = requiredPolicies.Split(';')
                                            .Select(p => p.Trim())
                                            .ToList();
            if (listRequirePolicies.All(p => policies.Contains(p)))
            {
                return true;
            }
            else return false;
        }
    }

    //public class PolicyAuthorizationFilter : IAuthorizationFilter
    //{
    //    private readonly string _requiredPolicies;

    //    public PolicyAuthorizationFilter(string requiredPolicies)
    //    {
    //        _requiredPolicies = requiredPolicies;
    //    }

    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        var user = context.HttpContext.User;

    //        if (!user.Identity.IsAuthenticated)
    //        {
    //            context.Result = new UnauthorizedResult();
    //            return;
    //        }

    //        var policiesClaim = user.Claims.FirstOrDefault(c => c.Type == "RolePolicies");

    //        if (policiesClaim == null)
    //        {
    //            context.Result = new ForbidResult();
    //            return;
    //        }

    //        var policies = policiesClaim.Value.Split(';')
    //                                      .Select(p => p.Trim())
    //                                      .ToList();
    //        var listRequirePolicies = _requiredPolicies.Split(';')
    //                                        .Select(p => p.Trim())
    //                                        .ToList();
    //        if (listRequirePolicies.All(p => policies.Contains(p)))
    //        {
    //            return; // Доступ разрешен
    //        }

    //        context.Result = new ForbidResult();
    //    }
    //}
}
