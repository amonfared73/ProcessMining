using Microsoft.EntityFrameworkCore;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.DTOs;
using ProcessMining.Core.Domain.Models;
using ProcessMining.Infra.EntityFramework.DbContextes;
using ProcessMining.Infra.Tools.Hashers;

namespace ProcessMining.Core.ApplicationService.Queries
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IDbContextFactory<ProcessMiningDbContext> _contextFactory;
        public UserService(IDbContextFactory<ProcessMiningDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<User> GetByUsername(string username)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            }
        }

        public async Task Register(UserDto userDto)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                string passwordHash = PasswordHasher.Hash(userDto.Password);
                var registrationUser = new User()
                {
                    Username = userDto.Username,
                    PasswordHash = passwordHash,
                };
                await base.InsertAsync(registrationUser);
            }
        }
    }
}
