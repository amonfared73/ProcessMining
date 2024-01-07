
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.ApplicationService.Services.Authenticators;
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
using Microsoft.AspNetCore.Authorization;

namespace ProcessMining.EndPoint.API.Controllers
{
    public class AuthenticationController : ProcessMiningControllerBase<Authentication>
    {
        private readonly IAuthenticationService _service;
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenRepository;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly Authenticator _authenticator;

        public AuthenticationController(IAuthenticationService service, IUserService userService, IRefreshTokenService refreshTokenRepository, RefreshTokenValidator refreshTokenValidator, Authenticator authenticator) : base(service)
        {
            _service = service;
            _userService = userService;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenValidator = refreshTokenValidator;
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
            var existingUserByUsername = await _userService.GetByUsername(userDto.Username);
            if (existingUserByUsername != null)
                return Conflict(new ResponseMessage("Username already exist!"));

            // Creating the user
            string passwordHash = PasswordHasher.Hash(userDto.Password);
            var registrationUser = new User()
            {
                Username = userDto.Username,
                PasswordHash = passwordHash
            };

            await _userService.InsertAsync(registrationUser);
            return Ok(new ResponseMessage(string.Format("Username: {0} created successfully", registrationUser.ToString())));
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

            User user = await _userService.GetByUsername(login.Username);

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
            if (refreshTokenDTO == null)
            {
                return NotFound(new ResponseMessage("Invalid refresh token!"));
            }

            await _refreshTokenRepository.DeleteTokenById(refreshTokenDTO.Id);

            User user = await _userService.GetUserForLoginAsync(refreshTokenDTO.UserId);
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
            if (!int.TryParse(rawUserId, out int userId))
            {
                return Unauthorized();
            }
            await _refreshTokenRepository.DeleteAllUserTokens(userId);
            return NoContent();
        }
    }
}
