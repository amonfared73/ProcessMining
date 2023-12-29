using Microsoft.AspNetCore.Authorization;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.Models;

namespace ProcessMining.EndPoint.API.Controllers
{
    [Authorize]
    public class CarController : ProcessMiningControllerBase<Car>
    {
        private readonly ICarService _service;
        public CarController(ICarService service) : base(service)
        {
            _service = service;
        }
    }
}
