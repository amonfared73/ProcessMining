
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.DTOs;
using ProcessMining.Core.Domain.Models;
using ProcessMining.Core.Domain.Responses;
using ProcessMining.Infra.Tools.Hashers;

namespace ProcessMining.EndPoint.API.Controllers
{
    public class AuthenticationController : ProcessMiningControllerBase<Authentication>
    {
        private readonly IAuthenticationService _service;

        public AuthenticationController(IAuthenticationService service) : base(service)
        {
            _service = service;
        }
    }
}
