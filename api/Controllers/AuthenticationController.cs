using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Lapis.API.Dtos;
using Lapis.API.Helpers;
using Lapis.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lapis.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IRefreshToken _refreshToken;
        private readonly IMapper _mapper;

        public AuthenticationController(IUserService userService, IJwtGenerator jwtGenerator, IRefreshToken refreshToken, IMapper mapper)
        {
            _userService = userService;
            _jwtGenerator = jwtGenerator;
            _refreshToken = refreshToken;
            _mapper = mapper;
        }

        /// <summary>
        /// Using input username and password to create access & refresh token for user
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>User info in body & refresh token in cookie</returns>
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Verify user account
            var userModel = await _userService.GetUserByEmail(loginDto.Email);

            if (userModel == null)
            {
                return NotFound(new ResponseDto(404, "Wrong credentials"));
            }

            bool verified = false;

            verified = BCrypt.Net.BCrypt.Verify(loginDto.Password, userModel.Password);

            if (verified == false)
            {
                return NotFound(new ResponseDto(404, "Wrong credentials"));
            }

            // Create claims for account
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Email, userModel.Email),
                new Claim("id", userModel.Id)
            };

            foreach (var role in userModel.Roles)
            {
                claims.Add(new Claim("role", role));
            }

            // Generate user access and refresh token
            string accessToken = _jwtGenerator.GenerateToken(claims, 60 * 12); // 12 hours

            string refreshToken = _refreshToken.CreateRefreshToken(userModel.Email);

            if (accessToken == null || refreshToken == null)
            {
                return NotFound(new ResponseDto(404, "Token generate fail"));
            }

            // Return refresh token in cookies and access token in body
            Response.Cookies.Append("X-Refresh-Token", refreshToken, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true });

            var loginReponse = _mapper.Map<LoginReponseDto>(userModel);

            loginReponse.AccessToken = accessToken;

            return Ok(loginReponse);
        }

        /// <summary>
        /// Delete saved refresh token
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public ActionResult<ResponseDto> Logout()
        {
            // Try to get cookie
            if (!Request.Cookies.TryGetValue("X-Refresh-Token", out var refreskTokenFromCookie))
            {
                return BadRequest(new ResponseDto(400, "No cookies found"));
            }

            // Get saved email in refresh Token 
            var emailFromToken = _refreshToken.GetEmailByToken(refreskTokenFromCookie);

            try
            {
                _refreshToken.RemoveTokenByEmail(emailFromToken);
            }
            catch
            {
                return BadRequest(new ResponseDto(400, "Saved refresh token not found"));
            }

            return Ok(new ResponseDto(200, "Logout success!"));
        }
    }
}