using ProcessMining.Core.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.ApplicationService.Services
{
    public interface IRefreshTokenService : IBaseService<RefreshToken>
    {
        Task<RefreshToken> GetByToken(string token);
        Task CreateToken(RefreshToken refreshToken);
        Task DeleteTokenById(int id);
        Task DeleteAllUserTokens(int userId);
    }
}
