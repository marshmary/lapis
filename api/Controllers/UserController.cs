using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lapis.API.Dtos;
using Lapis.API.Models;
using Lapis.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using static Lapis.API.Helpers.CommonEnum;

namespace Lapis.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Create new account with role "user"
        /// </summary>
        /// <param name="userCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> CreateAccount([FromBody] UserCreateDto userCreateDto)
        {
            // Validate input user
            var existedUser = await _userService.GetUserByEmail(userCreateDto.Email);

            if (existedUser != null)
            {
                return BadRequest(new ResponseDto(400, "Email is already existed"));
            }

            // Set model password and role
            var userModel = _mapper.Map<User>(userCreateDto);

            userModel.Password = BCrypt.Net.BCrypt.HashPassword(userModel.Password);

            userModel.Roles = new string[] { Roles.user.ToString() };

            // Create new account
            var rs = await _userService.Create(userModel);

            return Created("", new ResponseDto(201, "Account created"));
        }

        /// <summary>
        /// Update name and password of user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPatch("profile/{id}")]
        public async Task<ActionResult<ResponseDto>> PartialUpdateUser(string id, [FromBody] JsonPatchDocument<UserUpdateDto> patchDocument)
        {
            // Validate user input
            var userModelFromService = await _userService.GetUserById(id);
            if (userModelFromService == null)
            {
                return NotFound(new ResponseDto(404, "User not found"));
            }

            // Map input data to model data
            var userToPatch = _mapper.Map<UserUpdateDto>(userModelFromService);
            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!TryValidateModel(userToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(userToPatch, userModelFromService);

            // Update user profile
            await _userService.Update(id, userModelFromService);

            return Ok(new ResponseDto(200));
        }

        /// <summary>
        /// Validate old password and update new password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userUpdatePasswordDto"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPatch("password/{id}")]
        public async Task<ActionResult<ResponseDto>> UpdateUserPassword(string id, [FromBody] UserUpdatePasswordDto userUpdatePasswordDto)
        {
            // Validate input user
            var userModelFromService = await _userService.GetUserById(id);
            if (userModelFromService == null)
            {
                return NotFound(new ResponseDto(404, "User not found"));
            }

            // Validate old password
            var verified = BCrypt.Net.BCrypt.Verify(userUpdatePasswordDto.OldPassword, userModelFromService.Password);

            if (verified != true)
            {
                return BadRequest(new ResponseDto(400, "Old password does not match"));
            }

            // If true then hash new password
            userModelFromService.Password = BCrypt.Net.BCrypt.HashPassword(userUpdatePasswordDto.NewPassword);

            await _userService.Update(id, userModelFromService);

            return Ok(new ResponseDto(200));
        }
    }
}