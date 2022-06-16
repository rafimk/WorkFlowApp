using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Work_Flow_App.Models
{
    public class AuthenticationResult
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsAdmin { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
