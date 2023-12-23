using Microsoft.AspNetCore.Mvc;
using ProcessMining.Core.ApplicationService.Queries;
using ProcessMining.Core.ApplicationService.Services;
using ProcessMining.Core.Domain.DTOs;
using ProcessMining.Core.Domain.Models;
using ProcessMining.Core.Domain.Responses;
using ProcessMining.Infra.Tools.Hashers;

namespace ProcessMining.EndPoint.API.Controllers
{
    public class UserController : ProcessMiningControllerBase<User>
    {
        private readonly IUserService _service;

        public UserController(IUserService service) : base(service)
        {
            _service = service;
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
    }
}
