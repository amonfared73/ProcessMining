using Microsoft.AspNetCore.Mvc;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.Models;

namespace ProcessMining.EndPoint.API.Controllers
{
    public class UserController : ProcessMiningControllerBase<User>
    {
        private readonly IUserService _service;

        public UserController(IUserService service) : base(service)
        {
            _service = service;
        }
    }
}
