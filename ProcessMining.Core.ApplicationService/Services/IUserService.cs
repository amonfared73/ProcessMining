using ProcessMining.Core.Domain.DTOs;
using ProcessMining.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessMining.Core.ApplicationService.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<User> GetByUsername(string username);
        Task Register(UserDto userDto);
    }
}
