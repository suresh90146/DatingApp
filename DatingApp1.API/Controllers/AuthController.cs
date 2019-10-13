using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp1.API.Dtos;
using DatingApp1.API.Model;
using DatingApp1.API.Model.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace DatingApp1.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repository, IConfiguration config)
        {
            _config = config;
            _repository = repository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _repository.UserExists(userForRegisterDto.Username))
            {
                return BadRequest("UserName already exist");
            }
            var userToCreate = new User
            {
                UserName = userForRegisterDto.Username
            };

            var createUser = await _repository.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDtos userForLoginDtos)
        {

            var userFromRepo = await _repository.Login(userForLoginDtos.Username.ToLower(), userForLoginDtos.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[] {
                      new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                      new Claim(ClaimTypes.Name,userFromRepo.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHeader = new JwtSecurityTokenHandler();
            var token = tokenHeader.CreateToken(tokenDescription);

            return Ok(new
            {
                token = tokenHeader.WriteToken(token)
            });
        }
    }
}