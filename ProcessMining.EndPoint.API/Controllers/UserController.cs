using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.ApplicationService.Services.Authenticators;
using ProcessMining.Core.ApplicationService.Services.RefreshTokenRepositories;
using ProcessMining.Core.ApplicationService.TokenGenerators;
using ProcessMining.Core.ApplicationService.TokenValidators;
using ProcessMining.Core.Domain.BaseModels;
using ProcessMining.Core.Domain.DTOs;
using ProcessMining.Core.Domain.Models;
using ProcessMining.Core.Domain.Responses;
using ProcessMining.Core.Domain.ViewModels;
using ProcessMining.Infra.Tools.Hashers;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace ProcessMining.EndPoint.API.Controllers
{
    public class UserController : ProcessMiningControllerBase<User>
    {
        private readonly IUserService _service;
        private readonly IRefreshTokenService _refreshTokenRepository;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly Authenticator _authenticator;

        public UserController(IUserService service, AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator, RefreshTokenValidator refreshTokenValidator, IRefreshTokenService refreshTokenService, Authenticator authenticator) : base(service)
        {
            _service = service;
            _refreshTokenValidator = refreshTokenValidator;
            _refreshTokenRepository = refreshTokenService;
            _authenticator = authenticator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(UserDto userDto)
        {
            // Check validity
            if (!ModelState.IsValid)
            {
                IEnumerable<string> messages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new ResponseMessage(messages));
            }

            // Check confirmation password
            if (userDto.Password != userDto.ConfirmPassword)
                return BadRequest(new ResponseMessage("Password does not match confirm password!"));

            // Check existing username
            var existingUserByUsername = await _service.GetByUsername(userDto.Username);
            if (existingUserByUsername != null)
                return Conflict(new ResponseMessage("Username already exist!"));

            // Creating the user
            string passwordHash = PasswordHasher.Hash(userDto.Password);
            var registrationUser = new User()
            {
                Username = userDto.Username,
                PasswordHash = passwordHash
            };

            await _service.InsertAsync(registrationUser);
            return Ok(new ResponseMessage(string.Format("Username: {0} created successfully", registrationUser.ToString())));
        }

        // Disabling insert user method for the client, all users must be added through registration
        [NonAction]
        public override Task InsertAsync(User entity)
        {
            return base.InsertAsync(entity);
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginRequestViewModel login)
        {
            // Check validity
            if (!ModelState.IsValid)
            {
                IEnumerable<string> messages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new ResponseMessage(messages));
            }

            User user = await _service.GetByUsername(login.Username);

            if (user == null)
                return Unauthorized(new ResponseMessage("User not found!"));

            bool isCorrectPassword = PasswordHasher.Verify(login.Password, user.PasswordHash);
            if (!isCorrectPassword)
                return Unauthorized(new ResponseMessage("Incorrect password!"));

            AuthenticatedUserResponse response = await _authenticator.Authenticate(user);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshAsync(RefreshReuqest refreshReuqest)
        {
            // Check validity
            if (!ModelState.IsValid)
            {
                IEnumerable<string> messages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new ResponseMessage(messages));
            }

            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshReuqest.RefreshToken);
            if (!isValidRefreshToken)
            {
                return BadRequest(new ResponseMessage("Invalid refresh token!"));
            }

            RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByToken(refreshReuqest.RefreshToken);
            if(refreshTokenDTO == null)
            {
                return NotFound(new ResponseMessage("Invalid refresh token!"));
            }

            await _refreshTokenRepository.DeleteTokenById(refreshTokenDTO.Id);

            User user = await _service.GetByIdAsync(refreshTokenDTO.UserId);
            if (user == null)
            {
                return NotFound(new ResponseMessage("User not found!"));
            }

            AuthenticatedUserResponse response = await _authenticator.Authenticate(user);
            return Ok(response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            string rawUserId = HttpContext.User.FindFirstValue("id");
            if(!int.TryParse(rawUserId, out int userId))
            {
                return Unauthorized();
            }
            await _refreshTokenRepository.DeleteAllUserTokens(userId);
            return NoContent();
        }
    }
}
