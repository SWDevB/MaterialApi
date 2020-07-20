using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MaterialApi.Configuration;
using MaterialApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MaterialApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        ILogger<AuthController> _logger;
        UserManager<IdentityUser> _userManager;
        JwtSettings _tokenSettings;

        public AuthController(UserManager<IdentityUser> userManager, IOptions<JwtSettings> tokenSettingsOptions, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _tokenSettings = tokenSettingsOptions.Value;
            _logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<string>> Register([FromBody] User user)
        {
            try
            {
                var newUser = new IdentityUser() { UserName = user.Credentials.UserName };
                var result = await _userManager.CreateAsync(newUser, user.Credentials.Password);

                if (!result.Succeeded)
                {
                    var dictionary = new ModelStateDictionary();

                    foreach (IdentityError error in result.Errors)
                    {
                        dictionary.AddModelError(error.Code, error.Description);
                    }

                    return new BadRequestObjectResult(new { Message = "Registration Failed", Errors = dictionary });
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured on register");
                return Problem("Error Occured");
            }
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<string>> Login([FromBody] Credentials credentials)
        {
            try
            {
                IdentityUser identityUser;
                identityUser = await ValidateUser(credentials);
                
                if (identityUser == null) 
                { 
                    return new BadRequestObjectResult(new { Message = "Login failed" }); 
                }
                var token = GenerateToken(identityUser); 
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured during login");
                return Problem("Error Occured");
            }
        }

        private async Task<IdentityUser> ValidateUser(Credentials credentials) 
        {
            var identityUser = await _userManager.FindByNameAsync(credentials.UserName); 
            if (identityUser != null) 
            { 
                var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, credentials.Password); 
                return result == PasswordVerificationResult.Failed ? null : identityUser; 
            }
            return null;
        }

        private object GenerateToken(IdentityUser identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); var key = Encoding.ASCII.GetBytes(_tokenSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, identityUser.UserName.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpiryTimeInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _tokenSettings.Audience,
                Issuer = _tokenSettings.Issuer
            };
            var token = tokenHandler.CreateToken(tokenDescriptor); return tokenHandler.WriteToken(token);
        }
    }
}
