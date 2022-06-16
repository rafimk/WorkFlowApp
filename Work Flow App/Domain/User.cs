using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work_Flow_App.Domain
{
    public class User : IdentityUser<int>
    {
        public bool IsAdmin { get; set; }
        public List<Request> Requests { get; set; }
        public List<RequestWorkFlow> RequestWorkFlows { get; set; }
    }
}
