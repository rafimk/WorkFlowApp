using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Domain;
using Work_Flow_App.Models;

namespace Work_Flow_App.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string userName, string password, bool isAdmin);
        Task<AuthenticationResult> LoginAsync(string emuserNameail, string password);
        Task<User> GetByIdAsync(int userId);
    }
}
