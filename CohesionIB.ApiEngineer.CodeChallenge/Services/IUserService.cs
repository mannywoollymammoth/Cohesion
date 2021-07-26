using CohesionIB.ApiEngineer.CodeChallenge.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CohesionIB.ApiEngineer.CodeChallenge.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly IServiceProvider _serviceProvider;
        // IDataHandler _dataHandler;
        public UserService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Task<User> Authenticate(string username, string password)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                IDataHandler _dataHandler = scope.ServiceProvider.GetRequiredService<IDataHandler>();
                User user = _dataHandler.getUser(username);
                if (password == user.Password)
                    return Task.FromResult<User>(user);
            }
            throw new AccessViolationException();
        }
    }
}
