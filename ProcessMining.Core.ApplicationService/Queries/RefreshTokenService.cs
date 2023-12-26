using Microsoft.EntityFrameworkCore;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.Attributes;
using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Infra.EntityFramework.DbContextes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.ApplicationService.Queries
{
    [RegistrationRequired]
    public class RefreshTokenService : BaseService<RefreshToken>, IRefreshTokenService
    {
        private readonly IDbContextFactory<ProcessMiningDbContext> _contextFactory;

        public RefreshTokenService(IDbContextFactory<ProcessMiningDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateToken(RefreshToken refreshToken)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                context.RefreshTokens.Add(refreshToken);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteTokenById(int id)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                RefreshToken refreshToken = await context.RefreshTokens.FirstOrDefaultAsync(r => r.Id == id);
                if (refreshToken != null)
                {
                    context.RefreshTokens.Remove(refreshToken);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteAllUserTokens(int userId)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<RefreshToken> refreshTokens = await context.RefreshTokens
                    .Where(t => t.UserId == userId)
                    .ToListAsync();
                context.RemoveRange(refreshTokens);
                await context.SaveChangesAsync();
            }
        }

        public async Task<RefreshToken> GetByToken(string token)
        {
            using (ProcessMiningDbContext context = _contextFactory.CreateDbContext())
            {
                return await context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
            }
        }
    }
}
