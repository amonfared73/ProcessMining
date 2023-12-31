using ProcessMining.Core.Domain.DTOs;
using ProcessMining.Core.Domain.Models;

namespace ProcessMining.Core.ApplicationService.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> GetByUsername(string username);
        Task<User> GetUserForLoginAsync(int id);
        Task Register(UserDto userDto);
    }
}
