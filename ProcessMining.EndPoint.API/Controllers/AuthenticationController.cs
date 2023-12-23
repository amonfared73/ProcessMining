
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.Models;

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
