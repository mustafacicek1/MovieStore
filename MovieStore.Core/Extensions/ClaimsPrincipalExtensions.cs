using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetAuthenticatedUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            var result = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            return result;
        }
    }
}
