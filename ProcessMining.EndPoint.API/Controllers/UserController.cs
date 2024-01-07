using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.ApplicationService.Services.Authenticators;
using ProcessMining.Core.ApplicationService.TokenGenerators;
using ProcessMining.Core.ApplicationService.TokenValidators;
using ProcessMining.Core.Domain.Attributes;
using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.BaseViewModels;
using ProcessMining.Core.Domain.DTOs;
using ProcessMining.Core.Domain.Models;
using ProcessMining.Core.Domain.Responses;
using ProcessMining.Core.Domain.ViewModels;
using ProcessMining.Infra.Tools.Hashers;
using System.Security.Claims;

namespace ProcessMining.EndPoint.API.Controllers
{
    [Authorize]
    public class UserController : ProcessMiningControllerBase<User>
    {
        private readonly IUserService _service;
        public UserController(IUserService service) : base(service)
        {
            _service = service;
        }
    }
}
