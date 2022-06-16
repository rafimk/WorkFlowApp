using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Work_Flow_App.Infrastructure.Interfaces;

namespace Work_Flow_App.Infrastructure
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContext _hcontext;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _hcontext = httpContextAccessor.HttpContext;
        }

        public string GetUserName()
        { 
            return _hcontext.User.FindFirst(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase))?.Value;
        }

        public int GetId()
        { 
            return Convert.ToInt32(_hcontext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
