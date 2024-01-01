
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.BaseViewModels;
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
        [NonAction]
        public override Task<PagedResultViewModel<Authentication>> GetAllAsync(BaseRequestViewModel request)
        {
            return base.GetAllAsync(request);
        }
        [NonAction]
        public override Task<SingleResultViewModel<Authentication>> GetByIdAsync(int id)
        {
            return base.GetByIdAsync(id);
        }
        [NonAction]
        public override Task<SingleResultViewModel<Authentication>> InsertAsync(Authentication entity)
        {
            return base.InsertAsync(entity);
        }
        [NonAction]
        public override Task<SingleResultViewModel<Authentication>> UpdateAsync(Authentication entity)
        {
            return base.UpdateAsync(entity);
        }
        [NonAction]
        public override Task<SingleResultViewModel<Authentication>> DeleteAsync(int id)
        {
            return base.DeleteAsync(id);
        }
    }
}
