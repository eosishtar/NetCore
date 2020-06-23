using NetCore.Core.Boundaries;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Authorisation
{
    public class UsernameProvider : IUsernameProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsernameProvider(IHttpContextAccessor contextAccessor)
        {
            this._httpContextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public string GetUsername()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name ?? "System";
        }

    }
}
