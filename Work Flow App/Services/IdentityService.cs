using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Data;
using Work_Flow_App.Domain;
using Work_Flow_App.Models;
using Work_Flow_App.Services.Interfaces;

namespace Work_Flow_App.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public IdentityService(UserManager<User> userManager,
                               ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<AuthenticationResult> RegisterAsync(string userName, string password, bool isAdmin)
        {
            var existingUser = await _userManager.FindByNameAsync(userName);

            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { "User name already exists" }
                };
            }

            var newUser = new User
            {
                Email = userName,
                UserName = userName,
                IsAdmin = isAdmin
            };

            var createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }

            return new AuthenticationResult
            {
                Id = newUser.Id,
                UserName = newUser.UserName,
                IsAdmin = newUser.IsAdmin,
                Errors = null
            }; 
        }

        public async Task<AuthenticationResult> LoginAsync(string userName, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);

                if (user == null)
                {
                    return new AuthenticationResult
                    {
                        Errors = new[] { "User dose not exist" }
                    };
                }

                var userHasValidPassword = await _userManager.CheckPasswordAsync(user, password);

                if (!userHasValidPassword)
                {
                    return new AuthenticationResult
                    {
                        Errors = new[] { "User/password combination is wrong" }
                    };
                }

                return new AuthenticationResult
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    IsAdmin = user.IsAdmin,
                    Errors = null
                };
            }
            catch (Exception ex)
            {
                return new AuthenticationResult
                {
                    Errors = new[] { ex.Message.ToString() }
                };
            }
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
