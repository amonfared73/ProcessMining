﻿using Microsoft.AspNetCore.Authorization;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.Attributes;
using ProcessMining.Core.Domain.BaseModels;

namespace ProcessMining.EndPoint.API.Controllers
{
    [Authorize]
    [DisableBaseOperations]
    public class RefreshTokenController : ProcessMiningControllerBase<RefreshToken>
    {
        private readonly IRefreshTokenService _service;

        public RefreshTokenController(IRefreshTokenService service) : base(service)
        {
            _service = service;
        }
    }
}
